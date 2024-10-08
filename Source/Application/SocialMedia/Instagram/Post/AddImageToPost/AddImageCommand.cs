using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.InstagramAgg.Post;
using Domain.SocialMediaAgg.InstagramAgg.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.SocialMedia.Instagram.Post.AddImageToPost
{
    public class AddImageCommand : IBaseCommand
    {
        public string InstagramUserName { get; set; }
        public IFormFile ImageFile { get; set; }
        public long PostId { get; set; }
        public int Sequence { get; set; }
    }
    internal class AddImageCommandHandler : IBaseCommandHandler<AddImageCommand>
    {
        private readonly IInstagramRepository _repository;
        private readonly IFileService _fileService;

        public AddImageCommandHandler(IInstagramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            var instagram = await _repository.GetTrackingByUserName(request.InstagramUserName);
            foreach (var item in instagram.Posts)
            {
                if (item.Id == request.PostId)
                {
                    string imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.InstagramPostImages);
                    item.AddImage(new InstagramPostImage(imageName,request.Sequence));
                    await _repository.Save();
                    return OperationResult.Success();
                } 
            }
            return OperationResult.NotFound();
        }
    }
}
