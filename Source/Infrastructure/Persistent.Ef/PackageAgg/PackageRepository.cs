using Domain.PackageAgg;
using Domain.PackageAgg.Repository;
using Infrastructure._Utilities;

namespace Infrastructure.Persistent.Ef.PackageAgg
{
    public class PackageRepository : BaseRepository<Package> , IPackageRepository
    {
        public PackageRepository(PlanningContext context) : base(context)
        {
        }

        public bool DeletePackage(long id)
        {
            var package = Context.Packages.Where(i => i.Id == id).FirstOrDefault();
            if (package != null)
            {
                Context.Packages.Remove(package);
                return true;
            }

            return false;
        }
    }
}
