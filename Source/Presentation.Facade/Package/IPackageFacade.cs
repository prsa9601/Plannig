using Application.Package.Add;
using Application.Package.Delete;
using Application.Package.Edit;
using Application.Package.RemoveActivePackage;
using Application.Package.SetActivePackage;
using Application.Package.SetSpecification;
using Application.SocialMedia.Instagram.Post.SetImageToPost;
using Common.Application;
using Query.Package.DTOs;

namespace Presentation.Facade.Package
{
    public interface IPackageFacade
    {
        Task<OperationResult> Add(AddPackageCommand command);
        Task<OperationResult> Edit(EditPackageCommand command);
        Task<OperationResult> Delete(RemovePackageCommand command);
        Task<OperationResult> SetImage(SetImageCommand command);
        Task<OperationResult> SetSpecification(SetSpecificationPackageCommand command);
        Task<OperationResult> SetActivePackage(SetActivePackageCommand command);
        Task<OperationResult> RemoveActivePackage(RemoveActivePackageCommand command);

        Task<List<PackageDto?>> GetListPackages();
        Task<List<PackageDto>?> GetListPackagesActiveByUserId(string Id);
        Task<List<PackageDto>?> GetPackagesByUserId(string UserId);
        Task<PackageDto?> GetPackage(long id);

    }
}
