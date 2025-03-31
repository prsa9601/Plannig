using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.User.UserFilterForAdmin
{
    public class GetUserFilterForAdminQuery : QueryFilter<UserFilterResultForAdmin, UserFilterParamForAdmin>
    {
        public GetUserFilterForAdminQuery(UserFilterParamForAdmin filterParams) : base(filterParams)
        {
        }
    }
    internal class GetUserFilterForAdminQueryHandler : IQueryHandler<GetUserFilterForAdminQuery, UserFilterResultForAdmin>
    {
        private readonly PlanningContext _context;

        public GetUserFilterForAdminQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<UserFilterResultForAdmin> Handle(GetUserFilterForAdminQuery request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(param.UserName))
            {
                result = result.Where(i => i.UserName.Contains(param.UserName));
            }
            if (!string.IsNullOrWhiteSpace(param.Name))
            {
                result = result.Where(i => i.Name.Contains(param.Name));
            }
            if (!string.IsNullOrWhiteSpace(param.Family))
            {
                result = result.Where(result => result.Family!.Contains(param.Family));
            }
            if (!string.IsNullOrWhiteSpace(param.PhoneNumber))
            {
                result = result.Where(i => i.PhoneNumber.Contains(param.PhoneNumber));
            }
            if (!string.IsNullOrWhiteSpace(param.Email))
            {
                result = result.Where(i => i.Email.Contains(param.Email));
            }
            var skip = (@param.PageId - 1) * @param.Take;
            var model = new UserFilterResultForAdmin()
            {
                Data = await result.Skip(skip).Take(@param.Take).Select(s => s.MapForAdmin(_context, param.ActivePackage))
                    .ToListAsync(cancellationToken),
                FilterParams = @param
            };
            model.GeneratePaging(result, @param.Take, @param.PageId);
            return model;
        }
    }
}
