using Common.Query;
using Hangfire;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.SocialMedia.Instagram.Account.DTOs;

namespace Query.SocialMedia.Instagram.Account.GetList
{
    public record class GetListInstagramQuery(string UserName) : IQuery<List<InstagramAccountDto>?>;
    internal class GetListInstagramAccountQueryHandler : IQueryHandler<GetListInstagramQuery, List<InstagramAccountDto>?>
    {
        private readonly PlanningContext _context;

        public GetListInstagramAccountQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<List<InstagramAccountDto>?> Handle(GetListInstagramQuery request, CancellationToken cancellationToken)
        {
            var model = await _context.Instagram.Where(i => i.UserName.Equals(request.UserName)).ToListAsync(cancellationToken);
            if (model.Count() == 0) return null;
            return model.Select(i => i.Map()).ToList()!;
        }
    }
}
