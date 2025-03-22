using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs;

namespace Query.User.GetByPhoneNumber
{
    public record class GetUserByPhoneNumberQuery(string phoneNumber) : IQuery<UserDto?>;
    
    
    public class GetUserByPhoneNumberQueryHandler : IQueryHandler<GetUserByPhoneNumberQuery, UserDto?>
    {
        private readonly PlanningContext _context;

        public GetUserByPhoneNumberQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
        {
            var model = await _context.Users.FirstOrDefaultAsync(i => i.PhoneNumber == request.phoneNumber);
            return await model.Map(_context)!.SetUserRoleTitles(_context);
        }
    }
}
 