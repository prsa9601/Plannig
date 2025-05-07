using Application.SocialMedia.Instagram.Account.Add;
using Application.SocialMedia.Instagram.Account.Delete;
using Application.SocialMedia.Instagram.Account.Edit;
using Application.SocialMedia.Instagram.Account.SetProfile;
using Application.SocialMedia.Instagram.Post.AddImageToPost;
using Application.SocialMedia.Instagram.Post.AddPost;
using Application.SocialMedia.Instagram.Post.DeletePost;
using Application.SocialMedia.Instagram.Post.EditPost;
using Application.SocialMedia.Instagram.Post.RemoveImageToPost;
using Application.SocialMedia.Instagram.Post.SendPostToInstagram;
using Application.SocialMedia.Instagram.Post.SetImageToPost;
using Application.SocialMedia.Instagram.Story.Add;
using Application.SocialMedia.Instagram.Story.Delete;
using Common.Application;
using Query.SocialMedia.Instagram.Account.DTOs;
using Query.SocialMedia.Instagram.Story.DTOs;
using static Query.SocialMedia.Instagram.Post.DTOs.PostFilterData;

namespace Presentation.Facade.Instagram
{
    public interface IInstagramFacade
    {
        Task<OperationResult> Delete(DeleteStoryCommand command);
        Task<OperationResult> AddStory(AddStoryCommand command);
        //Task<OperationResult> DeleteStory();
        Task<OperationResult> UploadStory(Application.SocialMedia
            .Instagram.Story.SendToInstagram.SendToInstagramCommand command);
        Task<OperationResult> EditStory(Application.SocialMedia
            .Instagram.Story.Edit.EditStoryCommand command);
        Task<OperationResult> DeleteStory(Application.SocialMedia
            .Instagram.Story.Delete.DeleteStoryCommand command);
        Task<OperationResult> Delete(DeletePostInstagramCommand command);
        Task<OperationResult> SetImage(SetImageCommand command);
        Task<OperationResult> AddImage(AddImageCommand image);
        Task<OperationResult> RemoveImage(RemoveImagePostCommand id);
        Task<OperationResult> Add(AddPostInstagramCommand command);
        Task<OperationResult> Edit(EditPostInstagramCommand command);

        //Instagram
        Task<OperationResult> PostToInstagram(SendToInstagramCommand command);
        
        Task<OperationResult> AddAccount(AddInstagramAccountCommand command);
        Task<OperationResult> EditAccount(EditInstagramAccountCommand command);
        Task<OperationResult> DeleteAccount(DeleteInstagramAccountCommand command);
        Task<OperationResult> SetProfileAccount(SetProfileInstagramAccountCommand command);


        Task<InstagramAccountDto?> GetById(long Id);
        Task<List<InstagramAccountDto>?> GetList(string UserName);
        Task<InstagramAccountFilterResult?> GetByFilter(InstagramAccountFilterParam param);

        //Story
        Task<InstagramPostFilterResult?> GetInstagramPostByFilter(InstagramPostFilterParam param);
        
        //Post
        Task<StoryFilterResult?> GetInstagramStoryByFilter(StoryFilterParam param);

    }
}
