using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs;

namespace Query.User.GetByUserName
{
    public record class GetUserByUserNameQuery(string userName) : IQuery<UserDto?>;


    public class GetUserByUserNameQueryHandler : IQueryHandler<GetUserByUserNameQuery, UserDto?>
    {
        private readonly PlanningContext _context;

        public GetUserByUserNameQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var model = await _context.Users.FirstOrDefaultAsync(i => i.UserName == request.userName);
            return await model.Map(_context)!.SetUserRoleTitles(_context);
        }
    }
}
