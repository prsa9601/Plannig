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
    public class DeleteEventCommand : IBaseCommand
    {
        public long Id { get; set; }
    }
    public class DeleteEventCommandHandler : IBaseCommandHandler<DeleteEventCommand>
    {
        private readonly IEventRepository _repository;
        private readonly EventScheduler _schedule;

        public DeleteEventCommandHandler(IEventRepository repository, EventScheduler schedule)
        {
            _repository = repository;
            _schedule = schedule;
        }

        public async Task<OperationResult> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            bool result = await _repository.Delete(i => i.Id == request.Id);
            if (!result)
                return OperationResult.Error("مشکلی در حذف پیش آمده!");
            _schedule.DeleteEvent(request.Id);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
