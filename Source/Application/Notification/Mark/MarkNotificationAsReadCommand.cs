using Common.Application;
using Domain.NotificationAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notification.Mark
{
    public class MarkNotificationAsReadCommand : IBaseCommand
    {
        public long NotificationId { get; set; }
        public string UserId { get; set; }
    }
    internal class MarkNotificationAsReadCommandHandler : IBaseCommandHandler<MarkNotificationAsReadCommand>
    {
        private readonly INotificationRepository _repository;

        public MarkNotificationAsReadCommandHandler(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            //var q = await _repository.GetListTrackingAsync();
            var notification = await _repository.GetByFilterAsync(i => i.Id.Equals
            (request.NotificationId) && i.UserIds!.Any(x => x.Equals(request.UserId)));
            if (notification == null)
                return OperationResult.NotFound();
            notification!.MarkAsSeen();
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
