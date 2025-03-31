using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Role.DTOs;
using Query.User._Package.UsersPackagesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.User._Package.GetFilterByUserId
{
    public class GetFilterUserPackagesByUserIdQuery : QueryFilter<UsersPackagesByUserIdFilterResult, UsersPackagesByUserIdFilterParam>
    {
        public GetFilterUserPackagesByUserIdQuery(UsersPackagesByUserIdFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal class GetFilterUserPackagesByUserIdQueryHandler : IQueryHandler<GetFilterUserPackagesByUserIdQuery, UsersPackagesByUserIdFilterResult>
    {
        private readonly PlanningContext _context;

        public GetFilterUserPackagesByUserIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<UsersPackagesByUserIdFilterResult> Handle(GetFilterUserPackagesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = _context.Users.Select(i => i).AsQueryable();

            if (@param?.FilterStartTime != DateTime.MinValue)
            {
                result = result.Where(i => i.CreationDate >= @param.FilterStartTime);
            }
            if (@param?.FilterEndTime != DateTime.MaxValue && @param?.FilterEndTime != DateTime.MinValue)
            {
                result = result.Where(i => i.CreationDate <= @param.FilterEndTime);
            }
            if (@param.ActivePackages == true)
            {
                result = result.Where(i => i.UserPackages.Any(x => x.ExpiryDate > DateTime.Now));
            }
            if (!string.IsNullOrEmpty(@param.UserId))
            {
                result = result.Where(i => i.UserPackages.Any(
                    i => i.UserId == @param.UserId));
            }

            switch (param.search)
            {
                case SearchUserPackage.Latest:
                    result = result.OrderByDescending(
                        i => i.UserPackages.Max(x => x.CreationDate));
                    break;
                //case SearchUserPackage.BestSeller:
                //    break;
                default:
                    break;
            }

            var skip = (@param.PageId - 1) * @param.Take;
            var model = new UsersPackagesByUserIdFilterResult()
            {
                Data = await result.Skip(skip).Take(@param.Take).Select(s => s.UsersPackagesMap()!)
                    .ToListAsync(cancellationToken),
                FilterParams = @param
            };
            model.GeneratePaging(result, @param.Take, @param.PageId);
            return model;
        }
    }
}
