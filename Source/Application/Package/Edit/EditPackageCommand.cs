using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.PackageAgg;
using Domain.PackageAgg.Repository;
using Domain.PackageAgg.Service;
using Microsoft.AspNetCore.Http;

namespace Application.Package.Edit
{
    public class EditPackageCommand : IBaseCommand
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public long Id { get; set; }
        public IFormFile? Picture { get; set; }
        public Dictionary<string, string> Specifications { get; set; }

    }
    internal class EditPackageCommandHandler : IBaseCommandHandler<EditPackageCommand>
    {
        private readonly IFileService _fileService;
        private readonly IPackageRepository _repository;
        private readonly IPackageService _service;

        public EditPackageCommandHandler(IFileService fileService, IPackageRepository repository, IPackageService service)
        {
            _fileService = fileService;
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(EditPackageCommand request, CancellationToken cancellationToken)
        {
            //addimage
            //var imageName = await _fileService
            //    .SaveFileAndGenerateName(request.ImageFile, Directories.ProductGalleryImage);






            var package = await _repository.GetTracking(request.Id);
            var oldAvatar = package.ImageName;
            if (request.Picture != null)
            {
                var imageName = await _fileService
                    .SaveFileAndGenerateName(request.Picture, Directories.PackageImages);
                package.SetImage(imageName);

                DeleteOldImage(request.Picture, oldAvatar);

            }

            package.Edit(request.Price, request.Title, request.Link, _service);

            var specifications = new List<PackageSpecification>();
            request.Specifications.ToList().ForEach(specification =>
            {
                specifications.Add(new PackageSpecification(specification.Key, specification.Value));
            });

            package.SetSpecification(specifications);
            await _repository.Save();

            return OperationResult.Success();
        }
        private void DeleteOldImage(IFormFile? avatarFile, string oldImage)
        {
            if (avatarFile == null || oldImage == "avatar.png")
                return;

            _fileService.DeleteFile(Directories.UserAvatars, oldImage);
        }
    }

}
