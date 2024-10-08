using Application.SocialMedia.Instagram.Post.AddImageToPost;
using Application.SocialMedia.Instagram.Post.AddPost;
using Application.SocialMedia.Instagram.Post.DeletePost;
using Application.SocialMedia.Instagram.Post.EditPost;
using Application.SocialMedia.Instagram.Post.RemoveImageToPost;
using Application.SocialMedia.Instagram.Post.SetImageToPost;
using Application.SocialMedia.Instagram.Story.Delete;
using Application.SocialMedia.Instagram.Story.Edit;
using Application.SocialMedia.Instagram.Story.SendToInstagram;
using Common.Application;
using MediatR;
using Presentation.Facade.Instagram;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        public async Task<OperationResult> Delete(DeletePostInstagramCommand instagramCommand)
        {
            return await _mediator.Send(instagramCommand);
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

        public async Task<OperationResult> Add(AddPostInstagramCommand instagramCommand)
        {
            return await _mediator.Send(instagramCommand);
        }

        public async Task<OperationResult> Edit(EditPostInstagramCommand instagramCommand)
        {
            return await _mediator.Send(instagramCommand);
        }

        public async Task<OperationResult> PostToInstagram(Application.SocialMedia.Instagram.Post.SendPostToInstagram.SendToInstagramCommand command)
        {
            return await _mediator.Send(command);
        }


    }
}
