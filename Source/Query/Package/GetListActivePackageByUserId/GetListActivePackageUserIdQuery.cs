using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Package.DTOs;

namespace Query.Package.GetListActivePackageByUserId
{
    public class GetListActivePackageUserIdQuery : IQuery<List<PackageDto>?>
    {
        public string Id { get; set; }
    }
    public class GetListActivePackageByUserIdQueryHandler :
        IQueryHandler<GetListActivePackageUserIdQuery, List<PackageDto>?>
    {
        readonly private PlanningContext _context;

        public GetListActivePackageByUserIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<List<PackageDto>?> Handle(GetListActivePackageUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Packages.Where(i => i.Active == true).ToListAsync();
            var resultUser = await _context.Users.Where(i => i.Id == request.Id).FirstOrDefaultAsync();
            var userPackages = resultUser.UserPackages.Where(i => i.IsActive);
            List<Domain.PackageAgg.Package> packages = new List<Domain.PackageAgg.Package>();
            foreach (var item in userPackages)
            {
                packages.Add(await _context.Packages.Where(i => i.Id == item.Id).FirstOrDefaultAsync());

            }
            return packages.Select(i => i.Map()).ToList();
        }
    }
}
