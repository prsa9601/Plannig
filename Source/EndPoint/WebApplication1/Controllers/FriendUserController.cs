using Application.User._Friend.Add;
using Application.User._Friend.Remove;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Facade.User.Friend;
using Query.User._Friend.DTOs;

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendUserController : ApiController
    {
        private readonly IFriendFacade _facade;

        public FriendUserController(IFriendFacade facade)
        {
            _facade = facade;
        }
        [Authorize]
        [HttpPost]
        public async Task<ApiResult> Add([FromBody] string receiverUserName)
        {
            var result = await _facade.AddFriend(new AddFriendsUserCommand(receiverUserName, User.GetUserIdToString()));
            return CommandResult(result);
        }
        [Authorize]
        [HttpDelete]
        public async Task<ApiResult> Remove(string ReceiverUserName)
        {
            var result = await _facade.RemoveFriend(new RemoveFriendUserCommand(User.Identity.Name, ReceiverUserName));
            return CommandResult(result);
        }
        [HttpGet]
        [Authorize]
        public async Task<ApiResult<List<FriendDto>?>> GetFriendsByUserName()
        {
            var result = await _facade.GetFriendsByUserId(User.Identity.Name);
            return QueryResult(result);
        }
    }
}
