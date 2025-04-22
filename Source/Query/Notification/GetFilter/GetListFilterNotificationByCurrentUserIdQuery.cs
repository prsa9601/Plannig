using Common.Query;
using Infrastructure.Persistent.Ef;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Query.Notification.DTOs;

namespace Query.Notification.GetList
{
    public class GetListFilterNotificationByCurrentUserIdQuery : QueryFilter<NotificationFilterResult, NotificationFilterParam>
    {
        public GetListFilterNotificationByCurrentUserIdQuery(NotificationFilterParam filterParams) : base(filterParams)
        {
        }

    }
    public class GetListFilterNotificationByCurrentUserIdQueryHandler : IQueryHandler<GetListFilterNotificationByCurrentUserIdQuery, NotificationFilterResult>
    {
        private readonly PlanningContext _context;

        public GetListFilterNotificationByCurrentUserIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<NotificationFilterResult> Handle(GetListFilterNotificationByCurrentUserIdQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var userNames = _context.Users.Where(i => i.Id.Equals(@params.UserName)).ToListAsync(cancellationToken);
            var notifications = _context.Notifications.Where(i => i.UserIds.Any(x=>x.Equals(userNames)) && i.IsSend == true);
            //var f = notifications.ToList();            
            //&&i.NotificationType==NotificationType.
            var skip = (@params.PageId - 1) * @params.Take;
            var model = new NotificationFilterResult()
            {
                Data = await notifications.Skip(skip).Take(@params.Take).Select(s => s.MapFilter(_context)!)
                    .ToListAsync(cancellationToken),
                FilterParams = @params
            };
            model.GeneratePaging(notifications, @params.Take, @params.PageId);
            return model;
        }
    }
}
