using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Package.DTOs;

namespace Query.Package.GetById
{
    public record GetPackageByIdQuery(long id) : IQuery<PackageDto?>;
  
    internal class GetPackageByIdQueryHandler : IQueryHandler<GetPackageByIdQuery, PackageDto?>
    {
        private readonly PlanningContext _context;

        public GetPackageByIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<PackageDto?> Handle(GetPackageByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Packages
                    .FirstOrDefaultAsync(i=>i.Id == request.id);

            return result.Map();
        }
    }
}
