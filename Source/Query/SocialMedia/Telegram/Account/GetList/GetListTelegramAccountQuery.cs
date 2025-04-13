using Common.Query;
using Query.SocialMedia.Telegram.Account.DTOs;

namespace Query.SocialMedia.Telegram.Account.GetList
{
    public class GetListTelegramAccountQuery : IQuery<List<TelegramAccountDto?>>
    {
    }
    public class GetListTelegramAccountQueryHandler
        : IQueryHandler<GetListTelegramAccountQuery, List<TelegramAccountDto?>>
    {
        public Task<List<TelegramAccountDto?>> Handle(GetListTelegramAccountQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
