using Application.SocialMedia.Instagram.Post.AddImageToPost;
using Application.SocialMedia.Instagram.Post.AddPost;
using Application.SocialMedia.Instagram.Post.DeletePost;
using Application.SocialMedia.Instagram.Post.EditPost;
using Application.SocialMedia.Instagram.Post.RemoveImageToPost;
using Application.SocialMedia.Instagram.Post.SendPostToInstagram;
using Application.SocialMedia.Instagram.Post.SetImageToPost;
using Application.SocialMedia.Instagram.Story.Add;
using Application.SocialMedia.Instagram.Story.Delete;
using Application.SocialMedia.Instagram.Story.Edit;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Presentation.Facade.Instagram;

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

        
        [HttpPost]
        public async Task<ApiResult> AddPost([FromBody] AddPostInstagramCommand instagramCommand)
        {
            var result = await _facade.Add(instagramCommand);
            return CommandResult(result);
        }
        [HttpPost("AddStory")]
        public async Task<ApiResult> UploadStory([FromBody] Application.SocialMedia
            .Instagram.Story.SendToInstagram.SendToInstagramCommand command)
        {
            var result = await _facade.UploadStory(command);
            return CommandResult(result);
        }
        [HttpPatch]
        public async Task<ApiResult> EditPost([FromBody] EditPostInstagramCommand instagramCommand)
        {
            var result = await _facade.Edit(instagramCommand);
            return CommandResult(result);
        }
        [HttpDelete]
        public async Task<ApiResult> DeletePost([FromQuery] DeletePostInstagramCommand instagramCommand)
        {
            var result = await _facade.Delete(instagramCommand);
            return CommandResult(result);
        }
        [HttpPatch("EditStory")]
        public async Task<ApiResult> EditStory(Application.SocialMedia.Instagram
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
        [HttpPatch("SetImage")]
        public async Task<ApiResult> SetImage(SetImageCommand command)
        {
            var result = await _facade.SetImage(command);
            return CommandResult(result);
        } 
        [HttpPost("AddImage")]
        public async Task<ApiResult> AddImage(AddImageCommand command)
        {
            var result = await _facade.AddImage(command);
            return CommandResult(result);
        } 
        [HttpDelete("RemoveImage")]
        public async Task<ApiResult> DeleteImage([FromQuery]RemoveImagePostCommand command)
        {
            var result = await _facade.RemoveImage(command);
            return CommandResult(result);
        }
    }
}
