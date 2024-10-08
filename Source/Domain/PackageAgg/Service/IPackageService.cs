using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PackageAgg.Service
{
    public interface IPackageService
    {
        bool ExistTitle(string title);
    }
}
