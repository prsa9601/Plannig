using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.TelegramAgg.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.SocialMedia.Telegram.Post.AddPost
{
    public record class AddPostCommand(string TelegramId, DateTime dateOfPosting, string description, string link, string slug, IFormFile Image, string VideoName) : IBaseCommand;

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
            var telegram = await _repository.GetTrackingWithString(request.TelegramId);
            if(telegram == null) 
                return OperationResult.NotFound();
            //List<string> imageNames = new List<string>();
            //foreach (var item in request.Images)
            //{
            //    string imageName = await _fileService.SaveFileAndGenerateName(item, Directories.TelegramImages);
            //    imageNames.Add(imageName);
            //}
            string imageName = await _fileService.SaveFileAndGenerateName(request.Image, Directories.TelegramImages);

            telegram.AddPost(new Domain.SocialMediaAgg.TelegramAgg.Post.Post(
            request.dateOfPosting, request.description, request.link, imageName, request.VideoName));
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
