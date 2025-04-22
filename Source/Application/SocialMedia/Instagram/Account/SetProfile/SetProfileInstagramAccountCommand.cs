using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.InstagramAgg.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.SocialMedia.Instagram.Account.SetProfile
{
    public class SetProfileInstagramAccountCommand : IBaseCommand
    {
        public long Id { get; set; }
        public IFormFile Image { get; set; }
    }
    internal class SetProfileInstagramAccountCommandHandler : IBaseCommandHandler<SetProfileInstagramAccountCommand>
    {
        private readonly IInstagramRepository _repository;
        private readonly IFileService _fileService;

        public SetProfileInstagramAccountCommandHandler(IInstagramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(SetProfileInstagramAccountCommand request, CancellationToken cancellationToken)
        {
            var instagram = await _repository.GetTracking(request.Id);
            if (instagram == null)
                return OperationResult.NotFound();
            if (request.Image != null)
            {
                string imageName = await _fileService.SaveFileAndGenerateName(
                    request.Image, Directories.InstagramProfile);
                _fileService.DeleteFile(Directories.InstagramProfile, instagram.Profile!);
                instagram.ChangeImage(imageName);
                await _repository.Save();
                return OperationResult.Success();
            }
            return OperationResult.Error();
        }
    }
}
