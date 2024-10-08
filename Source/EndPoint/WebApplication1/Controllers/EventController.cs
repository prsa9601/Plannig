using Application.Event.Add;
using Application.Event.Delete;
using Application.Event.Edit;
using Application.User.Delete;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        public async Task<ApiResult> Add([FromForm]AddEventCommand command)
        {
            var result = await _facade.AddEvent(command);
            return CommandResult(result);
        }

        // GET api/<EventController>/5
        [HttpGet("{id}")]
        public async Task<ApiResult<EventDto?>> Get(long id)
        {
            var result = await _facade.GetEventById(id);
            return QueryResult(result);
        }

        [HttpPatch]
        public async Task<ApiResult> Edit([FromForm]EditEventCommand command)
        {
            var result = await _facade.EditEvent(command);
            return CommandResult(result);
        }

        [HttpGet("GetByUserId{Id}")]
        public async Task<ApiResult<List<EventDto>?>> GetByUserId(string Id)
        {
            var result = await _facade.GetEventsByUserId(Id);
            return QueryResult(result);
        }
        [HttpDelete]
        public async Task<ApiResult> Delete(DeleteEventCommand command)
        {
            var result = await _facade.DeleteEvent(command);
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
