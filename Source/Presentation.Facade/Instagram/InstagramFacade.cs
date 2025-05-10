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
using Application.SocialMedia.Instagram.Story.Delete;
using Application.SocialMedia.Instagram.Story.Edit;
using Common.Application;
using MediatR;
using Query.SocialMedia.Instagram.Account.DTOs;
using Query.SocialMedia.Instagram.Account.GetByFilter;
using Query.SocialMedia.Instagram.Account.GetById;
using Query.SocialMedia.Instagram.Account.GetList;
using Query.SocialMedia.Instagram.Post.DTOs;
using Query.SocialMedia.Instagram.Post.GetByFilter;
using Query.SocialMedia.Instagram.Story.DTOs;
using Query.SocialMedia.Instagram.Story.GetByFilter;

namespace Presentation.Facade.Instagram
{
    internal class InstagramFacade : IInstagramFacade
    {
        private readonly IMediator _mediator;

        public InstagramFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Delete(DeleteStoryCommand command)
        {
            return await _mediator.Send(command);
        }

        //public async Task<OperationResult> DeleteStory(int instagramCommand)
        //{
        //    return await _mediator.Send(instagramCommand);
        //}

        public async Task<OperationResult> UploadStory(Application.SocialMedia.Instagram.Story.SendToInstagram.SendToInstagramCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditStory(EditStoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> DeleteStory(DeleteStoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Delete(DeletePostInstagramCommand command)
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

        //public async Task<OperationResult> AddImage(AddImageCommand instagramCommand)
        //{
        //    return await _mediator.Send(instagramCommand);
        //}

        public async Task<OperationResult> RemoveImage(RemoveImagePostCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Add(AddPostInstagramCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditPostInstagramCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> PostToInstagram(Application.SocialMedia.Instagram.Post.SendPostToInstagram.SendToInstagramCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<InstagramAccountDto?> GetById(long Id)
        {
            return await _mediator.Send(new GetInstagramAccountById(Id));
        }

        public async Task<List<InstagramAccountDto>?> GetList(string UserName)
        {
            return await _mediator.Send(new GetListInstagramQuery(UserName));
        }

        public async Task<InstagramAccountFilterResult?> GetByFilter(InstagramAccountFilterParam param)
        {
            return await _mediator.Send(new GetInstagramAccountByFilter(param));
        }

        public async Task<OperationResult> AddAccount(AddInstagramAccountCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditAccount(EditInstagramAccountCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> DeleteAccount(DeleteInstagramAccountCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SetProfileAccount(SetProfileInstagramAccountCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> AddStory(AddStoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<PostFilterData.InstagramPostFilterResult?> GetInstagramPostByFilter(PostFilterData.InstagramPostFilterParam param)
        {
            return await _mediator.Send(new GetInstagramPostByFilterQuery(param));
        }

        public async Task<StoryFilterResult?> GetInstagramStoryByFilter(StoryFilterParam param)
        {
            return await _mediator.Send(new GetInstagramStoryByFilterQuery(param));
        }
    }
}
