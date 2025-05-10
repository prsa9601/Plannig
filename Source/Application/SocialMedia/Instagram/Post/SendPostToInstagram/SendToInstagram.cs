using Common.Application;
using Domain.SocialMediaAgg.InstagramAgg.Repository;
using FluentValidation.Results;

namespace Application.SocialMedia.Instagram.Post.SendPostToInstagram
{
    public class SendToInstagramCommand : IBaseCommand
    {
        public string PostId { get; set; }
      
    }
    internal class SendToInstagramCommandHandler : IBaseCommandHandler<SendToInstagramCommand>
    {
         readonly IInstagramRepository _repository;

        public SendToInstagramCommandHandler(IInstagramRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SendToInstagramCommand request, CancellationToken cancellationToken)
        {
            var instagram = await _repository.GetTrackingWithString(request.PostId);

            foreach (var item in instagram.Posts)
            {
                if (item.InstagramPostId == request.PostId)
                {
                    //await _repository.PostToInstagram(instagram.accessToken
                    //,item.ImageName ,  item.Description);

                    //, item.Images.Select(i => i.ImageName).ToList(),

                    return OperationResult.Success();
                }
            }
            return OperationResult.Error();
        }
    }
}
