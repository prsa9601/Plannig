using Common.Application;
using Domain.UserAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User._Notification.RemoveForUser
{
    public class RemoveUserNotificationForUserCommand : IBaseCommand
    {
        public required string UserId { get; set; }
        public long UserNotificationId { get; set; }
    }
    internal class RemoveUserNotificationForUserCommandHandler : IBaseCommandHandler<RemoveUserNotificationForUserCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public RemoveUserNotificationForUserCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveUserNotificationForUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingWithString(request.UserId);

            if (user == null)
                return OperationResult.NotFound();

            if (user.UserNotifications.Any(i => i.Id == request.UserNotificationId
            && i.UserIds.Any(x => x.Equals(request.UserId))))
            {
                var notification = user.UserNotifications.FirstOrDefault(i => i.Id ==
                request.UserNotificationId && i.UserIds.Any(x => x.Equals(request.UserId)));
                if (notification == null)
                    return OperationResult.NotFound();

                notification.UserIds.Remove(request.UserId);
            }
            else
            {
                var Users = await _repository.GetAllUser();
                if (Users == null)
                    return OperationResult.NotFound();
                foreach (var item in Users)
                {
                    if (item.UserNotifications.Any(i => i.Id == request.UserNotificationId
                    && i.UserIds.Any(x => x.Equals(request.UserId))))
                    {
                        var notification = item.UserNotifications.FirstOrDefault(i => i.Id ==
                        request.UserNotificationId && i.UserIds.Any(x => x.Equals(request.UserId)));
                        if (notification == null)
                            return OperationResult.NotFound();

                        notification.UserIds.Remove(request.UserId);
                    }
                }
            }
            //user.RemoveUserNotification(request.UserNotificationId);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
