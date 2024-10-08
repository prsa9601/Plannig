 using Common.Application;
using Domain.SocialMediaAgg.InstagramAgg.Repository;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace Application.SocialMedia.Instagram.Post.DeletePost
{
    public class DeletePostInstagramCommand : IBaseCommand
    {
        public long PostId { get; set; }
        public string UserName { get; set; }
    }
    internal class DeletePostCommandHandler : IBaseCommandHandler<DeletePostInstagramCommand>
    {
        private readonly IInstagramRepository _repository;

        public DeletePostCommandHandler(IInstagramRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(DeletePostInstagramCommand request, CancellationToken cancellationToken)
        {
            var instagram = await _repository.GetTrackingByUserName(request.UserName);
            if (instagram == null) 
                return OperationResult.NotFound();

            instagram.RemovePost(request.PostId);

            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
