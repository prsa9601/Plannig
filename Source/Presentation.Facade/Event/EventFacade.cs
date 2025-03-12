using Application.Event.Add;
using Application.Event.Delete;
using Application.Event.Edit;
using Application.Event.SetDates;
using Common.Application;
using MediatR;
using Query.Event.DTOs;
using Query.Event.GetById;
using Query.Event.GetByUserId;

namespace Presentation.Facade.Event
{
    public class EventFacade : IEventFacade
    {
        private readonly IMediator _mediator;

        public EventFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult<long>> AddEvent(AddEventCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult<long>> EditEvent(EditEventCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SetDatesEvent(SetDatesEventCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> DeleteEvent(DeleteEventCommand command)
        {
            return await _mediator.Send(command);
        }

 

      

        public async Task<EventDto?> GetEventById(long id)
        {
            return await _mediator.Send(new GetEventByIdQuery(id));
        }

  

        public async Task<List<EventDto?>> GetEventsByUserId(string userId)
        {
            return await _mediator.Send(new GetEventByUserIdQuery(userId));
        }
    }
}