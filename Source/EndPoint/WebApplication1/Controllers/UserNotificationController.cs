using Application.User._Notification.Admin.Add;
using Application.User._Notification.Admin.Remove;
using Application.User._Notification.Admin.RemoveAll;
using Application.User._Notification.MarkAsRead;
using Application.User._Notification.RemoveAllForUser;
using Application.User._Notification.RemoveForUser;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Planning.Api.Model.UserNotification;
using Presentation.Facade.User.Notification;
using Query.User._Notification.DTOs;

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNotificationController : ApiController
    {
        private readonly IUserNotificationFacade _facade;

        public UserNotificationController(IUserNotificationFacade facade)
        {
            _facade = facade;
        }

        [Authorize]
        [HttpPost("Add")]
        public async Task<ApiResult> AddUserNotification(AddUserNotificationCommandViewModel command)
        {
            var date = DateTime.Now;
            return CommandResult(await _facade.Add(new AddUserNotificationCommand
            {
                Description = command.Description,
                IsActive = command.IsActive,
                NotificationType = command.NotificationType,
                SendTime = command.SendTime,
                SendToAllUser = command.SendToAllUser,
                Title = command.Title,
                UserId = User.GetUserIdToString(),
                UserIds = command.UserIds,
            }));
        }
        [Authorize]
        [HttpPatch("MarkUserNotificationAsRead")]
        public async Task<ApiResult> MarkAsRead(MarkAsReadUserNotificationViewModel command)
        {
            return CommandResult(await _facade.MarkAsRead(new MarkUserNotificationAsReadCommand
            {
               UserNotificationId = command.UserNotificationId,
               UserId=User.GetUserIdToString(),
            }));
        }

        [Authorize]
        [HttpDelete("Remove")]
        public async Task<ApiResult> RemoveUserNotification(long UserNotificationId)
        {
            return CommandResult(await _facade.Remove(new RemoveUserNotificationCommand
            {
                UserId = User.GetUserIdToString(),
                UserNotificationId = UserNotificationId
            }));
        }

        [Authorize]
        [HttpDelete("RemoveAll")]
        public async Task<ApiResult> RemoveAllUserNotifications()
        {
            return CommandResult(await _facade.RemoveAll(new RemoveAllUserNotificationCommand
            {
                UserId = User.GetUserIdToString(),
            }));
        }
        
        [Authorize]
        [HttpDelete("RemoveForUser")]
        public async Task<ApiResult> RemoveUserNotificationForUser(RemoveUserNotificationViewModel model)
        {
            return CommandResult(await _facade.RemoveForUser(new RemoveUserNotificationForUserCommand
            {
                UserId = User.GetUserIdToString(),
                UserNotificationId = model.UserNotificationId
            }));
        }

        [Authorize]
        [HttpDelete("RemoveAllForUser")]
        public async Task<ApiResult> RemoveAllUserNotificationsForUser()
        {
            return CommandResult(await _facade.RemoveAllForUser(new RemoveAllUserNotificationForUserCommand
            {
                UserId = User.GetUserIdToString(),
            }));
        }

        [Authorize]
        [HttpGet("GetById")]
        public async Task<ApiResult<UserNotificationDto?>> GetUserNotificationById(long UserNotificationId)
        {
            return QueryResult(await _facade.GetById
                (UserNotificationId, User.GetUserIdToString()));
        }

        [Authorize]
        [HttpGet("GetFilter")]
        public async Task<ApiResult<UserNotificationFilterResult>> GetUserNotificationFilter
            ([FromQuery] UserNotificationFilterParamViewModel param)
        {
            return QueryResult(await _facade.GetByFilter
                (new UserNotificationFilterParam
                {
                    UserId = User.GetUserIdToString(),
                    PageId = param.PageId,
                    Take = param.Take,
                    Search = param.Search,
                }));
        }
        
        [Authorize]
        [HttpGet("GetFilterForLayout")]
        public async Task<ApiResult<UserNotificationFilterResult>> GetUserNotificationFilterForLayout
            ([FromQuery] UserNotificationFilterParamViewModel param)
        {
            return QueryResult(await _facade.GetByFilterForLayout
                (new UserNotificationFilterParam
                {
                    UserId = User.GetUserIdToString(),
                    PageId = param.PageId,
                    Take = param.Take,
                    Search = param.Search,
                }));
        }

        [Authorize]
        [HttpGet("GetFilterForAdmin")]
        public async Task<ApiResult<UserNotificationFilterResultForAdmin>> GetUserNotificationFilterForAdmin
            ([FromQuery] UserNotificationFilterParamForAdmin param)
        {
            return QueryResult(await _facade.GetByFilterForAdmin
                (param));
        }
        
        [Authorize]
        [HttpGet("GetInformationUserForAdmin")]
        public async Task<ApiResult<Dictionary<string, string>>> GetUserNotificationFilterForAdmin()
        {
            return QueryResult(await _facade.GetUserNamesForAdmin());
        }

    }
}
