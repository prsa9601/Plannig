using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.SocialMedia.Instagram.Story.DTOs;

namespace Query.SocialMedia.Instagram.Story.GetByFilter
{
    public class GetInstagramStoryByFilterQuery : QueryFilter<StoryFilterResult, StoryFilterParam>
    {
        public GetInstagramStoryByFilterQuery(StoryFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal class GetInstagramStoryByFilterQueryHandler : IQueryHandler<GetInstagramStoryByFilterQuery, StoryFilterResult>
    {
        private readonly PlanningContext _context;

        public GetInstagramStoryByFilterQueryHandler(PlanningContext context)
        {
            _context = context;
        }
        public async Task<StoryFilterResult> Handle(GetInstagramStoryByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _context.Instagram.Include(i => i.Stories).SelectMany(i => i.Stories!);

            var storyList = (IQueryable<Domain.SocialMediaAgg.InstagramAgg.Story.Story>)
                result.Select(i => i);

            ////if (!string.IsNullOrWhiteSpace(@params.Search))
            ////    postList = postList.Where(p =>
            ////        p.Description.Contains(@params.Search));

            if (@params.InstagramId != null && @params.InstagramId > 0)
                storyList = storyList.Where(i => i.InstagramId.Equals(@params.InstagramId));

            switch (@params.SearchOrderBy)
            {
                case StorySearchOrderBy.latest:
                    {
                        storyList = storyList.OrderByDescending(r => r.CreationDate);

                        break;
                    }

            }

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new StoryFilterResult()
            {
                Data = await storyList.Skip(skip).Take(@params.Take).Select(s => s.StoryMap())
                    .ToListAsync(cancellationToken),
                FilterParams = @params
            };
            model.GeneratePaging(storyList, @params.Take, @params.PageId);
            return model;
        }
    }
}
