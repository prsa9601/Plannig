using Application.Package.Add;
using Application.Package.Delete;
using Application.Package.Edit;
using Application.Package.RemoveActivePackage;
using Application.Package.SetActivePackage;
using Application.Package.SetSpecification;
using Application.SocialMedia.Instagram.Post.SetImageToPost;
using Common.Application;
using MediatR;
using Query.Package.DTOs;
using Query.Package.GetById;
using Query.Package.GetList;
using Query.Package.GetListActivePackageByUserId;
using Query.Package.GetPackagesByUserId;

namespace Presentation.Facade.Package
{
    internal class PackageFacade : IPackageFacade
    {
        private readonly IMediator _mediator;

        public PackageFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Add(AddPackageCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Delete(RemovePackageCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditPackageCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> RemoveActivePackage(RemoveActivePackageCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<List<PackageDto?>> GetListPackages()
        {
            return await _mediator.Send(new GetPackageListQuery());
        }

        public async Task<PackageDto?> GetPackage(long id)
        {
            return await _mediator.Send(new GetPackageByIdQuery(id));
        }

        public async Task<OperationResult> SetImage(SetImageCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SetSpecification(SetSpecificationPackageCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SetActivePackage(SetActivePackageCommand command)
        {
            return await _mediator.Send(command);
        }
        public async Task<List<PackageDto>?> GetPackagesByUserId(string UserId)
        {
            return await _mediator.Send(new GetPackagesByUserIdQuery() { Id = UserId });
        }
        public async Task<List<PackageDtoForUserProfile?>> GetListPackagesActiveByUserId(string id)
        {
            return await _mediator.Send(new GetListActivePackageUserIdQuery(){Id = id});
        }
    }
}
