using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.SocialMedia.Instagram.DTOs;
using Query.SocialMedia.Instagram.Post.DTOs;

namespace Query.SocialMedia.Instagram.GetById
{
    internal record class GetByIdQuery(long id) : IQuery<InstagramDto?>;
    internal class GetByIdQueryHandler : IQueryHandler<GetByIdQuery, InstagramDto?>
    {
        private readonly PlanningContext _context;

        public GetByIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<InstagramDto?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var instagram = await _context.Instagram.FirstOrDefaultAsync(i => i.Id == request.id);
            if (instagram == null) 
                return null;

            return instagram.MapInstagram();

        }
    }
}
  