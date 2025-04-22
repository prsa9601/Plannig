using System;
using Common.Application;
using Domain.SocialMediaAgg.TelegramAgg.Repository;

namespace Application.SocialMedia.Telegram.Post.SendMessageToTelegram
{
    public class SendMessageToTelegramCommand : IBaseCommand
    {
        //public long id { get; set; }
        public long TelegramId { get; set; }
        //public string token { get; set; }
       // public string imagePath { get; set; }
        public string caption { get; set; }
    }
    internal class SendMessageToTelegramCommandHandler : IBaseCommandHandler<SendMessageToTelegramCommand>
    {
        private readonly ITelegramRepository _repository;

        public SendMessageToTelegramCommandHandler(ITelegramRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SendMessageToTelegramCommand request, CancellationToken cancellationToken)
        {
            var telegram = await _repository.GetTracking(request.TelegramId);
            if (telegram == null)
                return OperationResult.NotFound();

            await _repository.SendMessageToTelegram(telegram.Chat_Id, request.caption, telegram.Token);
            return OperationResult.Success();
        }
    }
}
