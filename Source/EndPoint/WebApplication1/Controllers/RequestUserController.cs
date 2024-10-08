using Application.User._RequestBox.Add;
using Application.User._RequestBox.Remove;
using Common.AspNetCore;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Facade.User.Request;
using Query.User._RequestBox.DTOs;

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestUserController : ApiController
    {
        private readonly IRequestFacade _facade;

        public RequestUserController(IRequestFacade facade)
        {
            _facade = facade;
        }
        [Authorize]
        [HttpPost]
        public async Task<ApiResult> Add([FromBody] string ReceiverUserName)
        {
            var result = await _facade.AddRequest(new AddRequestFriendCommand()
            {
                userName = User.Identity.Name,
                userNameFriend = ReceiverUserName
            });
            return CommandResult(result);
        }
        [Authorize]
        [HttpDelete]
        public async Task<ApiResult> Remove([FromBody] string FriendUserName)
        {
            var result = await _facade.RemoveRequest(new RemoveRequestFriendCommand()
            {
                userName = User.Identity.Name,
                userNameFriend = FriendUserName
            });
            return CommandResult(result);
        }
        [HttpGet("GetRequestCurrentUser")]
        [Authorize]
        public async Task<ApiResult<List<RequestDto>?>> GetRequestsByUserName()
        {
            var result = await _facade.GetRequestList(User.Identity.Name);
            return QueryResult(result);
        }
        [Authorize]
        [HttpGet("GetById")]
        public async Task<ApiResult<RequestDto?>> GetRequestById(long id)
        {
            var result = await _facade.GetRequestById(id, User.Identity.Name);
            return QueryResult(result);
        }
        [Authorize]
        [HttpGet("GetFilter")]
        public async Task<ApiResult<RequestBoxFilterResult?>> GetRequestByFilter([FromQuery]RequestBoxFilterParam param)
        {
            var result = await _facade.GetRequestByFilter(new RequestBoxFilterParam()
            {
                PageId = param.PageId,
                Take = param.Take,
                filter = param.filter,
                UserName = User.Identity.Name
            });
            return QueryResult(result);
        }
    }
}
