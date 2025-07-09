using Common.Application;
using Domain.UserAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User._Notification.RemoveAllForUser
{
    public class RemoveAllUserNotificationForUserCommand : IBaseCommand
    {
        public required string UserId { get; set; }
    }
    internal class RemoveAllUserNotificationForUserCommandHandler : IBaseCommandHandler<RemoveAllUserNotificationForUserCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public RemoveAllUserNotificationForUserCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveAllUserNotificationForUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingWithString(request.UserId);

            if (user == null)
                return OperationResult.NotFound();

            if (user.UserNotifications.Any(i => i.UserIds.Any(x => x.Equals(request.UserId))))
            {
                var notification = user.UserNotifications.Where(i =>
                i.UserIds.Any(x => x.Equals(request.UserId)) && i.IsSend == true && i.IsSeen == true);
                if (notification == null)
                    return OperationResult.NotFound();
                foreach (var item in notification)
                {
                    item.UserIds.Remove(request.UserId);
                }
            }
            else
            {
                var Users = await _repository.GetAllUser();
                if (Users == null)
                    return OperationResult.NotFound();
                foreach (var item in Users)
                {
                    if (item.UserNotifications.Any(i =>
                    i.UserIds.Any(x => x.Equals(request.UserId))))
                    {
                        var notifications = item.UserNotifications.Where(i =>
                        i.UserIds.Any(x => x.Equals(request.UserId)) && i.IsSend == true && i.IsSeen == true).ToList();
                        if (notifications == null)
                            return OperationResult.NotFound();

                        notifications.ForEach(i => i.UserIds.Remove(request.UserId));
                    }
                }
            }
            //user.RemoveUserNotification(request.UserNotificationId);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
