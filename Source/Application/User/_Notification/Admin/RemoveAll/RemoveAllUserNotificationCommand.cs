using Common.Application;
using Domain.UserAgg.Repository;

namespace Application.User._Notification.Admin.RemoveAll
{
    public class RemoveAllUserNotificationCommand : IBaseCommand
    {
        public required string UserId { get; set; }
    }

    internal class RemoveAllUserNotificationCommandHandler : IBaseCommandHandler<RemoveAllUserNotificationCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public RemoveAllUserNotificationCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveAllUserNotificationCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingByUserName(request.UserId);

            if (user == null)
                return OperationResult.NotFound();

            user.ClearUserNotification();
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
