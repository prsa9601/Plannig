using Application.Notification.Add;
using Application.Notification.ChangeDate;
using Application.Notification.Edit;
using Application.Notification.EmailSender;
using Application.Notification.Remove;
using Application.Notification.SmsSender;
using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planning.Api.Model.Notification;
using Presentation.Facade.Notification;
using Presentation.Facade.User;
using Query.Notification.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ApiController
    {
        private readonly INotificationFacade _facade;
        private readonly IUserFacade _userFacade;
        public NotificationController(INotificationFacade facade, IUserFacade userFacade)
        {
            _facade = facade;
            _userFacade = userFacade;
        }


        // POST api/<NotificationController>
        [HttpPost("SendEmail")]
        public async Task<ApiResult> SendEmail([FromBody] SendNotificationByEmailCommand command)
        {


            var result = await _facade.SendEmail(new SendNotificationByEmailCommand
            {
                startTime = command.startTime,
                EventId = command.EventId,
                notificationId = command.notificationId,
                userNames = command.userNames,
            });
            return CommandResult(result);
        }
        [HttpPost("AddNotification")]
        public async Task<ApiResult<long>> AddNotification([FromBody] AddNotificationViewModel command)
        {
            var user = await _userFacade.GetUserById(User.GetUserIdToString());
            var package = user.userPackageDto;
            var activePackage = package.Where(i => i.IsActive == true &&
                (i.ExpiryDate) < DateTime.Now).FirstOrDefault();
            //var emailCount = 0;
            //var smsCount = 0;
            //foreach (var item in activePackage) 
            //{
            //    emailCount += item.AllowedEmailCount;
            //    smsCount += item.AllowedSmsCount;
            //}
            //if (activePackage != null)
            //{
            var result = await _facade.AddNotification(new AddNotificationCommand
            {
                IsActive = true,
                //AllowedEmailCount = activePackage.AllowedEmailCount,
                //AllowedSmsCount = activePackage.AllowedSmsCount,
                creatorUserName = user.UserName,
                //EventExpiredTime = activePackage.CreationDate.Add(activePackage.ExpiryDate),
                EventId = command.EventId,
                EventStartTime = command.EventStartTime,
                IsSeen = false,
                IsSend = false,
                NotificationType = command.NotificationType,
                SendTime = command.SendTime,
                UserNames = command.UserIds
            });
            return CommandResult(result);
            //}
            //return CommandResult(OperationResult<long>.Error(403));

        }
        [HttpPatch("EditNotification")]
        public async Task<ApiResult> EditNotification([FromBody] EditNotificationViewModel command)
        {
            var result = await _facade.EditNotification(new EditNotificationCommand
            {
                // = ,
                IsActive = true,
                EventEndTime = command.EventEndTime,
                //AllowedEmailCount = activePackage.AllowedEmailCount,
                //AllowedSmsCount = activePackage.AllowedSmsCount,
                creatorUserName = User.GetUserName(),
                //EventExpiredTime = activePackage.CreationDate.Add(activePackage.ExpiryDate),
                EventId = command.EventId,
                EventStartTime = command.EventStartTime,
                IsSeen = false,
                IsSend = false,
                NotificationType = command.NotificationType,
                SendTime = command.SendTime,
                UserNames = command.UserNames
            });
            return CommandResult(result);
        }
        [HttpPatch("ChangeDateNotification")]
        public async Task<ApiResult> ChangeDateNotification([FromBody] ChangeDateNotificationCommand command)
        {
            var result = await _facade.ChangeDate(new ChangeDateNotificationCommand
            {
                EventId = command.EventId,
                SendTime = command.SendTime,
                StartTime = command.StartTime,
                EndTime = command.EndTime,
            });
            return CommandResult(result);
        }
        [HttpDelete("RemoveNotification/{eventId}")]
        public async Task<ApiResult> RemoveNotification(long eventId)
        {
            var result = await _facade.RemoveNotification(new RemoveNotificationCommand()
            {
                EventId = eventId,
                //ScheduleId = scheduleId
            });
            return CommandResult(result);
        }
        [HttpPost("SendSms")]
        public async Task<ApiResult> SendSms([FromBody] SendNotificationWithSms command)
        {
            var result = await _facade.SendSms(command);
            return CommandResult(result);
        }
        [Authorize]
        [HttpGet("GetFilterNotificationsCurrentUser")]
        public async Task<ApiResult<NotificationFilterResult?>> GetFilterNotificationsCurrentUser([FromQuery] NotificationFilterParamViewModel param)
        {
            var result = await _facade.GetFilterNotificationsCurrentUser(new Query.Notification.DTOs.NotificationFilterParam
            {
                UserName = User.GetUserName(),
                PageId = param.PageId,
                Take = param.Take
            });
            return QueryResult(result);
        }
        [Authorize]
        [HttpGet("GetByIdNotificationsCurrentUser&NotificationId={NotificationId}")]
        public async Task<ApiResult<NotificationDto?>> GetByIdNotificationsCurrentUser(long NotificationId)
        {
            var result = await _facade.GetByIdNotificationsCurrentUser(User.GetUserName(), NotificationId);
            return QueryResult(result);
        }
    }
}
