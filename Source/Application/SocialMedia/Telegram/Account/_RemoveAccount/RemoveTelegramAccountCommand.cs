using Common.Application;
using Domain.SocialMediaAgg.TelegramAgg.Repository;

namespace Application.SocialMedia.Telegram.Account._RemoveAccount
{
    public record class RemoveTelegramAccountCommand(long TelegramId) : IBaseCommand;

    internal class RemoveTelegramAccountCommandHandler : IBaseCommandHandler<RemoveTelegramAccountCommand>
    {
        private readonly ITelegramRepository _repository;

        public RemoveTelegramAccountCommandHandler(ITelegramRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveTelegramAccountCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteOneEntity(i => i.Id.Equals(request.TelegramId));
            if (!result)
                return OperationResult.Error();
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
