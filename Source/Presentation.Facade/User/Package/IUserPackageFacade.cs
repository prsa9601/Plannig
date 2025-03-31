using Application.Package.SetActivePackage;
using Application.User._UserPackage.DeActivePackage;
using Application.User._UserPackage.EditPackage;
using Application.User._UserPackage.SetPackage;
using Common.Application;
using MediatR;
using Query.User._Package;
using Query.User._Package.GetById;
using Query.User._Package.GetCurrentUser;
using Query.User._Package.GetFilter;
using Query.User._Package.GetFilterByUserId;
using Query.User._Package.UsersPackagesDTOs;
using Query.User.DTOs;

namespace Presentation.Facade.User.Package
{
    public interface IUserPackageFacade
    {
        Task<OperationResult> SetUserPackage(SetUserPackageCommand command);
        Task<OperationResult> EditUserPackage(EditUserPackageCommand command);
        Task<OperationResult> DeActiveUserPackage(DeActiveUserPackageCommand command);
        Task<List<UserPackageDto>?> GetPackagesCurrentUser(string userId);
        Task<UsersSinglePackagesDto?> GetByIdUsersPackages(string userId, long packageId);
        Task<UsersPackagesFilterResult?> GetFilterUsersPackages(
         UsersPackagesFilterParam param);
        Task<UsersPackagesByUserIdFilterResult?> GetFilterUsersPackagesByUserId(
         UsersPackagesByUserIdFilterParam param);
    }

    internal class UserPackageFacade : IUserPackageFacade
    {
        private readonly IMediator _mediator;

        public UserPackageFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> SetUserPackage(SetUserPackageCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditUserPackage(EditUserPackageCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> DeActiveUserPackage(DeActiveUserPackageCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<List<UserPackageDto>?> GetPackagesCurrentUser(string userId)
        {
            return await _mediator.Send(new GetCurrentUserPackageQuery()
            {
                CurrentUserId = userId
            });
        }
        public async Task<UsersPackagesFilterResult?> GetFilterUsersPackages(
            UsersPackagesFilterParam param)
        {
            return await _mediator.Send(new GetFilterUserPackagesQuery(param));
        }
        public async Task<UsersSinglePackagesDto?> GetByIdUsersPackages(string userId, long packageId)
        {
            return await _mediator.Send(new GetUserPackageByIdQuery()
            {
                packageId = packageId,
                userId = userId
            });
        }

        public async Task<UsersPackagesByUserIdFilterResult?> GetFilterUsersPackagesByUserId(UsersPackagesByUserIdFilterParam param)
        {
            return await _mediator.Send(new GetFilterUserPackagesByUserIdQuery(param));
        }
    }
}
