using Common.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Query.SocialMedia.Instagram.Story.DTOs;
using Query.SocialMedia.Instagram.Story;
using Query.SocialMedia.Telegram.DTOs;
using Infrastructure.Persistent.Ef;

namespace Query.SocialMedia.Telegram.GetByFilter
{
    public class GetTelegramByFilterQuery : QueryFilter<TelegramFilterResult, TelegramFilterParam>
    {
        public GetTelegramByFilterQuery(TelegramFilterParam filterParams) : base(filterParams)
        {
        }
    }
}

namespace Query.SocialMedia.Telegram.GetByFilter
{
    public class GetTelegramByFilterQueryHandler : IQueryHandler<GetTelegramByFilterQuery, TelegramFilterResult>
    {
        private readonly PlanningContext _context;
        public async Task<TelegramFilterResult> Handle(GetTelegramByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _context.Telegrams.Select(i => i);

            //var storyList = (IQueryable<Domain.SocialMediaAgg.InstagramAgg.Story.Story>)
            //    result.Select(i => i).ToList();

            ////if (!string.IsNullOrWhiteSpace(@params.Search))
            ////    postList = postList.Where(p =>
            ////        p.Description.Contains(@params.Search));


            switch (@params.SearchOrderBy)
            {
                case PostSearchOrderBy.latest:
                    {
                        // storyList = storyList.OrderByDescending(r => r.CreationDate);
                        result.OrderByDescending(i => i.CreationDate);
                        break;
                    }

            }

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new TelegramFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take).Select(s => s.MapTelegram())
                    .ToListAsync(cancellationToken),
                FilterParams = @params
            };
            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
