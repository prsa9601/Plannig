using Common.Application;
using Domain.UserAgg.Repository;

namespace Application.User._Notification.Admin.Remove
{
    public class RemoveUserNotificationCommand : IBaseCommand
    {
        public required string UserId { get; set; }
        public long UserNotificationId { get; set; }
    }
    internal class RemoveUserNotificationCommandHandler : IBaseCommandHandler<RemoveUserNotificationCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public RemoveUserNotificationCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveUserNotificationCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingWithString(request.UserId);

            if (user == null)
                return OperationResult.NotFound();

            user.RemoveUserNotification(request.UserNotificationId);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
