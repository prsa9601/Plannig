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

namespace Application.SocialMedia.Instagram.Story.Add
{
    public class AddStoryCommand : IBaseCommand
    {
        public long InstagramId { get; set; }
        public DateTime DateOfPosting { get; set; }
        public string Link { get; set; }
        public IFormFile Image { get; set; }
    }
    internal class AddStoryCommandHandler : IBaseCommandHandler<AddStoryCommand>
    {
        private readonly IInstagramRepository _repository;
        private readonly IFileService _fileService;

        public AddStoryCommandHandler(IInstagramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(AddStoryCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var instagram = await _repository.GetTracking(request.InstagramId);
                if (instagram == null)
                    return OperationResult.NotFound();

                string filePath = _fileService.DetermineDirectory(request.Image);
                string imageName = await _fileService.SaveFileAndGenerateName(
                    request.Image, filePath);
                var story = new Domain.SocialMediaAgg.InstagramAgg.Story.Story(
                    request.DateOfPosting, request.Link);
                instagram.AddStory(story);
                //if (filePath == "wwwroot/images/Instagram/Story/Images")
                if (filePath == Directories.InstagramStoryImages)
                {
                    story.SetImage(imageName);
                }
                //else if (filePath == "wwwroot/images/Instagram/Story/Videos")
                else if (filePath == Directories.InstagramStoryVideos)
                {
                    story.SetVideo(imageName);
                }

                await _repository.Save();
                return OperationResult.Success();
            }
            catch (Exception e)
            {
                return OperationResult.Error(e.Message);
            }
        }
    }
}
