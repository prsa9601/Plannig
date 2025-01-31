using Application.User._Friend.Add;
using Application.User._Friend.Remove;
using Common.AspNetCore;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Planning.Api.Model.Friend;
using Presentation.Facade.User.Friend;
using Query.User._Friend.DTOs;
using Query.User._Friend.FilterFriendByUserNameForEventPage;
using Query.User.DTOs;

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
            var result = await _facade.GetFriendsByUserName(User.Identity.Name);
            return QueryResult(result);
        }
        [HttpGet("SearchFriendForEvent")]
        [Authorize]
        public async Task<ApiResult<SearchFriendForEventFilterResult>> SearchFriendForEvent([FromQuery]SearchFriendForEventFilterParamModel param)
        {
            var result = await _facade.SearchFriendForEvent(new SearchFriendForEventFilterParam()
            {
                CurrentUserId = User.GetUserIdToString(),
                PageId = param.PageId,
                Take = param.Take,
                UserName = param.UserName
            });
            return QueryResult(result);
        }
        [HttpGet("GetFriendsForProfile")]
        [Authorize]
        public async Task<ApiResult<FriendDtoForProfileResult?>> GetFriendsByUserName([FromQuery] FriendDtoForProfileParamViewModel param)
        {
            var result = await _facade.GetFriendFilterForProfileQuery(new FriendDtoForProfileParam()
            {
                CurrentUserId = User.GetUserIdToString(),
                PageId = param.PageId,
                Take = param.Take,
                UserName = param.UserName
            });
            return QueryResult(result);
        }
        [HttpGet("GetByUserId")]
        [Authorize]
        public async Task<ApiResult<List<FriendDto>?>> GetFriendsByUserId()
        {
            var result = await _facade.GetFriendsByUserId(User.GetUserIdToString());
            return QueryResult(result);
        }

        [HttpGet("searchUserForProfile")]
        [Authorize]
        public async Task<ApiResult<UserFriendFilterResult>> GetFriendsByUserIdForProfile([FromQuery] UserFriendFilterParam param)
        {
            var result = await _facade.GetFriendsByUserIdForProfile(new UserFriendFilterParam()
            {
                CurrentUserId = User.GetUserIdToString(),
                PageId = param.PageId,
                Take = param.Take,
                UserName = param.UserName
            });
            return QueryResult(result);
        }
    }
}
