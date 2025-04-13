using Application.SocialMedia.Telegram.Account._RemoveAccount;
using Application.SocialMedia.Telegram.Account.CreateAccount;
using Application.SocialMedia.Telegram.Account.EditAccount;
using Common.Application;
using MediatR;

namespace Presentation.Facade.Telegram.Account
{
    public interface IAccountTelegramFacade 
    {
        Task<OperationResult> CreateAccount(CreateTelegramAccountCommand command);
        Task<OperationResult> EditAccount(EditTelegramAccountCommand command);
        Task<OperationResult> DeleteAccount(RemoveTelegramAccountCommand command);
    }
    internal class AccountTelegramFacade : IAccountTelegramFacade
    {
        private readonly IMediator _mediator;

        public AccountTelegramFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> CreateAccount(CreateTelegramAccountCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> DeleteAccount(RemoveTelegramAccountCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditAccount(EditTelegramAccountCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
