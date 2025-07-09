using Common.Application;
using Domain.NotificationAgg.Repository;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Application.Notification.Remove
{
    public class RemoveNotificationCommand : IBaseCommand
    {
        //public string ScheduleId { get; set; }
        public long  EventId { get; set; }
    }
    public class RemoveNotificationCommandHandler : IBaseCommandHandler<RemoveNotificationCommand>
    {
        private readonly INotificationRepository _repository;
        private readonly ILogger _logger;

        public RemoveNotificationCommandHandler(INotificationRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<OperationResult> Handle(RemoveNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notification = await _repository.GetTracking(request.EventId);
                var result = await _repository.Delete(i => i.EventId == request.EventId);
                //return (result) ? OperationResult.Success() : OperationResult.NotFound();
                if (!result)
                    return OperationResult.NotFound();
                bool deleteScheduleResult = false;
                var DeleteScheduleResult = false;
                if (result && notification!.ScheduleId != null)
                {
                  deleteScheduleResult = BackgroundJob.Delete(notification.ScheduleId);
                  DeleteScheduleResult = BackgroundJob.Delete(notification.NotificationScheduleId);
                }
                await _repository.Save();
                //if (result && deleteScheduleResult)
                //    return OperationResult.Success("نوتیفیکیشن با موفقیت حذف شد.");
                //else if (result && !deleteScheduleResult)
                //    _logger.LogError("نوتیفیکیشن از هنگفایر حذف نشد!");
                if (!result || !deleteScheduleResult || !DeleteScheduleResult)
                    _logger.LogError("نوتیفیکیشن از دیتابیس حذف نشد!");

                return OperationResult.Success();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
