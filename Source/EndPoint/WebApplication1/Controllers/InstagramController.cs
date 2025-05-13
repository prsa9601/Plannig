using Application.SocialMedia.Instagram.Account.Add;
using Application.SocialMedia.Instagram.Account.Delete;
using Application.SocialMedia.Instagram.Account.Edit;
using Application.SocialMedia.Instagram.Account.SetProfile;
using Application.SocialMedia.Instagram.Post.AddImageToPost;
using Application.SocialMedia.Instagram.Post.AddPost;
using Application.SocialMedia.Instagram.Post.DeletePost;
using Application.SocialMedia.Instagram.Post.EditPost;
using Application.SocialMedia.Instagram.Post.RemoveImageToPost;
using Application.SocialMedia.Instagram.Post.SetImageToPost;
using Application.SocialMedia.Instagram.Story.Add;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Facade.Instagram;
using Query.SocialMedia.Instagram.Account.DTOs;
using Query.SocialMedia.Instagram.Story.DTOs;
using static Planning.Api.Model.InstagramModel.InstagramViewModel;
using static Query.SocialMedia.Instagram.Post.DTOs.PostFilterData;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstagramController : ApiController
    {
        private readonly IInstagramFacade _facade;

        public InstagramController(IInstagramFacade facade)
        {
            _facade = facade;
        }

        [HttpPost("AddPost")]
        public async Task<ApiResult> AddPost([FromForm] AddPostInstagramViewModel command)
        {
            DateTime dateTime = DateTime.Parse(command.DateOfPosting);

            var result = await _facade.Add(new AddPostInstagramCommand()
            {
                DateOfPosting = dateTime,
                Description = command.Description,
                Videos = command.Videos,
                InstagramAccountId = command.InstagramAccountId,
                Link = command.Link,
                Images = command.Images
            });
            return CommandResult(result);
        }
        [HttpPost("AddStory")]
        [RequestSizeLimit(104857600)]
        public async Task<ApiResult> AddStory([FromForm] AddStoryCommand command)
        {
            var result = await _facade.AddStory(command);
            return CommandResult(result);
        }
        [HttpPost("SendStory")]
        public async Task<ApiResult> UploadStory([FromForm] Application.SocialMedia
            .Instagram.Story.SendToInstagram.SendToInstagramCommand command)
        {
            var result = await _facade.UploadStory(command);
            return CommandResult(result);
        }
        [HttpPatch("EditPost")]
        public async Task<ApiResult> EditPost([FromForm] EditPostInstagramCommand instagramCommand)
        {
            var result = await _facade.Edit(instagramCommand);
            return CommandResult(result);
        }
        [HttpDelete("DeletePost")]
        public async Task<ApiResult> DeletePost([FromQuery] DeletePostInstagramCommand instagramCommand)
        {
            var result = await _facade.Delete(instagramCommand);
            return CommandResult(result);
        }
        [HttpPatch("EditStory")]
        public async Task<ApiResult> EditStory([FromForm]Application.SocialMedia.Instagram
            .Story.Edit.EditStoryCommand command)
        {
            var result = await _facade.EditStory(command);
            return CommandResult(result);
        }
        [HttpDelete("DeleteStory")]
        public async Task<ApiResult> DeleteStory(Application.SocialMedia.Instagram
            .Story.Delete.DeleteStoryCommand command)
        {
            var result = await _facade.DeleteStory(command);
            return CommandResult(result);
        }
        [HttpPatch("SetImagePost")]
        public async Task<ApiResult> SetImage([FromForm] SetImageCommand command)
        {
            var result = await _facade.SetImage(command);
            return CommandResult(result);
        }
        [HttpPatch("AddImagePost")]
        public async Task<ApiResult> AddImage([FromForm] AddImageCommand command)
        {
            var result = await _facade.AddImage(command);
            return CommandResult(result);
        }
        [HttpDelete("RemoveImageToPost")]
        public async Task<ApiResult> DeleteImage(RemoveImagePostCommand command)
        {
            var result = await _facade.RemoveImage(command);
            return CommandResult(result);
        }
        [HttpGet("GetInstagramAccountById")]
        public async Task<ApiResult<InstagramAccountDto?>> GetById(long Id)
        {
            var result = await _facade.GetById(Id);
            return QueryResult(result);
        }
        [Authorize]
        [HttpGet("GetListInstagram")]
        public async Task<ApiResult<List<InstagramAccountDto>?>> GetList()
        {
            var result = await _facade.GetList(User.GetUserName());
            return QueryResult(result);
        }
        [HttpGet("GetInstagramByFilter")]
        [Authorize]
        public async Task<ApiResult<InstagramAccountFilterResult?>> GetInstagramByFilter(
            [FromQuery] InstagramAccountFilterParamViewModel filterParams)
        {
            var result = await _facade.GetByFilter(new InstagramAccountFilterParam
            {
                UserName = User.GetUserName(),
                EndTime = filterParams.EndTime,
                StartTime = filterParams.StartTime,
                InstagramUserName = filterParams.InstagramUserName,
                PageId = filterParams.PageId,
                Take = filterParams.Take,
                SearchOrderBy = filterParams.SearchOrderBy,
            });
            return QueryResult(result);
        }

        [Authorize]
        [HttpPost("AddInstagramAccount")]
        public async Task<ApiResult> AddAccount([FromForm] AddInstagramAccountCommandViewModel command)
        {
            var result = await _facade.AddAccount(new AddInstagramAccountCommand
            {
                UserId = User.GetUserIdToString(),
                accessToken = command.accessToken,
                Profile = command.Profile,
                InstagramUserName = command.InstagramUserName,
                UserName = User.GetUserName(),
            });
            return CommandResult(result);
        }
        [Authorize]
        [HttpPatch("EditInstagramAccount")]
        public async Task<ApiResult> EditAccount([FromForm] EditInstagramAccountCommand command)
        {
            var result = await _facade.EditAccount(command);
            return CommandResult(result);
        }
        [Authorize]
        [HttpPatch("SetProfileInstagramAccount")]
        public async Task<ApiResult> SetProfileAccount([FromForm] SetProfileInstagramAccountCommand command)
        {
            var result = await _facade.SetProfileAccount(command);
            return CommandResult(result);
        }
        [Authorize]
        [HttpDelete("DeleteInstagramAccount")]
        public async Task<ApiResult> DeleteAccount(long id)
        {
            var result = await _facade.DeleteAccount(new DeleteInstagramAccountCommand
            {
                Id = id
            });
            return CommandResult(result);
        }

        #region InstagramPost
        [HttpGet("GetInstagramPostByFilter")]
        [Authorize]
        public async Task<ApiResult<InstagramPostFilterResult?>> GetInstagramPostByFilter(
          [FromQuery] InstagramPostFilterParam filterParam)
        {
            var result = await _facade.GetInstagramPostByFilter(filterParam);
            return QueryResult(result);
        }
        #endregion

        #region InstagramStory
        [HttpGet("GetInstagramStoryByFilter")]
        [Authorize]
        public async Task<ApiResult<StoryFilterResult?>> GetInstagramStoryByFilter(
          [FromQuery] StoryFilterParam filterParams)
        {
            var result = await _facade.GetInstagramStoryByFilter(filterParams);
            return QueryResult(result);
        }
        #endregion
    }
}
