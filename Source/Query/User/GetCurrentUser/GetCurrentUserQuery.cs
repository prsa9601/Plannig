using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs;
using System.Security.Claims;

namespace Query.User.GetCurrentUser
{
    public record class GetCurrentUserQuery(string Id) : IQuery<UserDto?>;
    
    
    public class GetUserCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, UserDto?>
    {
        private readonly PlanningContext _context;
        private readonly UserManager<Domain.UserAgg.User> _userManager;

        public GetUserCurrentUserQueryHandler(PlanningContext context, UserManager<Domain.UserAgg.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<UserDto?> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            //var model = await _context.Users.FirstOrDefaultAsync(i => i..Id.ToString() == request.Id);
            var model = await _userManager.Users.FirstOrDefaultAsync(i => i.Id == request.Id);

            return await model.Map(_context)!.SetUserRoleTitles(_context)!;
        }
    }
}
