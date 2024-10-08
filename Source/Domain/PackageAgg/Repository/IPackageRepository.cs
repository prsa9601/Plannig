using Common.Domain.Repository;

namespace Domain.PackageAgg.Repository
{
    public interface IPackageRepository :IBaseRepository<Package>
    {
        bool DeletePackage(long id);
    }
}
