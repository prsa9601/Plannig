using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.TelegramAgg.Repository;

namespace Application.SocialMedia.Telegram.Post.RemoveImageToPost
{
    public class RemoveImagePostCommand : IBaseCommand
    {
        public long TelegramId { get; set; }
        public long PostId { get; set; }
        public long ImageId { get; set; }
    }
    internal class RemoveImagePostCommandHandler : IBaseCommandHandler<RemoveImagePostCommand>
    {
        private readonly ITelegramRepository _repository;
        private readonly IFileService _fileService;

        public RemoveImagePostCommandHandler(ITelegramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(RemoveImagePostCommand request, CancellationToken cancellationToken)
        {
            var telegram = await _repository.GetTracking(request.TelegramId);
            if (telegram == null)
                return OperationResult.NotFound();

            var post = telegram.Posts.FirstOrDefault(i => i.Id == request.PostId);
            if (post == null)
                return OperationResult.NotFound();

            post.RemoveImage(request.ImageId);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
