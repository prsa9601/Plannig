using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.SocialMedia.Telegram.Account.DTOs;

namespace Query.SocialMedia.Telegram.Account.GetList
{
    public record class GetListTelegramAccountQuery(string UserName) : IQuery<List<TelegramAccountDto?>>;
    public class GetListTelegramAccountQueryHandler
        : IQueryHandler<GetListTelegramAccountQuery, List<TelegramAccountDto?>>
    {
        private readonly PlanningContext _context;

        public GetListTelegramAccountQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<List<TelegramAccountDto?>> Handle(GetListTelegramAccountQuery request, CancellationToken cancellationToken)
        {
            var TelegramAccounts = _context.Telegrams.Where(i => i.UserName.Equals(request.UserName));
            if (TelegramAccounts == null) return null!;
            return await TelegramAccounts.Select(i => i.Map()).ToListAsync(cancellationToken);
        }
    }
}
