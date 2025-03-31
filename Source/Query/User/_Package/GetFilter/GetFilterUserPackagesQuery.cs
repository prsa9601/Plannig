using Common.Query;
using Domain.PackageAgg;
using Domain.UserAgg;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User._Package.UsersPackagesDTOs;

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


            List<UsersPackagesFilterDataDto> userResult = new List<UsersPackagesFilterDataDto>();
            foreach (var item in await result.ToListAsync())
            {
                foreach (var item2 in item.UserPackages)
                {
                    var userPackageModel = new UsersPackagesFilterDataDto
                    {
                        CreationDate = item2!.CreationDate,
                        UserId = item.Id,
                        Email = item.Email,
                        UserName = item.UserName!,
                        PhoneNumber = item.PhoneNumber,
                        userPackages = new DTOs.UserPackageDto
                        {
                            AllowedEmailCount = item2.AllowedEmailCount,
                            AllowedSmsCount = item2.AllowedSmsCount,
                            IsActive = item2.IsActive,
                            CreationDate = item2.CreationDate,
                            ExpiryDate = item2.ExpiryDate,
                            Id = item2.Id,
                            PackageId = item2.PackageId,
                            UserId = item2.UserId,
                        }
                    };
                    userResult.Add(userPackageModel);
                }
            }
            if (!string.IsNullOrWhiteSpace(@param.userName))
            {
                userResult = userResult.AsQueryable().Where(i => i.UserName.Contains(@param.userName)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(@param.phoneNumber))
            {
                userResult = userResult.AsQueryable().Where(i => i.PhoneNumber.Contains(@param.phoneNumber)).ToList();
            }
            if (@param?.FilterStartTime != DateTime.MinValue)
            {
                userResult = userResult.AsQueryable().Where(i => i.CreationDate >= @param!.FilterStartTime).ToList();
            }
            if (@param?.FilterEndTime != DateTime.MaxValue && @param?.FilterEndTime != DateTime.MinValue)
            {
                userResult = userResult.AsQueryable().Where(i => i.CreationDate <= @param!.FilterEndTime).ToList();
            }
            if (@param!.ActivePackages == true)
            {
                userResult = userResult.AsQueryable().Where(i => i.userPackages.ExpiryDate > DateTime.Now).ToList();
            }
            //if (@param.packageId > 0)
            //{
            //    result = result.Where(i => i.UserPackages.Any(
            //        i => i.PackageId == @param.packageId));
            //}

            switch (param.search)
            {
                case SearchUserPackage.Latest:
                    userResult = userResult.AsQueryable().OrderByDescending(
                        (x => x.CreationDate)).ToList();
                    break;
                //case SearchUserPackage.BestSeller:
                //    break;
                default:
                    break;
            }

            var skip = (@param.PageId - 1) * @param.Take;
            var model = new UsersPackagesFilterResult()
            {
                Data = userResult.Skip(skip).Take(@param.Take).Select(s => s)
                    .ToList(),
                FilterParams = @param
            };
            model.GeneratePaging(userResult.AsQueryable(), @param.Take, @param.PageId);
            return model;
        }
    }
}
