using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User._Notification.DTOs;
using System.Collections.Generic;

namespace Query.User._Notification.GetByFilterForAdmin
{
    public class GetUserNotificationByFilterForAdminQuery :
        QueryFilter<UserNotificationFilterResultForAdmin, UserNotificationFilterParamForAdmin>
    {
        public GetUserNotificationByFilterForAdminQuery(UserNotificationFilterParamForAdmin
            filterParams) : base(filterParams)
        {
        }
    }
    internal class GetUserNotificationByFFilterForAdminQueryHandler :
        IQueryHandler<GetUserNotificationByFilterForAdminQuery, UserNotificationFilterResultForAdmin>
    {
        private readonly PlanningContext _context;

        public GetUserNotificationByFFilterForAdminQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<UserNotificationFilterResultForAdmin> Handle(GetUserNotificationByFilterForAdminQuery request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = await _context.Users.Include(c => c.UserNotifications).ToListAsync();

            var users = result.ToDictionary(i => i.Id, i => i.UserName);

            List<UserNotificationDtoForAdmin> userNotificationsDto = new();

            var userNotification = result.SelectMany(i => i.UserNotifications);

            userNotificationsDto.AddRange(userNotification.Select(s => new UserNotificationDtoForAdmin
            {
                CreationDate = s.CreationDate,
                IsSeen = s.IsSeen,
                Description = s.Description,
                Id = s.Id,
                SendType = s.SendType,
                SendTime = s.SendTime,
                IsActive = s.IsActive,
                IsSend = s.IsSend,
                Title = s.Title,
                UserNames = s.UserIds.Where(id => users.ContainsKey(id)).Select(id => users[id]).ToList()

            }));

            if (!string.IsNullOrEmpty(param.Search))
            {
                userNotificationsDto = userNotificationsDto.Where(i => i.Description.Contains(param.Search)
                || i.Title.Contains(param.Search)).ToList();

            }


            if (param.IsSend == true)
            {
                userNotificationsDto = userNotificationsDto.Where(i => i.IsSend == true).ToList();
            }
            if (param.IsSend == false /*&& param.Search != null*/)
            {
                userNotificationsDto = userNotificationsDto.Where(i => i.IsSend == false).ToList();
            }

            var skip = (@param.PageId - 1) * @param.Take;
            var model = new UserNotificationFilterResultForAdmin()
            {
                Data = await Task.Run(() => userNotificationsDto.Skip(skip).Take(@param.Take).Select(s => s).AsQueryable()
                    .ToList()),
                FilterParams = @param
            };
            model.GeneratePaging(userNotificationsDto.AsQueryable(), @param.Take, @param.PageId);
            return model;
        }
    }
}
