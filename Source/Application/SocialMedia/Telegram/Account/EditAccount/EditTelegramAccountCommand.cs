using Common.Application;
using Domain.SocialMediaAgg.TelegramAgg.Repository;
using Domain.SocialMediaAgg.TelegramAgg.Service;
using System.Reflection.Metadata.Ecma335;

namespace Application.SocialMedia.Telegram.Account.EditAccount
{
    public record class EditTelegramAccountCommand(long TelegramId,
        string Token, string ChatId, string UserName, bool UsedDefaultToken) : IBaseCommand;

    internal class EditTelegramAccountCommandHandler : IBaseCommandHandler<EditTelegramAccountCommand>
    {
        private readonly ITelegramRepository _repository;
        private readonly ITelegramService _service;

        public EditTelegramAccountCommandHandler(ITelegramRepository repository, ITelegramService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(EditTelegramAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var telegram = await _repository.GetTracking(request.TelegramId);
                if (telegram == null) return OperationResult.NotFound();
                telegram.Edit(request.Token,
                   request.ChatId, request.UserName, request.UsedDefaultToken, _service);

                await _repository.Save();
                return OperationResult.Success();
            }
            catch(Exception exception)
            {
                return OperationResult.Error(exception.Message);
            }
        }
    }
}
