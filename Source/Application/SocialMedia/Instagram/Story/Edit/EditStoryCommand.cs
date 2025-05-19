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
        //public IFormFile? Video { get; set; }
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
            var story = instagram.Stories.Where(i => i.Id == request.StoryId)
                .Select(i => i).FirstOrDefault();

            if (story == null)
                return OperationResult.NotFound();

            //if (request.Image != null && request.Video != null)
            //    return OperationResult.Error("امکان ذخیره تصویر و ویدیو به صورت همزمان وجود ندارد!");
           
            string filePath = _fileService.DetermineDirectory(request.Image);
            string imageName = await _fileService.SaveFileAndGenerateName(
                request.Image, filePath);

            //if (story.Video != null)
            //    _fileService.DeleteFile(Directories.InstagramStoryImages, story.Video!.VideoPath);
            //if (story.Image != null)
            //{

            //    _fileService.DeleteFile(Directories.InstagramStoryImages, story.Image!.PictureName);
            //    story.RemoveImage();
            //}
            if (filePath == Directories.InstagramStoryImages)
            {
                _fileService.DeleteFile(Directories.InstagramStoryImages, story.Image!.PictureName);
                story.RemoveImage();
                story.SetImage(imageName);
               
            }
            //else if (filePath == "wwwroot/images/Instagram/Story/Videos")
            else if (filePath == Directories.InstagramStoryVideos)
            {
                _fileService.DeleteFile(Directories.InstagramStoryVideos, story.Video!.VideoPath);
                story.RemoveVideo();
                story.SetVideo(imageName);
            }


            story.SetImage(imageName);

            //if (request.Image != null && request.Video == null)
            //{
            //    string imageName = await _fileService.SaveFileAndGenerateName(
            //        request.Image, Directories.InstagramStoryImages);
            //    if (story.Image!=null)
            //        _fileService.DeleteFile(Directories.InstagramStoryImages, story.Image!.PictureName);
            //    if (story.Video != null)
            //    {
            //        _fileService.DeleteFile(Directories.InstagramStoryVideos, story.Video!.VideoPath);
            //        story.RemoveVideo();
            //    }

            //    story.SetImage(imageName);
            //}


            story.Edit(request.DateOfPosting, request.Link);

            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
