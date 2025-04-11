using Application.Comment.ChangeStatus;
using Application.Comment.Create;
using Application.Comment.Edit;
using Application.Comment.Remove;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Facade.Comment;
using Query.Comment.DTOs;

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ApiController
    {
        private readonly ICommentFacade _facade;

        public CommentController(ICommentFacade facade)
        {
            _facade = facade;
        }

        [Authorize]
        [HttpPost("CreateComment")]
        public async Task<ApiResult> CreateComment(CreateCommentCommand command)
        {
            return CommandResult(await _facade.Create(command));
        }
        [Authorize]
        [HttpPatch("EditComment")]
        public async Task<ApiResult> EditComment(EditCommentCommand command)
        {
            return CommandResult(await _facade.Edit(command));
        }
        [Authorize]
        [HttpPatch("ChangeCommentStatus")]
        public async Task<ApiResult> ChangeCommentStatus(ChangeStatusCommentCommand command)
        {
            return CommandResult(await _facade.ChangeStatus(command));
        }
        [Authorize]
        [HttpDelete("DeleteComment")]
        public async Task<ApiResult> DeleteComment(long CommentId)
        {
            return CommandResult(await _facade.Remove(new DeleteCommentCommand(CommentId)));
        }
        [Authorize]
        [HttpGet("GetCommentById")]
        public async Task<ApiResult<CommentDto?>> GetCommentById(long CommentId)
        {
            return QueryResult(await _facade.GetCommentById(CommentId));
        }
        [Authorize]
        [HttpGet("GetCommentByFilter")]
        public async Task<ApiResult<CommentFilterResult?>> GetCommentByFilter([FromQuery]CommentFilterParam param)
        {
            return QueryResult(await _facade.GetCommentByFilter(param));
        }
    }
}
