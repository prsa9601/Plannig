using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.InstagramAgg.Post;
using Domain.SocialMediaAgg.InstagramAgg.Repository;
using Hangfire;
using Microsoft.AspNetCore.Http;

namespace Application.SocialMedia.Instagram.Post.AddPost
{
    public class AddPostInstagramCommand : IBaseCommand
    {
        public long InstagramAccountId { get; set; } //TableId 
        public DateTime DateOfPosting { get; set; }
        public string Link { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<IFormFile>? Images { get; set; }
        public List<IFormFile>? Videos { get; set; }

    }

    public class AddPostCommandHandler : IBaseCommandHandler<AddPostInstagramCommand>
    {
        private readonly IInstagramRepository _repository;
        private readonly IFileService _fileService;
        public AddPostCommandHandler(IInstagramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(AddPostInstagramCommand request, CancellationToken cancellationToken)
        {
            var post = new Domain.SocialMediaAgg.InstagramAgg.Post.Post(request.DateOfPosting,
                request.Description, request.Link);

            var instagram = await _repository.GetTracking(request.InstagramAccountId);
            if (instagram == null)
                return OperationResult.NotFound();

            instagram.AddPost(post);
            //await _repository.Save();

            if (request.Images!.Count > 0)
            { 
                List<string> imageNames = new List<string>();   
                foreach (var item in request.Images)
                {
                    string imageName = await _fileService.SaveFileAndGenerateName
                        (item, Directories.InstagramPostImages);
                    imageNames.Add(imageName);
                }
                post.AddImage(imageNames);
            }
            
            if (request.Videos!.Count > 0)
            { 
                List<string> videoNames = new List<string>();   
                foreach (var item in request.Videos)
                {
                    string imageName = await _fileService.SaveFileAndGenerateName
                        (item, Directories.InstagramPostVideos);
                    videoNames.Add(imageName);
                }
                post.AddVideo(videoNames);
            }

            //instagram.AddPost(post);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
