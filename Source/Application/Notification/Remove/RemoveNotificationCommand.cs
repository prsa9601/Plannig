using Common.Application;
using Domain.Notification.Repository;

namespace Application.Notification.Remove
{
    public class RemoveNotificationCommand : IBaseCommand
    {
        //public string ScheduleId { get; set; }
        public long  EventId { get; set; }
    }
    internal class RemoveNotificationCommandHandler : IBaseCommandHandler<RemoveNotificationCommand>
    {
        private readonly INotificationRepository _repository;

        public RemoveNotificationCommandHandler(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.Delete(i => i.EventId == request.EventId);
                //return (result) ? OperationResult.Success() : OperationResult.NotFound();
                if (!result)
                    return OperationResult.NotFound();
                await _repository.Save();
                return OperationResult.Success("نوتیفیکیشن با موفقیت حذف شد.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
