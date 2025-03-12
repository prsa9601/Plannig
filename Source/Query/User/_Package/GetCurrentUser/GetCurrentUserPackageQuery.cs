using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs;

namespace Query.User._Package.GetCurrentUser
{
    public class GetCurrentUserPackageQuery : IQuery<List<UserPackageDto>?>
    {
        public string CurrentUserId { get; set; }
    }
    internal class GetCurrentUserPackageQueryHandler : IQueryHandler
        <GetCurrentUserPackageQuery, List<UserPackageDto>?>
    {
        private readonly PlanningContext _context;

        public GetCurrentUserPackageQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<List<UserPackageDto>?> Handle(GetCurrentUserPackageQuery request, CancellationToken cancellationToken)
        {
            var packages = await _context.Users.Where(i => i.Id.Equals(request.CurrentUserId))
                .FirstOrDefaultAsync(cancellationToken);

            return packages!.UserPackages!.PackagesMap()!;

            //.ToListAsync(cancellationToken);
        }
    }

}

