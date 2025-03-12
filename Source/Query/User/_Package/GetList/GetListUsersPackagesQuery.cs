using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User._Package.UsersPackagesDTOs;

namespace Query.User._Package.GetList
{
    public class GetListUsersPackagesQuery : IQuery<List<UsersPackagesDTOs.UsersPackagesDto?>>
    {
    }
    public class GetListUsersPackagesQueryHandler : IQueryHandler<GetListUsersPackagesQuery
        , List<UsersPackagesDTOs.UsersPackagesDto?>>
    {
        private readonly PlanningContext _context;

        public GetListUsersPackagesQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<List<UsersPackagesDto?>> Handle(GetListUsersPackagesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.Select(i=>i.UsersPackagesMap())
                .ToListAsync(cancellationToken);
            
        }
    }
}
//getById For Admin
