using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Package.DTOs;

namespace Query.Package.GetList
{
    public class GetPackageListQuery : IQuery<List<PackageDto?>>
    {
    }
    internal class GetPackageListQueryHandler : IQueryHandler<GetPackageListQuery, List<PackageDto?>>
    {
        private readonly PlanningContext _context;

        public GetPackageListQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<List<PackageDto?>> Handle(GetPackageListQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Packages.
                Select(i=>i.Map()).ToListAsync();
            return result;
        }
    }
}
