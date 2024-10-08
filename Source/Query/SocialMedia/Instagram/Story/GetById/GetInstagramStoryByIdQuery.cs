using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.SocialMedia.Instagram.Story.DTOs;

namespace Query.SocialMedia.Instagram.Story.GetById
{
    internal record class GetInstagramStoryByIdQuery(long Id) : IQuery<StoryDto?>;

    internal class GetInstagramStoryByIdQueryHandler : IQueryHandler<GetInstagramStoryByIdQuery, StoryDto?>
    {
        private readonly PlanningContext _context;

        public GetInstagramStoryByIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }
        public async Task<StoryDto?> Handle(GetInstagramStoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = (IQueryable<Domain.SocialMediaAgg.InstagramAgg.Story.Story>)
                _context.Instagram.Include("Stories").Select(i => i.Stories);

            var model = await result.FirstOrDefaultAsync(i => i.Id == request.Id);

            return model.StoryMap();
        }
    }
}
