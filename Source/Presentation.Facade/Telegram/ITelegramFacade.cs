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

namespace Presentation.Facade.Telegram
{
    public interface ITelegramFacade
    {
        Task<OperationResult> Delete(DeletePostCommand command);
        Task<OperationResult> SetImage(SetImageCommand command);
        Task<OperationResult> AddImage(AddImageCommand image);
        Task<OperationResult> RemoveImage(RemoveImagePostCommand id);
        Task<OperationResult> Add(AddPostCommand command);
        Task<OperationResult> Edit(EditPostCommand command);

        //Instagram
        Task<OperationResult> PostToInstagram(SendToInstagramCommand command);

        //Telegram
       // Task<OperationResult> DeleteTelegram(int postId);
        Task<OperationResult> SendMessageToTelegram(SendMessageToTelegramCommand command);
        Task<OperationResult> SendImageToTelegram(SendImageToTelegramCommand command);
        Task<OperationResult> SendVideoToTelegram(SendVideoToTelegramCommand command);




    }
}
