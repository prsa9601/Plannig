using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.TelegramAgg.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.SocialMedia.Telegram.Post.EditPost
{
    public record class EditPostCommand(string TelegramId, long PostId, DateTime DateOfPosting, string Description, string Link, string Slug, string VideoName, IFormFile Image) : IBaseCommand;
    internal class EditPostCommandHandler : IBaseCommandHandler<EditPostCommand>
    {
        private readonly ITelegramRepository _repository;
        private readonly IFileService _fileService;

        public EditPostCommandHandler(ITelegramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            var telegram = await _repository.GetTrackingWithString(request.TelegramId);
            if (telegram == null)
                return OperationResult.NotFound();
            
            var post = telegram.Posts.FirstOrDefault(i => i.Id == request.PostId);
            if(post == null) 
                return OperationResult.NotFound();

            string ImageName = await _fileService.SaveFileAndGenerateName(request.Image, Directories.TelegramImages);
            post.Edit(request.DateOfPosting, request.Description, request.Link, ImageName, request.VideoName);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
