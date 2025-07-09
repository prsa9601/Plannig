using Common.Application;
using Domain.NotificationAgg.Repository;
using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notification.RemoveAll
{
    public class RemoveAllNotificationCommand : IBaseCommand
    {
        public required string UserId { get; set; }
    }
    internal class RemoveAllNotificationCommandHandler : IBaseCommandHandler<RemoveAllNotificationCommand>
    {
        private readonly INotificationRepository _repository;
        private readonly ILogger _logger;

        public RemoveAllNotificationCommandHandler(ILogger logger, INotificationRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveAllNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _repository.GetListByFilterAsync(
                i => i.UserIds.Any(x => x.Equals(request.UserId)));
            //var result = await _repository.Delete(i => i.user);
            //return (result) ? OperationResult.Success() : OperationResult.NotFound();
            if (notification == null)
                return OperationResult.NotFound();
            foreach (var item in notification)
            {
                item.UserIds!.Remove(request.UserId);
            }

            //bool deleteScheduleResult = false;
            //var DeleteScheduleResult = false;
            //if (result && notification!.ScheduleId != null)
            //{
            //    deleteScheduleResult = BackgroundJob.Delete(notification.ScheduleId);
            //    DeleteScheduleResult = BackgroundJob.Delete(notification.NotificationScheduleId);
            //}
            await _repository.Save();
            //if (result && deleteScheduleResult)
            //    return OperationResult.Success("نوتیفیکیشن با موفقیت حذف شد.");
            //else if (result && !deleteScheduleResult)
            //    _logger.LogError("نوتیفیکیشن از هنگفایر حذف نشد!");
            //if (!result || !deleteScheduleResult || !DeleteScheduleResult)
            //    _logger.LogError("نوتیفیکیشن از دیتابیس حذف نشد!");

            return OperationResult.Success();
        }
    }
}
