using Application.Event.Add;
using Application.Event.Delete;
using Application.Event.Edit;
using Application.Event.SetDates;
using Common.Application;
using Query.Event.DTOs;

namespace Presentation.Facade.Event
{
    public interface IEventFacade
    {
        Task<OperationResult<long>> AddEvent(AddEventCommand command);
        Task<OperationResult<long>> EditEvent(EditEventCommand command);
        Task<OperationResult> SetDatesEvent(SetDatesEventCommand command);
        Task<OperationResult> DeleteEvent(DeleteEventCommand command);


        Task<EventDto?> GetEventById(long id);
        Task<List<EventDto?>> GetEventsByUserId(string userId);
        
    }
}

