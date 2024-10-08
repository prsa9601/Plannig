using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.SocialMedia.Instagram.Post.DTOs;

namespace Query.SocialMedia.Instagram.Post.GetById
{
    public record class GetInstagramByIdQuery(long Id) : IQuery<PostDto?>;
    internal class GetInstagramByIdQueryHandler : IQueryHandler<GetInstagramByIdQuery, PostDto?>
    {
        private readonly PlanningContext _context;

        public GetInstagramByIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }
        public async Task<PostDto?> Handle(GetInstagramByIdQuery request, CancellationToken cancellationToken)
        {
            var result = (IQueryable<Domain.SocialMediaAgg.InstagramAgg.Post.Post>)
                _context.Instagram.Include("Posts").Select(i => i.Posts);

            var model = await result.FirstOrDefaultAsync(i => i.Id == request.Id);

            return model.PostMap();
        }
    }
}
