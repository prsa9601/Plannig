using Application.SocialMedia.Telegram.Post.AddImageToPost;
using Application.SocialMedia.Telegram.Post.AddPost;
using Application.SocialMedia.Telegram.Post.DeletePost;
using Application.SocialMedia.Telegram.Post.EditPost;
using Application.SocialMedia.Telegram.Post.RemoveImageToPost;
using Application.SocialMedia.Telegram.Post.SendMessageToTelegram;
using Application.SocialMedia.Telegram.Post.SendPictureToTelegram;
using Application.SocialMedia.Telegram.Post.SendVideoToTelegram;
using Application.SocialMedia.Telegram.Post.SetImageToPost;
using Common.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Presentation.Facade.Telegram;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : ApiController
    {
        private readonly ITelegramFacade _facade;

        public TelegramController(ITelegramFacade facade)
        {
            _facade = facade;
        }

        [HttpPost]
        public async Task<ApiResult> Add(AddPostCommand command)
        {
            var result = await _facade.Add(command);
            return CommandResult(result);
        }
        [HttpPatch]
        public async Task<ApiResult> Edit(EditPostCommand command)
        {
            var result = await _facade.Edit(command);
            return CommandResult(result);
        }
        [HttpDelete]
        public async Task<ApiResult> Delete([FromQuery]DeletePostCommand command)
        {
            var result = await _facade.Delete(command);
            return CommandResult(result);
        }
        [HttpPatch("SetImage")]
        public async Task<ApiResult> Add(SetImageCommand command)
        {
            var result = await _facade.SetImage(command);
            return CommandResult(result);
        }
        [HttpPost("AddImage")]
        public async Task<ApiResult> Edit(AddImageCommand command)
        {
            var result = await _facade.AddImage(command);
            return CommandResult(result);
        }
        [HttpDelete("RemoveImage")]
        public async Task<ApiResult> Delete([FromQuery]RemoveImagePostCommand command)
        {
            var result = await _facade.RemoveImage(command);
            return CommandResult(result);
        }
        [HttpPost("SendMessage")]
        public async Task<ApiResult> SendMessage(SendMessageToTelegramCommand command)
        {
            var result = await _facade.SendMessageToTelegram(command);
            return CommandResult(result);
        }      
        [HttpPost("SendImage")]
        public async Task<ApiResult> SendImage(SendImageToTelegramCommand command)
        {
            var result = await _facade.SendImageToTelegram(command);
            return CommandResult(result);
        }
        [HttpPost("SendVideo")]
        public async Task<ApiResult> SendVideo(SendVideoToTelegramCommand command)
        {
            var result = await _facade.SendVideoToTelegram(command);
            return CommandResult(result);
        }
    }
}
