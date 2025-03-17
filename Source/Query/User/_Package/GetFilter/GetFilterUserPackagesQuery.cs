using Common.Query;
using Domain.UserAgg;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User._Package.UsersPackagesDTOs;
using Query.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.User._Package.GetFilter
{
    public class GetFilterUserPackagesQuery : QueryFilter<UsersPackagesFilterResult, UsersPackagesFilterParam>
    {
        public GetFilterUserPackagesQuery(UsersPackagesFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal class GetFilterUserPackageQueryHandler :
        IQueryHandler<GetFilterUserPackagesQuery, UsersPackagesFilterResult>
    {
        private readonly PlanningContext _context;

        public GetFilterUserPackageQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<UsersPackagesFilterResult> Handle(GetFilterUserPackagesQuery request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = _context.Users.Select(i => i).AsQueryable();

            if (!string.IsNullOrWhiteSpace(@param.userName))
            {
                result = result.Where(i => i.UserName.Contains(@param.userName));
            }

            if (!string.IsNullOrWhiteSpace(@param.phoneNumber))
            {
                result = result.Where(i => i.PhoneNumber == @param.phoneNumber);
            }
            if (@param.ActivePackages)
            {
                result = result.Where(i => i.UserPackages.Any(x => x.ExpiryDate > DateTime.Now));
            }
            if (@param.packageId > 0)
            {
                result = result.Where(i => i.UserPackages.Any(
                    i => i.PackageId == @param.packageId));
            }

            if (!string.IsNullOrWhiteSpace(@param.packageTitle))
            {
                result = result.Where(i => i.UserPackages.Any(
                    i => i.PackageTitle.Contains(@param.packageTitle)));
            }
            //if (@param.packageId>0)
            //{
            //    var listUsers = new List<Domain.UserAgg.User>();
            //    foreach (var item in result.AsEnumerable())
            //    {
            //        foreach (var item2 in item.UserPackages)
            //        {
            //            if (item2.PackageId.Equals(param.packageId))
            //            {
            //                listUsers.Add(item);
            //            }
            //        }
            //    }

            //    result = listUsers.AsQueryable();
            //}
            //if (!string.IsNullOrWhiteSpace(@param.packageTitle))
            //{
            //    var listUsers = new List<Domain.UserAgg.User>();
            //    foreach (var item in result.AsEnumerable()) 
            //    {
            //        foreach(var item2 in item.UserPackages)
            //        {
            //            if (item2.PackageTitle.Contains(param.packageTitle))
            //            {
            //                listUsers.Add(item);
            //            }
            //        }
            //    }

            //    result = listUsers.AsQueryable();
            //}

            switch (param.search)
            {
                case SearchUserPackage.Latest:
                    result = result.OrderByDescending(
                        i => i.UserPackages.OrderByDescending(x => x.CreationDate));
                    break;
                //case SearchUserPackage.BestSeller:
                //    break;
                default:
                    break;
            }

            var skip = (@param.PageId - 1) * @param.Take;
            var model = new UsersPackagesFilterResult()
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
