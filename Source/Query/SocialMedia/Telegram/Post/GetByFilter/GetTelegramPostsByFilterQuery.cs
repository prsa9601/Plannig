using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.SocialMedia.Telegram.Post.DTOs;

namespace Query.SocialMedia.Telegram.Post.GetByFilter
{
    public class GetTelegramPostsByFilterQuery : QueryFilter<TelegramPostFilterResult, TelegramFilterParam>
    {
        public GetTelegramPostsByFilterQuery(TelegramFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal class GetTelegramPostsByFilterQueryHandler : IQueryHandler<GetTelegramPostsByFilterQuery, TelegramPostFilterResult>
    {
        private readonly PlanningContext _context;

        public GetTelegramPostsByFilterQueryHandler(PlanningContext context)
        {
            _context = context;
        }
        public async Task<TelegramPostFilterResult> Handle(GetTelegramPostsByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _context.Telegrams.Select(i => i.Posts).Include("Posts");

            var postList = (IQueryable<Domain.SocialMediaAgg.TelegramAgg.Post.Post>)
                result.Select(i => i).ToList();

        

            switch (@params.SearchOrderBy)
            {
                case TelegramSearchOrderBy.latest:
                {
                    postList = postList.OrderByDescending(r => r.CreationDate);

                    break;
                }

            }

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new TelegramPostFilterResult()
            {
                Data = await postList.Skip(skip).Take(@params.Take).Select(s => s.PostMap())
                    .ToListAsync(cancellationToken),
                FilterParams = @params
            };
            model.GeneratePaging((IQueryable<Domain.SocialMediaAgg.InstagramAgg.Post.Post>)postList, @params.Take, @params.PageId);
            return model;
        }
    }
}
