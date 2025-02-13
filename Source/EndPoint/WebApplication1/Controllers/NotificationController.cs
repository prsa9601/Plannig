using Application.Notification.EmailSender;
using Application.Notification.SmsSender;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Presentation.Facade.Notification;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ApiController
    {
        public NotificationController(INotificationFacade faaacade)
        {
            _faaacade = faaacade;
        }

        public INotificationFacade _faaacade { get; set; }
        // GET: api/<NotificationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<NotificationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NotificationController>
        [HttpPost]
        public async Task<ApiResult> Post([FromBody] SendNotificationByEmail command)
        {
            var result = await _faaacade.SendEmail(command);
            return CommandResult(result);
        }
        [HttpPost("SendSms")]
        public async Task<ApiResult> SendSms([FromBody] SendNotificationWithSms command)
        {
            var result = await _faaacade.SendSms(command);
            return CommandResult(result);
        }

        // PUT api/<NotificationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NotificationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
