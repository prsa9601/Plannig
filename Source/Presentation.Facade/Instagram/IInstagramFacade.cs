using Application.SocialMedia.Instagram.Post.AddImageToPost;
using Application.SocialMedia.Instagram.Post.AddPost;
using Application.SocialMedia.Instagram.Post.DeletePost;
using Application.SocialMedia.Instagram.Post.EditPost;
using Application.SocialMedia.Instagram.Post.RemoveImageToPost;
using Application.SocialMedia.Instagram.Post.SendPostToInstagram;
using Application.SocialMedia.Instagram.Post.SetImageToPost;
using Application.SocialMedia.Instagram.Story.Delete;
using Common.Application;

namespace Presentation.Facade.Instagram
{
    public interface IInstagramFacade
    {
        Task<OperationResult> Delete(DeleteStoryCommand command);
        //Task<OperationResult> DeleteStory();
        Task<OperationResult> UploadStory(Application.SocialMedia
            .Instagram.Story.SendToInstagram.SendToInstagramCommand command);
        Task<OperationResult> EditStory(Application.SocialMedia
            .Instagram.Story.Edit.EditStoryCommand command);
        Task<OperationResult> DeleteStory(Application.SocialMedia
            .Instagram.Story.Delete.DeleteStoryCommand command);
        Task<OperationResult> Delete(DeletePostInstagramCommand instagramCommand);
        Task<OperationResult> SetImage(SetImageCommand command);
        Task<OperationResult> AddImage(AddImageCommand image);
        Task<OperationResult> RemoveImage(RemoveImagePostCommand id);
        Task<OperationResult> Add(AddPostInstagramCommand instagramCommand);
        Task<OperationResult> Edit(EditPostInstagramCommand instagramCommand);

        //Instagram
        Task<OperationResult> PostToInstagram(SendToInstagramCommand command);


    }
}
