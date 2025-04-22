using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.SocialMedia.Instagram.Account.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace Query.SocialMedia.Instagram.Account.GetById
{
    public record class GetInstagramAccountById(long Id) : IQuery<InstagramAccountDto?>;

    internal class GetInstagramAccountByIdHandler : IQueryHandler<GetInstagramAccountById, InstagramAccountDto?>
    {
        private readonly PlanningContext _context;

        public GetInstagramAccountByIdHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<InstagramAccountDto?> Handle(GetInstagramAccountById request, CancellationToken cancellationToken)
        {
            var Instagram = await _context.Instagram.FirstOrDefaultAsync
                 (i => i.Id.Equals(request.Id));
            if (Instagram == null) return null;
            return Instagram.Map();
        }
    }
}
