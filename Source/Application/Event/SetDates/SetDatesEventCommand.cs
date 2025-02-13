using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;
using Domain.EventAgg.Repository;

namespace Application.Event.SetDates
{
    public class SetDatesEventCommand : IBaseCommand
    {
        public long Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
    internal class SetDatesEventCommandHandler : IBaseCommandHandler<SetDatesEventCommand>
    {
        public IEventRepository _repository { get; set; }
        public SetDatesEventCommandHandler(IEventRepository repository)
        {
            _repository = repository;
        }

  
        public async Task<OperationResult> Handle(SetDatesEventCommand request, CancellationToken cancellationToken)
        {
            var eventClass = await _repository.GetTracking(request.Id);
            if(eventClass == null)
                return OperationResult.NotFound();
            eventClass.SetDates(request.StartTime, request.EndTime);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
