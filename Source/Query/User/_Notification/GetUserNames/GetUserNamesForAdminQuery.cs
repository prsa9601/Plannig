using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User._Notification.DTOs;

namespace Query.User._Notification.GetUserNames
{
    public class GetUserNamesForAdminQuery : IQuery<Dictionary<string, string>>
    {
    }
    internal class GetUserNamesForAdminQueryHandler : IQueryHandler<GetUserNamesForAdminQuery, Dictionary<string, string>>
    {
        private readonly PlanningContext _context;

        public GetUserNamesForAdminQueryHandler(PlanningContext context)
        {
            _context = context;
        }
        //InformationUsersDto
        public async Task<Dictionary<string, string>> Handle(GetUserNamesForAdminQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            var users = await _context.Users.Select(i => i).ToListAsync();
            foreach (var item in users)
            {
                result.TryAdd(item.Id, item.UserName!);
            }

            return result;
        }
    }
}
