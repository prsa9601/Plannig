using Application.Blog.Add;
using Application.Blog.Edit;
using Application.Blog.IncreaseVisit;
using Application.Blog.Remove;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Facade.Blog;
using Query.Blog.DTOs;

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ApiController
    {
        private readonly IBlogFacade _facade;

        public BlogController(IBlogFacade facade)
        {
            _facade = facade;
        }

        [Authorize]
        [HttpPost("CreateBlog")]
        public async Task<ApiResult> CreateBlog(AddBlogCommand command)
        {
            return CommandResult(await _facade.Create(command));
        }
        [Authorize]
        [HttpPatch("EditBlog")]
        public async Task<ApiResult> EditBlog(EditBlogCommand command)
        {
            return CommandResult(await _facade.Edit(command));
        }
        [Authorize]
        [HttpPatch("IncreaseVisit")]
        public async Task<ApiResult> ChangeBlogStatus(IncreaseBlogVisitCommand command)
        {
            return CommandResult(await _facade.IncreaseVisit(command));
        }
        [Authorize]
        [HttpDelete("DeleteBlog")]
        public async Task<ApiResult> DeleteBlog(RemoveBlogCommand command)
        {
            return CommandResult(await _facade.Remove(command));
        }
        [Authorize]
        [HttpGet("GetBlogById")]
        public async Task<ApiResult<BlogDto?>> GetBlogById(long BlogId)
        {
            return QueryResult(await _facade.GetBlogById(BlogId));
        }
        [Authorize]
        [HttpGet("GetBlogBySlug")]
        public async Task<ApiResult<BlogDto?>> GetBlogBySlug(string Slug)
        {
            return QueryResult(await _facade.GetBlogBySlug(Slug));
        }
        [Authorize]
        [HttpGet("GetBlogByFilter")]
        public async Task<ApiResult<BlogFilterResult?>> GetBlogByFilter([FromQuery] BlogFilterParam param)
        {
            return QueryResult(await _facade.GetBlogByFilter(param));
        }
    }
}
