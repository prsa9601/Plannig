using Common.Application;
using Domain.UserAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User._Notification.MarkAsRead
{
    public class MarkUserNotificationAsReadCommand : IBaseCommand
    {
        public long UserNotificationId { get; set; }
        public string UserId { get; set; }
    }
    internal class MarkUserNotificationAsReadCommandHandler : IBaseCommandHandler<MarkUserNotificationAsReadCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public MarkUserNotificationAsReadCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(MarkUserNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingWithString(request.UserId);

            if (user == null)
                return OperationResult.NotFound();

            user.MarkNotification(request.UserNotificationId);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
