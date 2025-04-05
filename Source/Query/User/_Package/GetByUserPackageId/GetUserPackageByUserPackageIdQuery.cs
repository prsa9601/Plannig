using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs;

namespace Query.User._Package.GetByEventId
{
    public class GetUserPackageByUserPackageIdQuery : IQuery<UserPackageDto?>
    {
        public long Id { get; set; }
        public string UserId { get; set; }
    }
    internal class GetUserPackageByEventIdQueryHandler : IQueryHandler<GetUserPackageByUserPackageIdQuery, UserPackageDto?>
    {
        private readonly PlanningContext _context;

        public GetUserPackageByEventIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<UserPackageDto?> Handle(GetUserPackageByUserPackageIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(i => i.Id.Equals(request.UserId)).Select(i=>i.UserPackages).FirstOrDefaultAsync();
            var result = user!.Where(i => i!.Id.Equals(request.Id)).FirstOrDefault();
            var package = await _context.Packages.Where(i => i.Id.Equals(result.PackageId)).FirstOrDefaultAsync();
            return result.UserPackageMap(package!);
        }
    }
}
