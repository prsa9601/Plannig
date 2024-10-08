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
            var instagram = await _repository.GetTracking(request.InstagramId);
            if (instagram == null) 
                return OperationResult.NotFound();
            
            string imageName = await _fileService.SaveFileAndGenerateName(
                request.Image, Directories.InstagramStoryVideos);
           
            instagram.AddStory(new Domain.SocialMediaAgg.InstagramAgg.Story.Story(
                request.DateOfPosting, request.Link, imageName));
            
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
