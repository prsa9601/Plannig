using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Package.DTOs;

namespace Query.Package.GetPackagesByUserId
{
    public class GetPackagesByUserIdQuery : IQuery<List<PackageDto>?>
    {
        public string Id { get; set; }
    }
    internal class GetPackagesByUserIdQueryHandler : IQueryHandler<GetPackagesByUserIdQuery, List<PackageDto>?>
    {
        private readonly PlanningContext _context;

        public GetPackagesByUserIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<List<PackageDto>?> Handle(GetPackagesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var resultUser = await _context.Users.Where(i => i.Id == request.Id).FirstOrDefaultAsync();
            if (resultUser == null)
                return null;
            var userPackages = resultUser.UserPackages.Where(i => i.IsActive==true);
            List<Domain.PackageAgg.Package> packages = new List<Domain.PackageAgg.Package>();
            foreach (var item in userPackages)
            {
                packages.Add(await _context.Packages.Where(i => i.Id == item.Id).FirstOrDefaultAsync());

            }
            return packages.Select(i => i.Map()).ToList()!;
        }
    }
}
