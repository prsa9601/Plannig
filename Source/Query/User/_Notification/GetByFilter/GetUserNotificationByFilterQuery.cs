using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User._Friend.DTOs;
using Query.User._Notification.DTOs;

namespace Query.User._Notification.GetByFilter
{
    public class GetUserNotificationByFilterQuery
        : QueryFilter<UserNotificationFilterResult, UserNotificationFilterParam>
    {
        public GetUserNotificationByFilterQuery(UserNotificationFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal class GetUserNotificationByFilterQueryHandler : IQueryHandler<GetUserNotificationByFilterQuery, UserNotificationFilterResult>
    {
        private readonly PlanningContext _context;

        public GetUserNotificationByFilterQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<UserNotificationFilterResult> Handle(GetUserNotificationByFilterQuery request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = await _context.Users.Include(c => c.UserNotifications).
                FirstOrDefaultAsync(i => i.Id == param.UserId, cancellationToken);

            List<UserNotificationFilterDataDto> userNotificationsDto = new();

            if (!string.IsNullOrEmpty(param.Search))
            {
                var userNotification = result.UserNotifications.Where(x =>
                x.Title.Contains(param.Search) || x.Description.Contains(param.Search)
                || x.IsSend == true && x.IsActive == true);

                userNotificationsDto.AddRange(userNotification.Select(s => new UserNotificationFilterDataDto
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
                    UserName = result.UserName ?? result.Name,
                }));

            }
            else
            {
                userNotificationsDto.AddRange(result.UserNotifications.Where(x => x.IsSend == true && x.IsActive == true).Select(s => new UserNotificationFilterDataDto
                {
                    CreationDate = s.CreationDate,
                    IsSeen = s.IsSeen,
                    Description = s.Description,
                    SendType = s.SendType,
                    SendTime = s.SendTime,
                    Id = s.Id,
                    IsActive = s.IsActive,
                    IsSend = s.IsSend,
                    Title = s.Title,
                    UserName = result.UserName ?? result.Name,
                }));
            }

            var skip = (@param.PageId - 1) * @param.Take;
            var model = new UserNotificationFilterResult()
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
