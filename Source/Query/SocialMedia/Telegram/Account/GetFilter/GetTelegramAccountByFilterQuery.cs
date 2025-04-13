using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
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
            private readonly PlanningContext _context;

            public GetTelegramAccountByFilterQueryHandler(PlanningContext context)
            {
                _context = context;
            }

            public async Task<TelegramAccountFilterResult> Handle(GetTelegramAccountByFilterQuery request, CancellationToken cancellationToken)
            {
                var @param = request.FilterParams;
                var result = _context.Telegrams.Select(i => i);
                if (!string.IsNullOrEmpty(param.UserName))
                {
                    result = result.Where(i => i.UserName.Contains(param.UserName));
                }
                if (!string.IsNullOrEmpty(param.Chat_Id))
                {
                    result = result.Where(i => i.Chat_Id.Contains(param.Chat_Id));
                }
                if (param.TelegramAccountId > 0 && param.TelegramAccountId != null)
                {
                    result = result.Where(i => i.Id.Equals(param.TelegramAccountId));
                }
                switch (@param.SearchOrderBy)
                {
                    case TelegramAccountSearchOrderBy.latest:
                        {
                            // storyList = storyList.OrderByDescending(r => r.CreationDate);
                            result.OrderByDescending(i => i.CreationDate);
                            break;
                        }

                }
                var skip = (@param.PageId - 1) * @param.Take;
                var model = new TelegramAccountFilterResult()
                {
                    Data = await result.Skip(skip).Take(@param.Take).Select(s => s.Map()!)
                        .ToListAsync(cancellationToken),
                    FilterParams = @param
                };
                model.GeneratePaging(result, @param.Take, @param.PageId);
                return model;

            }
        }
    }
}
