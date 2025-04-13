using Common.Query;
using Query.SocialMedia.Telegram.Account.DTOs;

namespace Query.SocialMedia.Telegram.Account.GetFilter
{
    public class GetTelegramAccountByFilterQuery : QueryFilter<TelegramAccountFilterResult, TelegramAccountFilterParam>
    {
        public GetTelegramAccountByFilterQuery(TelegramAccountFilterParam filterParams) : base(filterParams)
        {
        }
        public class GetTelegramAccountByFilterQueryHandler : IQueryHandler<GetTelegramAccountByFilterQuery, TelegramAccountFilterResult>
        {
            public Task<TelegramAccountFilterResult> Handle(GetTelegramAccountByFilterQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
