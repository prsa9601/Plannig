using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;
using Domain.SocialMediaAgg.TelegramAgg.Repository;

namespace Application.SocialMedia.Telegram.Post.SendMessageToTelegram
{
    public class SendMessageToTelegramCommand : IBaseCommand
    {
        //public long id { get; set; }
        public string TelegramId { get; set; }
        public string token { get; set; }
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
            //var telegram = await _repository.GetTrackingWithString(request.TelegramId);
            //if (telegram == null) 
            //    return OperationResult.NotFound();

            await _repository.SendMessageToTelegram(request.TelegramId, request.caption, request.token);
            return OperationResult.Success();
        }
    }
}
