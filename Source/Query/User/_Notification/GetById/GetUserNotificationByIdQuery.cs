using Common.Query;
using Domain.UserAgg;
using Domain.UserAgg.Repository;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User._Notification.DTOs;
using System.Security.Cryptography;

namespace Query.User._Notification.GetById
{
    public class GetUserNotificationByIdQuery : IQuery<UserNotificationDto?>
    {
        public long UserNotificationId { get; set; }
        public required string UserId { get; set; }
    }

    internal class GetUserNotificationByIdQueryHandler :
        IQueryHandler<GetUserNotificationByIdQuery, UserNotificationDto?>
    {
        private readonly PlanningContext _context;

        public GetUserNotificationByIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<UserNotificationDto?> Handle(GetUserNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.
                FirstOrDefaultAsync(user => user.Id.Equals(request.UserId));

            if (user == null)
                return null;

            var userNotification = user.UserNotifications.
                FirstOrDefault(userNotification => userNotification.Id == request.UserNotificationId);

            if (userNotification == null)
                return null;

            return new UserNotificationDto
            {
                CreationDate = userNotification.CreationDate,
                Description = userNotification.Description,
                Id = userNotification.Id,
                IsActive = userNotification.IsActive,
                IsSeen = userNotification.IsSeen,
                IsSend = userNotification.IsSend,
                SendType = userNotification.SendType,
                SendTime = userNotification.SendTime,
                Title = userNotification.Title,
                UserName = user.UserName ?? user.Name
            };
        }
    }
}
