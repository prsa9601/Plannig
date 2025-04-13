using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.SocialMedia.Telegram.Account.DTOs;

namespace Query.SocialMedia.Telegram.Account.GetById
{
    public record class GetTelegramAccountByIdQuery(long AccountId) : IQuery<TelegramAccountDto?>;

    public class GetTelegramAccountByIdQueryHandler
        : IQueryHandler<GetTelegramAccountByIdQuery, TelegramAccountDto?>
    {
        private readonly PlanningContext _context;

        public GetTelegramAccountByIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<TelegramAccountDto?> Handle(GetTelegramAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var TelegramAccount = await _context.Telegrams.FirstOrDefaultAsync
                (i => i.Id.Equals(request.AccountId), cancellationToken);

            if (TelegramAccount == null)
                return null;

            return TelegramAccount.Map();
        }
    }
}
