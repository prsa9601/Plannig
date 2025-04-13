using Application.SocialMedia.Telegram.Account._RemoveAccount;
using Application.SocialMedia.Telegram.Account.CreateAccount;
using Application.SocialMedia.Telegram.Account.EditAccount;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Facade.Telegram;
using Presentation.Facade.Telegram.Account;
using Query.SocialMedia.Telegram.Account.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : ApiController
    {
        private readonly ITelegramFacade _facade;
        private readonly IAccountTelegramFacade _account;

        public TelegramController(ITelegramFacade facade, IAccountTelegramFacade account)
        {
            _facade = facade;
            _account = account;
        }

        [HttpPost("AddPost")]
        public async Task<ApiResult> Add([FromForm]AddPostCommand command)
        {
            var result = await _facade.Add(new AddPostCommand(command.TelegramId,
                 DateTime.Now, command.description, command.link, command.slug, command.Images,
                 command.Videos));
            return CommandResult(result);
        }
        [HttpPatch]
        public async Task<ApiResult> Edit([FromForm]EditPostCommand command)
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
        #region Account

        [Authorize]
        [HttpPost("CreateAccount")]
        public async Task<ApiResult> CreateAccount(CreateTelegramAccountCommand command)
        {
            return CommandResult(await _account.CreateAccount(command));
        }
        [Authorize]
        [HttpPatch("EditAccount")]
        public async Task<ApiResult> CreateAccount(EditTelegramAccountCommand command)
        {
            return CommandResult(await _account.EditAccount(command));
        }
        [Authorize]
        [HttpDelete("RemoveAccount")]
        public async Task<ApiResult> RemoveAccount(RemoveTelegramAccountCommand command)
        {
            return CommandResult(await _account.DeleteAccount(command));
        }
        [Authorize]
        [HttpGet("GetTelegramAccountById")]
        public async Task<ApiResult<TelegramAccountDto?>> GetTelegramAccountById(long TelegramAccountId)
        {
            return QueryResult(await _account.GetById(TelegramAccountId));
        }
        [Authorize]
        [HttpGet("GetListTelegramAccount")]
        public async Task<ApiResult<List<TelegramAccountDto?>>> GetListTelegramAccount(string UserName)
        {
            return QueryResult(await _account.GetList(UserName));
        }
        [Authorize]
        [HttpGet("GetByFilter")]
        public async Task<ApiResult<TelegramAccountFilterResult?>> GetListTelegramAccount([FromQuery]TelegramAccountFilterParam param)
        {
            return QueryResult(await _account.GetByFilter(param));
        }
        #endregion
    }
}
