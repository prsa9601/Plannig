using Application.Event.Add;
using Application.Event.Delete;
using Application.Event.Edit;
using Application.Event.SetDates;
using Application.User.Delete;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planning.Api.Model.Event;
using Presentation.Facade.Event;
using Query.Event.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ApiController
    {
        private readonly IEventFacade _facade;
        public EventController(IEventFacade facade)
        {
            _facade = facade;
        }
        // GET: api/<EventController>
        [Authorize]
        [HttpPost]
        public async Task<ApiResult> Add([FromBody] AddEventCommandViewModel command)
        {
            var q = User.GetUserName();
            var qq = User.GetPhoneNumber();
            var result = await _facade.AddEvent(new AddEventCommand()
            {
                accessNotification = command.accessNotification,
                creatorUserName = User.GetUserName(),
                Description = command.Description,
                EndTime = command.EndTime,
                EventAddress = command.EventAddress,
                Link = command.Link,
                notification = command.notification,
                StartTime = command.StartTime,
                tag = command.tag,
                Title = command.Title,
                userNames = command.userNames
            });
            return CommandResult(result);
        }

        // GET api/<EventController>/5
        [HttpGet("{id}")]
        public async Task<ApiResult<EventDto?>> Get(long id)
        {
            var result = await _facade.GetEventById(id);
            return QueryResult(result);
        }
        [Authorize]
        [HttpPatch]
        public async Task<ApiResult> Edit([FromBody] EditEventCommandViewModel command)
        {
            var result = await _facade.EditEvent(new EditEventCommand()
            {
                accessNotification = command.accessNotification,
                creatorUserName = User.GetUserName(),
                Description = command.Description,
                EndTime = command.EndTime,
                EventAddress = command.EventAddress,
                Link = command.Link,
                notification = command.notification,
                StartTime = command.StartTime,
                tag = command.tag,
                Title = command.Title,
                Id = command.Id,
                userNames = command.userNames
            });
            return CommandResult(result);
        }
        [Authorize]
        [HttpGet("GetByUserId")]
        public async Task<ApiResult<List<EventDto?>>> GetByUserId()
        {
            var result = await _facade.GetEventsByUserId(User.GetUserIdToString());
            return QueryResult(result);
        }
        [Authorize]
        [HttpPatch("SetDates")]
        public async Task<ApiResult> GetByUserId(SetDatesEventCommand command)
        {
            var result = await _facade.SetDatesEvent(command);
            return CommandResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(long id)
        {
            var result = await _facade.DeleteEvent(new DeleteEventCommand()
            {
                Id = id
            });
            return CommandResult(result);
        }


        // POST api/<EventController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<EventController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<EventController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
