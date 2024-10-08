using Application.Package.Add;
using Application.Package.Delete;
using Application.Package.Edit;
using Application.Package.SetSpecification;
using Application.SocialMedia.Instagram.Post.SetImageToPost;
using Common.Application;
using MediatR;
using Query.Package.DTOs;
using Query.Package.GetById;
using Query.Package.GetList;

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

        public async Task<List<PackageDto?>> GetListPackages()
        {
            return await _mediator.Send(new GetPackageListQuery());
        }

        public async Task<PackageDto?> GetPackage(long id)
        {
            return await _mediator.Send(new GetPackageByIdQuery(id));
        }

        public Task<OperationResult> SetImage(SetImageCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> SetSpecification(SetSpecificationPackageCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
