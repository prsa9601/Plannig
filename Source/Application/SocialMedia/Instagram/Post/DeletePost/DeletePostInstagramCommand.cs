using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.InstagramAgg.Repository;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace Application.SocialMedia.Instagram.Post.DeletePost
{
    public class DeletePostInstagramCommand : IBaseCommand
    {
        public long InstagramId { get; set; }
        public long Id { get; set; }
    }
    internal class DeletePostCommandHandler : IBaseCommandHandler<DeletePostInstagramCommand>
    {
        private readonly IInstagramRepository _repository;
        private readonly IFileService _fileService;

        public DeletePostCommandHandler(IInstagramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(DeletePostInstagramCommand request, CancellationToken cancellationToken)
        {
            var instagram = await _repository.GetTracking(request.InstagramId);
            if (instagram == null) 
                return OperationResult.NotFound();
            var post = instagram.Posts.FirstOrDefault(i=>i.Id==request.Id);
            if (post == null)
                return OperationResult.NotFound();
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
            instagram.RemovePost(request.Id);

            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
