using System.Runtime.CompilerServices;
using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.TelegramAgg.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.SocialMedia.Telegram.Post.SendPictureToTelegram
{
    public class SendImageToTelegramCommand : IBaseCommand
    {
        public string TelegramId { get; set; }
        public long PostId{ get; set; }

    }
    internal class SendImageToTelegramCommandHandler : IBaseCommandHandler<SendImageToTelegramCommand>
    {
        private readonly ITelegramRepository _repository;
        private readonly IFileService _fileService;
        public SendImageToTelegramCommandHandler(ITelegramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(SendImageToTelegramCommand request, CancellationToken cancellationToken)
        {
            var telegram = await _repository.GetTrackingWithString(request.TelegramId);
            if (telegram == null) 
                return OperationResult.NotFound();
            var post = telegram.Posts.FirstOrDefault(i=>i.Id == request.PostId);
            if(post == null)
                return OperationResult.NotFound();

           // string ImageName = await _fileService.SaveFileAndGenerateName(request.Image, Directories.TelegramImages);
            await _repository.SendImageToTelegram(request.TelegramId, post.Description, post.ImageName, post.Id, telegram.Token );
            return OperationResult.Success();
        }
    }
}
