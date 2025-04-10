using Domain.BlogAgg;
using Domain.BlogAgg.Repository;
using Infrastructure._Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.Ef.BlogAgg
{
    internal class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        public BlogRepository(PlanningContext context) : base(context)
        {
        }
    }
}
