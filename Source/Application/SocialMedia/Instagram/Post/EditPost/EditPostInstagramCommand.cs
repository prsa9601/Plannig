using Common.Application;
using Domain.SocialMediaAgg.InstagramAgg.Repository;

namespace Application.SocialMedia.Instagram.Post.EditPost
{
    public class EditPostInstagramCommand : IBaseCommand
    {
        public long postId { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfPosting { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string VideoName { get; set; }


    }
    internal class EditPostCommandHandler : IBaseCommandHandler<EditPostInstagramCommand>
    {
        private readonly IInstagramRepository _repository;

        public EditPostCommandHandler(IInstagramRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(EditPostInstagramCommand request, CancellationToken cancellationToken)
        {
            var instagram = await _repository.GetTrackingByUserName(request.UserName);
            if (instagram == null) 
                return OperationResult.NotFound();

            foreach (var item in instagram.Posts)
            {
                if (item.Id == request.postId)
                {
                    item.Edit(request.DateOfPosting,
                        request.Description, request.Link, 
                        request.ImageName, request.VideoName);

                    await _repository.Save();
                    return OperationResult.Success();
                }
            }
            return OperationResult.Error();
        }
    }
}
