using Application.SocialMedia.Telegram.Account._RemoveAccount;
using Application.SocialMedia.Telegram.Account.CreateAccount;
using Application.SocialMedia.Telegram.Account.EditAccount;
using Common.Application;
using MediatR;
using Query.SocialMedia.Telegram.Account.DTOs;
using Query.SocialMedia.Telegram.Account.GetById;
using Query.SocialMedia.Telegram.Account.GetFilter;
using Query.SocialMedia.Telegram.Account.GetList;

namespace Presentation.Facade.Telegram.Account
{
    public interface IAccountTelegramFacade 
    {
        Task<OperationResult> CreateAccount(CreateTelegramAccountCommand command);
        Task<OperationResult> EditAccount(EditTelegramAccountCommand command);
        Task<OperationResult> DeleteAccount(RemoveTelegramAccountCommand command);
        Task<TelegramAccountDto?> GetById(long TelegramAccountId);
        Task<List<TelegramAccountDto?>> GetList(string UserName);
        Task<TelegramAccountFilterResult?> GetByFilter(TelegramAccountFilterParam param);
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

        public async Task<TelegramAccountFilterResult?> GetByFilter(TelegramAccountFilterParam param)
        {
            return await _mediator.Send(new GetTelegramAccountByFilterQuery(param));
        }

        public async Task<TelegramAccountDto?> GetById(long TelegramAccountId)
        {
            return await _mediator.Send(new GetTelegramAccountByIdQuery(TelegramAccountId));
        }

        public async Task<List<TelegramAccountDto?>> GetList(string UserName)
        {
            return await _mediator.Send(new GetListTelegramAccountQuery(UserName));
        }
    }
}
