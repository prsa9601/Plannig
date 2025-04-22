using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Notification.DTOs;

namespace Query.Notification.GetById
{
    public class GetNotificationByIdQuery : IQuery<NotificationDto?>
    {
        public required string UserName { get; set; }
        public required long NotificationId { get; set; }
    }
    internal class GetNotificationByIdQueryHandler : IQueryHandler<GetNotificationByIdQuery, NotificationDto?>
    {
        private readonly PlanningContext _context;

        public GetNotificationByIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<NotificationDto?> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notifications = await  _context.Notifications.Where(i => i.UserIds!.Equals(request.UserName)
            && i.IsSend == true && i.Id.Equals(request.NotificationId)).FirstOrDefaultAsync();
            return await notifications.Map(_context);
        }
    }
}
