using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.TelegramAgg.Post;
using Domain.SocialMediaAgg.TelegramAgg.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.SocialMedia.Telegram.Post.AddPost
{
    public record class AddPostCommand(long TelegramId, DateTime dateOfPosting,
        string description, string? link, string slug,
        List<IFormFile> Images, List<IFormFile> Videos) : IBaseCommand;

    public class AddPostCommandHandler : IBaseCommandHandler<AddPostCommand>
    {
        private readonly ITelegramRepository _repository;
        private readonly IFileService _fileService;

        public AddPostCommandHandler(ITelegramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(AddPostCommand request, CancellationToken cancellationToken)
        {
            var telegram = await _repository.GetTracking(request.TelegramId);
            if (telegram == null)
                return OperationResult.NotFound();

            var post = new Domain.SocialMediaAgg.TelegramAgg.Post.Post(
            request.dateOfPosting, request.description);
            telegram.AddPost(post);
            try
            {
                await _repository.Save();
            }
            catch (Exception ex)
            {
                // لاگ خطا
                return OperationResult.Error(ex.Message);
            }

            //List<string> imageNames = new List<string>();
            //foreach (var item in request.Images)
            //{
            //    string imageName = await _fileService.SaveFileAndGenerateName(item, Directories.TelegramImages);
            //    imageNames.Add(imageName);
            //}
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
            //string imageName = await _fileService.SaveFileAndGenerateName(request.Images, Directories.TelegramImages);
            //string videoName = await _fileService.SaveFileAndGenerateName(request.Videos, Directories.TelegramVideo);

            await _repository.Save();

            return OperationResult.Success();
        }
    }
}
