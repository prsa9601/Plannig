 using Application.SocialMedia.Instagram.Story.SendToTelegram;
using Common.Application;
using Domain.SocialMediaAgg.TelegramAgg.Repository;

namespace Application.SocialMedia.Telegram.Post.SendVideoToTelegram
{
    public class SendVideoToTelegramCommand : IBaseCommand
    {
        public string  TelegramId { get; set; }
        public long PostId { get; set; }
        public int width { get; set; }
        public string token { get; set; }
        public int height { get; set; }
        public string videoCaption { get; set; }
        public string videoPath { get; set; }
        public string thumbnail { get; set; }
    }
    internal class SendVideoToTelegramCommandHandler : IBaseCommandHandler<SendVideoToTelegramCommand>
    {
        private readonly ITelegramRepository _repository;
        public SendVideoToTelegramCommandHandler(ITelegramRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SendVideoToTelegramCommand request, CancellationToken cancellationToken)
        {
            var telegram = await _repository.GetTrackingWithString(request.TelegramId);
            if(telegram == null)
                return OperationResult.NotFound();

            var post = telegram.Posts.FirstOrDefault(i => i.Id == request.PostId);
            if(post == null)
                return OperationResult.NotFound();

            await _repository.SendVideoToTelegram(request.TelegramId,
                post.Description, post.VideoName, post.Id,
                request.width, request.height, request.thumbnail,
                telegram.Token);

            return OperationResult.Success();
        }
    }
}
