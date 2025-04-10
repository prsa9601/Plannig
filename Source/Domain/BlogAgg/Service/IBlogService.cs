using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BlogAgg.Service
{
    public interface IBlogService
    {
        bool SlugExist(string slug);

    }
}
