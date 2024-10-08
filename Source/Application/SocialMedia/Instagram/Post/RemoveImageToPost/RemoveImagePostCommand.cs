using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;
using Domain.SocialMediaAgg.InstagramAgg.Repository;

namespace Application.SocialMedia.Instagram.Post.RemoveImageToPost
{
    public class RemoveImagePostCommand : IBaseCommand
    {
        public long InstagramId { get; set; }
        public long PostId { get; set; }
        public long ImageId { get; set; }
    }

    internal class RemoveImagePostCommandHandler : IBaseCommandHandler<RemoveImagePostCommand>
    {
        private readonly IInstagramRepository _repository;

        public RemoveImagePostCommandHandler(IInstagramRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveImagePostCommand request, CancellationToken cancellationToken)
        {
            var instagram = await _repository.GetTracking(request.InstagramId);
            var posts = instagram.Posts.Select(i => i);
            var image = posts.Select(i => i.Images).ToList();

            foreach (var item in posts)
            {
                if (item.Id == request.PostId)
                {
                    var post = new Domain.SocialMediaAgg.InstagramAgg.Post.Post(item.DateOfPosting,
                        item.Description, item.Link,
                        item.ImageName, item.VideoName);
                    post.RemoveImage(request.ImageId);
                    await _repository.Save();
                    return OperationResult.Success();
                }
            }
            return OperationResult.NotFound();
        }
    }
}
        //    foreach (var item1 in image)
        //    {
        //        foreach (var item in item1)
        //        {
        //            if (item.PostId == request.PostId && item.Id == request.ImageId)
        //            {
        //                posts.de();
        //            }
        //        }
        //    }
        //}                                                 