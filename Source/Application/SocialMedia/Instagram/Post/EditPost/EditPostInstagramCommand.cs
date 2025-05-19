using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.InstagramAgg.Repository;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Application.SocialMedia.Instagram.Post.EditPost
{
    public class EditPostInstagramCommand : IBaseCommand
    {
        public long postId { get; set; }
        public long InstagramAccountId { get; set; } //TableId 
        public DateTime DateOfPosting { get; set; }
        public string Link { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        //public List<IFormFile>? Images { get; set; }
        public List<IFormFile>? Videos { get; set; }


    }
    internal class EditPostCommandHandler : IBaseCommandHandler<EditPostInstagramCommand>
    {
        private readonly IInstagramRepository _repository;
        private readonly IFileService _fileService;

        public EditPostCommandHandler(IInstagramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(EditPostInstagramCommand request, CancellationToken cancellationToken)
        {
            var instagram = await _repository.GetTracking(request.InstagramAccountId);
            if (instagram == null)
                return OperationResult.NotFound();
            var post = instagram.Posts.FirstOrDefault(i => i.Videos.Any(x => x.Id == request.postId));
            if (post != null)
            {
                post.Edit(request.DateOfPosting,
                request.Description, request.Link);
                //if (request.Images!.Count > 0)
                //{
                //    List<string> imageNames = new List<string>();
                //    foreach (var image in request.Images)
                //    {
                //        string imageName = await _fileService.SaveFileAndGenerateName
                //            (image, Directories.InstagramPostImages);
                //        imageNames.Add(imageName);
                //    }
                //    item.AddImage(imageNames);
                //}

                foreach (var video in post.Videos.ToList())
                {
                    try
                    {
                        _fileService.DeleteFile(Directories.InstagramPostVideos, video.VideoName);
                        post.RemoveVideo(video.Id);

                    }
                    catch
                    {
                        return OperationResult.Error();
                    }
                }
                if (request.Videos!.Count > 0)
                {
                    List<string> videoNames = new List<string>();
                    foreach (var image in request.Videos)
                    {
                        string imageName = await _fileService.SaveFileAndGenerateName
                            (image, Directories.InstagramPostVideos);

                        //item.Videos.ForEach(i=> _fileService.DeleteFile
                        //    (Directories.InstagramPostVideos,i.VideoName));



                        videoNames.Add(imageName);
                    }
                    post.AddVideo(videoNames);
                }
                await _repository.Save();
                return OperationResult.Success();
            }
            return OperationResult.NotFound("پستی یتفت نشد.");
        }
    }
}
