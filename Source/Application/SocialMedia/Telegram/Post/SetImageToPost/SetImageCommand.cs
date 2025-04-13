using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.TelegramAgg.Post;
using Domain.SocialMediaAgg.TelegramAgg.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.SocialMedia.Telegram.Post.SetImageToPost
{
    public class SetImageCommand : IBaseCommand
    {
        public string TelegramId { get; set; }
        public List<IFormFile> Images { get; set; }
        public long postId { get; set; }
        //public int Secuence { get; set; }
    }
    internal class SetImageCommandHandler : IBaseCommandHandler<SetImageCommand>
    {
        private readonly ITelegramRepository _repository;
        private readonly IFileService _fileService;
        public SetImageCommandHandler(ITelegramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(SetImageCommand request, CancellationToken cancellationToken)
        {
            var telegram = await _repository.GetTrackingWithString(request.TelegramId);
            if (telegram == null) 
                return OperationResult.NotFound();

            var post = telegram.Posts.FirstOrDefault(i => i.Id == request.postId);
            if (post == null)
                return OperationResult.NotFound();

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
            //string ImageName = await _fileService.SaveFileAndGenerateName(request.Image , Directories.TelegramImages);
            
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
