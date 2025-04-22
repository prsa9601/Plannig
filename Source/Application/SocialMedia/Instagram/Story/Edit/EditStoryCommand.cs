using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.InstagramAgg.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.SocialMedia.Instagram.Story.Edit
{
    public class EditStoryCommand : IBaseCommand
    {
        public long InstagramId { get; set; }
        public long StoryId { get; set; } //TableId
        public DateTime DateOfPosting { get; set; }
        public string Link { get; set; }
        public IFormFile Image { get; set; }
    }
    internal class EditStoryCommandHandler : IBaseCommandHandler<EditStoryCommand>
    {
        private readonly IInstagramRepository _repository;
        private readonly IFileService _fileService;

        public EditStoryCommandHandler(IInstagramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(EditStoryCommand request, CancellationToken cancellationToken)
        {
            var instagram = await _repository.GetTracking(request.InstagramId);
            if (instagram == null)
                return OperationResult.NotFound();
            var story = instagram.Stories.Where(i => i.Id == request.StoryId).Select(i => i).FirstOrDefault();

            string imageName = await _fileService.SaveFileAndGenerateName(
                request.Image, Directories.InstagramStoryVideos);
            
            story.Edit(request.DateOfPosting, request.Link, imageName);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
