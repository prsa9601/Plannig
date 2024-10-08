using Application.SocialMedia.Instagram.Story.SendToInstagram;
using Application.SocialMedia.Telegram.Post.AddImageToPost;
using Application.SocialMedia.Telegram.Post.AddPost;
using Application.SocialMedia.Telegram.Post.DeletePost;
using Application.SocialMedia.Telegram.Post.EditPost;
using Application.SocialMedia.Telegram.Post.RemoveImageToPost;
using Application.SocialMedia.Telegram.Post.SendMessageToTelegram;
using Application.SocialMedia.Telegram.Post.SendPictureToTelegram;
using Application.SocialMedia.Telegram.Post.SendVideoToTelegram;
using Application.SocialMedia.Telegram.Post.SetImageToPost;
using Common.Application;
using MediatR;

namespace Presentation.Facade.Telegram
{
    internal class TelegramFacade : ITelegramFacade
    {
        private readonly IMediator _mediator;

        public TelegramFacade(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<OperationResult> Delete(DeletePostCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SetImage(SetImageCommand command)
        { 
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> AddImage(AddImageCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> RemoveImage(RemoveImagePostCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Add(AddPostCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditPostCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> PostToInstagram(SendToInstagramCommand command)
        {
            return await _mediator.Send(command);
        }

        //public async Task<OperationResult> DeleteTelegram(int postId)
        //{
        //    return await _mediator.Send(instagramCommand); 
        //}

        public async Task<OperationResult> SendMessageToTelegram(SendMessageToTelegramCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SendImageToTelegram(SendImageToTelegramCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SendVideoToTelegram(SendVideoToTelegramCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
