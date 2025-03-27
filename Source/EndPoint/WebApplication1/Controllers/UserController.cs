using Application.User._Friend.Add;
using Application.User._Friend.Remove;
using Application.User.Edit;
using Application.User.SetEvent;
using Common.AspNetCore;
using Domain.UserAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Presentation.Facade.User;
using Query.User.DTOs;
using System.Security.Claims;
using Planning.Api.Model;
using Application.User.SetRole;
using Application.User.ChangeActivityUserStatus;
using Application.User.ChangeEmailConfirmedStatus;
using Application.User.ChangePhoneNumberConfirmedStatus;
using Application.User.EditForAdmin;
using Application.User.SetAvatar;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiController
    {
        private readonly IUserFacade _facade;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMemoryCache _memoryCache;

        public UserController(IUserFacade facade, SignInManager<User> signInManager, UserManager<User> userManager, IMemoryCache memoryCache)
        {
            _facade = facade;
            _signInManager = signInManager;
            _userManager = userManager;
            _memoryCache = memoryCache;
        }


        [HttpGet]
        [Authorize]
        public async Task<ApiResult<UserDto?>> GetCurrentUser()
        {
            try
            {
                //var result = await _facade.GetCurrentUser(User.Identity.GetEmail());
                var result = await _facade.GetUserByUserName(User.Identity.Name);
                return QueryResult(result);
                // _memoryCache.Remove("UsernameCacheKey");

                // var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GetUserByPhoneNumber/{phoneNumber}")]
        public async Task<ApiResult<UserDto?>> GetCurrentByPhoneNumber(string phoneNumber)
        {
            try
            {
                //var result = await _facade.GetCurrentUser(User.Identity.GetEmail());
                var result = await _facade.GetUserByPhoneNumber(phoneNumber);
                return QueryResult(result);
                // _memoryCache.Remove("UsernameCacheKey");

                // var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("searchUser")]
        [Authorize]
        public async Task<ApiResult<UserFilterResult>> SearchUser([FromQuery] UserFilterParam param)
        {
            try
            {
                var result = await _facade.SearchUser(new UserFilterParam()
                {
                    PageId = param.PageId,
                    UserName = param.UserName,
                    Take = param.Take,
                    CurrentUserId = User.GetUserIdToString()
                });
                return QueryResult(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPatch("setRole")]
        public async Task<ApiResult> SetRole(SetUserRoleCommand command)
        {
            var result = await _facade.SetRole(command);
            return CommandResult(result);
        }
        [HttpGet("GetUsersForAdmin")]
        [Authorize]
        public async Task<ApiResult<UserFilterResultForAdmin>> GetUsersForAdmin([FromQuery] UserFilterParamForAdmin param)
        {
            try
            {
                var result = await _facade.GetUsersForAdmin(new UserFilterParamForAdmin()
                {
                    PageId = param.PageId,
                    UserName = param.UserName,
                    Take = param.Take,
                    Email = param.Email,
                    Family = param.Family,
                    Name = param.Name,
                    PhoneNumber = param.PhoneNumber,
                    ActivePackage = param.ActivePackage,
                });
                return QueryResult(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("GetUserByUserName/{userName}")]
        public async Task<ApiResult<UserDto?>> GetCurrentByUserName(string userName)
        {
            try
            {

                //var result = await _facade.GetCurrentUser(User.Identity.GetEmail());
                var result = await _facade.GetUserByUserName(userName);
                return QueryResult(result);
                // _memoryCache.Remove("UsernameCacheKey");

                // var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetById/{Id}")]
        [Authorize]
        public async Task<ApiResult<UserDto?>> GetUserById(string Id)
        {
            var result = await _facade.GetUserById(Id);
            return QueryResult(result);
        }
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [Authorize]
        [HttpPost("SetEvent")]
        public async Task<ApiResult> SetEvent([FromBody] List<long> EventId)
        {
            var result = await _facade.SetEvent(new SetUserEventCommand(EventId, User.GetUserIdToString()));
            return CommandResult(result);
        }

        [Authorize]
        [HttpPost("ChangeActivityStatusForAdmin")]
        public async Task<ApiResult> ChangeActivityStatusForAdmin
            (ChangeActivityUserStatusCommand command)
        {
            var result = await _facade.ChangeActivityStatusUser
                (new ChangeActivityUserStatusCommand
                {
                    UserId = command.UserId,
                    IsActive = command.IsActive
                });
            return CommandResult(result);
        }
        [Authorize]
        [HttpPost("ChangeEmailConfirmedStatusForAdmin")]
        public async Task<ApiResult> ChangeEmailConfirmedStatusForAdmin
            (ChangeEmailConfirmedUserStatusCommand command)
        {
            var result = await _facade.ChangeEmailConfirmedUserStatus
                (new ChangeEmailConfirmedUserStatusCommand
                {
                    UserId = command.UserId,
                    EmailConfirmed = command.EmailConfirmed
                });
            return CommandResult(result);
        }

        [Authorize]
        [HttpPost("ChangePhoneNumberConfirmedStatusForAdmin")]
        public async Task<ApiResult> ChangePhoneNumberConfirmedStatusForAdmin
            (ChangePhoneNumberConfirmedStatusCommand command)
        {
            var result = await _facade.ChangePhoneNumberConfirmedStatus
                (new ChangePhoneNumberConfirmedStatusCommand
                {
                    UserId = command.UserId,
                    PhoneNumberConfirmed = command.PhoneNumberConfirmed
                });
            return CommandResult(result);
        }


        [Authorize]
        [HttpPost("ChangeActivityStatus")]
        public async Task<ApiResult> ChangeActivityStatus()
        {
            var result = await _facade.ChangeActivityStatusUser
                (new ChangeActivityUserStatusCommand
                {
                    UserId = User.GetUserIdToString()
                });
            return CommandResult(result);
        }
        [Authorize]
        [HttpPost("ChangeEmailConfirmedStatus")]
        public async Task<ApiResult> ChangeEmailConfirmedStatus()
        {
            var result = await _facade.ChangeEmailConfirmedUserStatus
                (new ChangeEmailConfirmedUserStatusCommand
                {
                    UserId = User.GetUserIdToString()
                });
            return CommandResult(result);
        }

        [Authorize]
        [HttpPost("ChangePhoneNumberConfirmedStatus")]
        public async Task<ApiResult> ChangePhoneNumberConfirmedStatus()
        {
            var result = await _facade.ChangePhoneNumberConfirmedStatus
                (new ChangePhoneNumberConfirmedStatusCommand
                {
                    UserId = User.GetUserIdToString()
                });
            return CommandResult(result);
        }



        [HttpPost("AddFriend{FriendId}")]
        public async Task<ApiResult> AddFriend(string friendUserName)
        {
            var id = User.GetUserIdToString();

            //var user = await _facade.GetUserByUserName(User.Identity.Name);
            var result = await _facade.AddFriend(new AddFriendsUserCommand(friendUserName, User.GetUserIdToString()));
            return CommandResult(result);
        }

        //[HttpDelete("RemoveFriend/{FriendNumber}")]
        //public async Task<ApiResult> RemoveFriend(long id)
        //{
        //    //var user = await _facade.GetUserByUserName(User.Identity.Name);
        //    var result = await _facade.RemoveFriend(new RemoveFriendUserCommand(FriendNumber, user.Id));
        //    return CommandResult(result);
        //}

        [Authorize]
        [HttpPut]
        public async Task<ApiResult> Put([FromBody] EditUserViewModel command)
        {
            //var id = User.GetUserIdToString();
            var result = await _facade.EditUser(new EditUserCommand()
            {
                Id = User.GetUserIdToString(),
                Name = command.Name,
                Family = command.Family,
                userName = command.userName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber
            });
            return CommandResult(result);
        }
        [Authorize]
        [HttpPut("EditUserForAdmin")]
        public async Task<ApiResult> EditForAdmin([FromBody] EditUserForAdminCommand command)
        {
            //var id = User.GetUserIdToString();
            var result = await _facade.EditUserForAdmin(new EditUserForAdminCommand()
            {
                Id = command.Id,
                Name = command.Name,
                Family = command.Family,
                userName = command.userName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                IsActive = command.IsActive
            });
            return CommandResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult> Delete(string id)
        {
            var result = await _facade.Delete(new Application.User.Delete.DeleteUserCommand() { Id = id });
            return CommandResult(result);
        }
        [HttpPatch("SetAvatar")]
        public async Task<ApiResult> SetAvatar(SetAvatarCommand command)
        {
           
            var result = await _facade.SetAvatar(new Application.User.SetAvatar.SetAvatarCommand()
            {
                Avatar = command.Avatar,
                UserName = command.UserName
            });
            return CommandResult(result);
        }

    }
}
