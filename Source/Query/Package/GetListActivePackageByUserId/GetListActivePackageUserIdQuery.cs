using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Package.DTOs;

namespace Query.Package.GetListActivePackageByUserId
{
    public class GetListActivePackageUserIdQuery : IQuery<List<PackageDtoForUserProfile?>>
    {
        public string Id { get; set; }
    }
    public class GetListActivePackageByUserIdQueryHandler :
        IQueryHandler<GetListActivePackageUserIdQuery, List<PackageDtoForUserProfile?>>
    {
        readonly private PlanningContext _context;

        public GetListActivePackageByUserIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<List<PackageDtoForUserProfile?>> Handle(GetListActivePackageUserIdQuery request, CancellationToken cancellationToken)
        {
            var resultUser = await _context.Users.Where(i => i.Id == request.Id).FirstOrDefaultAsync();
            var userPackages = resultUser.UserPackages.Where(i => i.IsActive == true).ToList();
            if (resultUser.UserPackages.Count() == 0)
                return null;
            if (userPackages.Count() == 0)
                return null;
            var result = await _context.Packages.ToListAsync();
            List<Domain.PackageAgg.Package> packages = new List<Domain.PackageAgg.Package>();
            List<PackageDtoForUserProfile> packagesModel = new List<PackageDtoForUserProfile>();

            foreach (var item in userPackages)
            {
                foreach (var itemModel in result)
                {
                    if (itemModel.Id == item!.PackageId)
                    {
                        packagesModel.Add(new PackageDtoForUserProfile
                        {
                            Id = item.PackageId,
                            Active = item.IsActive,
                            AllowedEmailCount = item.AllowedEmailCount,
                            AllowedSmsCount = item.AllowedSmsCount,
                            Specification = itemModel.Specification!.MapSpecification()!,
                            CreationDate = item.CreationDate,
                            ExpiryDate = item.ExpiryDate,
                            UserPacakgeId = item.Id,
                            ExpiryTime = itemModel.ExpiryDate,
                            ImageName = itemModel.ImageName,
                            Link = itemModel.Link,
                            Price = itemModel.Price,
                            Title = itemModel.Title,
                        });
                    }
                }
            }
            //packages.AddRange(result.Where(i => i.Id == item.PackageId)!);

            return packagesModel.Select(i => i).ToList()!;
        }
    }
}

