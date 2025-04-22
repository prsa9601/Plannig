using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.InstagramAgg.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.SocialMedia.Instagram.Post.SetImageToPost
{
    public class SetImageCommand : IBaseCommand
    {
        public string UserName { get; set; }
        public IFormFile ImageFile { get; set; }
        public long postId { get; set; }
    }
    internal class SetImageCommandHandler : IBaseCommandHandler<SetImageCommand>
    {
        private readonly IInstagramRepository _repository;
        private readonly IFileService _fileService;

        public SetImageCommandHandler(IInstagramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(SetImageCommand request, CancellationToken cancellationToken)
        {
            var instagram = await _repository.GetTrackingByUserName(request.UserName);
            if (instagram == null)
                return OperationResult.NotFound();

            foreach (var item in instagram.Posts)
            {
                if (item.Id == request.postId)
                {
                    string imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.InstagramPostImages);
                    //item.SetPostImage(imageName);
                    await _repository.Save();
                    return OperationResult.Success();
                }
            }
            return OperationResult.Error();
        }
    }
}
