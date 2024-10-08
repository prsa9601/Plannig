using Common.Application;
using Domain.SocialMediaAgg.InstagramAgg.Repository;

namespace Application.SocialMedia.Instagram.Post.AddPost
{
    public class AddPostInstagramCommand : IBaseCommand
    {
        public string InstagramId { get; set; } = string.Empty;
        public DateTime DateOfPosting { get; set; }
        public string Link { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public string VideoName { get; set; } = string.Empty;                 

    }

    public class AddPostCommandHandler : IBaseCommandHandler<AddPostInstagramCommand>
    {
        private readonly IInstagramRepository _repository;

        public AddPostCommandHandler(IInstagramRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(AddPostInstagramCommand request, CancellationToken cancellationToken)
        {
            var post = new Domain.SocialMediaAgg.InstagramAgg.Post.Post(request.DateOfPosting,
                request.Description, request.Link,
                request.ImageName, request.VideoName);

            var instagram = await _repository.GetTrackingByInstagramId(request.InstagramId);
            if(instagram == null) 
                return OperationResult.NotFound();
            instagram.AddPost(post);
            return OperationResult.Success();
        }
    }
}
