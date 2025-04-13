using Common.Application;
using Domain.SocialMediaAgg.TelegramAgg;
using Domain.SocialMediaAgg.TelegramAgg.Repository;
using Domain.SocialMediaAgg.TelegramAgg.Service;

namespace Application.SocialMedia.Telegram.Account.CreateAccount
{
    public record class CreateTelegramAccountCommand(string Token, string ChatId, string UserName, bool UsedDefaultToken) : IBaseCommand;

    internal class CreateTelegramAccountCommandHandler : IBaseCommandHandler<CreateTelegramAccountCommand>
    {
        private readonly ITelegramRepository _repository;
        private readonly ITelegramService _service;

        public CreateTelegramAccountCommandHandler(ITelegramRepository repository, ITelegramService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(CreateTelegramAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var telegram = new Domain.SocialMediaAgg.TelegramAgg.Telegram(request.Token,
                    request.ChatId, request.UserName, request.UsedDefaultToken, _service);

                _repository.Add(telegram);
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
