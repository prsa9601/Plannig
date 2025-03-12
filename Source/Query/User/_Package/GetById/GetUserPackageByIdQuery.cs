using Common.Query;
using Infrastructure.Persistent.Ef;
using Query.User._Package.UsersPackagesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Query.User._Package.GetById
{
    public class GetUserPackageByIdQuery : IQuery<UsersSinglePackagesDto?>
    {
        public required long packageId { get; set; }
        public required string userId { get; set; }
    }
    internal class GetUserPackageByIdQueryHandler : IQueryHandler<GetUserPackageByIdQuery,
        UsersSinglePackagesDto?>
    {
        private readonly PlanningContext _context;

        public GetUserPackageByIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<UsersSinglePackagesDto?> Handle(GetUserPackageByIdQuery request, CancellationToken cancellationToken)
        {
            var user =  _context.Users.FirstOrDefault(i => i.Id.Equals(request.userId));
               return user.UsersSinglePackagesMap
            (request.userId, request.packageId); 
        }
    }

}
