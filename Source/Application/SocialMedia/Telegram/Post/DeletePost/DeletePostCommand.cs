using Common.Application;
using Domain.SocialMediaAgg.TelegramAgg.Repository;

namespace Application.SocialMedia.Telegram.Post.DeletePost
{
    public class DeletePostCommand : IBaseCommand
    {
        public long TelegramId { get; set; }
        public long PostId { get; set; }
    }
    internal class DeletePostCommandHandler : IBaseCommandHandler<DeletePostCommand>
    {
        private readonly ITelegramRepository _repository;

        public DeletePostCommandHandler(ITelegramRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var telegram = await _repository.GetTracking(request.TelegramId);
            if (telegram == null)
                return OperationResult.NotFound();
            telegram.RemovePost(request.PostId);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
