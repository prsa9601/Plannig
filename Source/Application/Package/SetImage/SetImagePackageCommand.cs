using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.PackageAgg.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.Package.SetImage
{
    public class SetImagePackageCommand : IBaseCommand
    {
        public long Id { get; set; }
        public IFormFile Picture { get; set; }
    }
    public class SetImagePackageCommandHandler : IBaseCommandHandler<SetImagePackageCommand>
    {
        private readonly IFileService _fileService;
        private readonly IPackageRepository _repository;

        public SetImagePackageCommandHandler(IFileService fileService, IPackageRepository repository)
        {
            _fileService = fileService;
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SetImagePackageCommand request, CancellationToken cancellationToken)
        {
            var package = await _repository.GetTracking(request.Id);
            var oldAvatar = package.ImageName;
            if (request.Picture != null)
            {
                var imageName = await _fileService
                    .SaveFileAndGenerateName(request.Picture, Directories.PackageImages);
                package.SetImage(imageName);
            }
            
            DeleteOldImage(request.Picture, oldAvatar);

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
