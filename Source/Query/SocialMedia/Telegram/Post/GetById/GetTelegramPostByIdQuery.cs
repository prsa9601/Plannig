using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.SocialMedia.Telegram.Post.DTOs;

namespace Query.SocialMedia.Telegram.Post.GetById
{
    public record class GetTelegramPostByIdQuery(long Id) : IQuery<PostDto?>;
   
    internal class GetTelegramPostByIdQueryHandler : IQueryHandler<GetTelegramPostByIdQuery, PostDto?>
    {
        private readonly PlanningContext _context;

        public GetTelegramPostByIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }
        public async Task<PostDto?> Handle(GetTelegramPostByIdQuery request, CancellationToken cancellationToken)
        {
            var result = (IQueryable<Domain.SocialMediaAgg.TelegramAgg.Post.Post>)
                _context.Telegrams.Include("Posts").Select(i => i.Posts);

            var model = await result.FirstOrDefaultAsync(i => i.Id == request.Id);

            return model.PostMap();
        }
    }
}
