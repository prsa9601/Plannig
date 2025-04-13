using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.TelegramAgg.Post;
using Domain.SocialMediaAgg.TelegramAgg.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.SocialMedia.Telegram.Post.EditPost
{
    public record class EditPostCommand(long TelegramId, long PostId,
        DateTime DateOfPosting, string Description, 
        string Slug, List<IFormFile> Videos, List<IFormFile> Images) : IBaseCommand;
    internal class EditPostCommandHandler : IBaseCommandHandler<EditPostCommand>
    {
        private readonly ITelegramRepository _repository;
        private readonly IFileService _fileService;

        public EditPostCommandHandler(ITelegramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            var telegram = await _repository.GetTracking(request.TelegramId);
            if (telegram == null)
                return OperationResult.NotFound();

            var post = telegram.Posts.FirstOrDefault(i => i.Id == request.PostId);
            if (post == null)
                return OperationResult.NotFound();
           
            post.Edit(request.DateOfPosting, request.Description);
            if (request.Images != null)
            {
                int i = 1;
                foreach (var item in request.Images)
                {
                    string imageName = await _fileService.
                                SaveFileAndGenerateName(item,
                                Directories.TelegramImages);
                    post.AddImage(new TelegramPostImage(imageName, i));
                    i++;
                }
            }
            if (request.Videos != null)
            {
                var i = 1;
                foreach (var item in request.Videos)
                {
                    string videoName = await _fileService.
                                SaveFileAndGenerateName(item,
                                Directories.TelegramImages);
                    post.AddVideo(new TelegramPostVideo(videoName, i));
                    i++;
                }
            }

            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
