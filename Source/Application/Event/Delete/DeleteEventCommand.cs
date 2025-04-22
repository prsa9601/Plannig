using Common.Application;
using Domain.EventAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Schedule;

namespace Application.Event.Delete
{
    public class DeleteEventCommand : IBaseCommand<long>
    {
        public long Id { get; set; }
        public string UserName { get; set; }
    }
    public class DeleteEventCommandHandler : IBaseCommandHandler<DeleteEventCommand, long>
    {
        private readonly IEventRepository _repository;
        private readonly EventScheduler _schedule;

        public DeleteEventCommandHandler(IEventRepository repository, EventScheduler schedule)
        {
            _repository = repository;
            _schedule = schedule;
        }

        public async Task<OperationResult<long>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var Event = await _repository.GetTracking(request.Id);
            if (Event == null)
                return OperationResult<long>.NotFound();

            var eventUser = Event.EventUser.FirstOrDefault
                (i => i.CreatorUserId.Equals(request.UserName));
            if (eventUser == null)
            {

                Event.RemoveUserAsFromEvent(request.UserName);
                await _repository.Save();
                return OperationResult<long>.Success(0);

            }
            bool result = await _repository.DeleteAsync(Event);
            if (!result)
            {
                return OperationResult<long>.Error("ایونت با موفقیت انجام نشد!");
            }
            _schedule.DeleteEvent(request.Id);
            await _repository.Save();
            return OperationResult<long>.Success(Event.Id);
        }
    }
}
