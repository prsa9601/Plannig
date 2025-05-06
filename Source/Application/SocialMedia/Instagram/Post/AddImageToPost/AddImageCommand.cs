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
        public long InstagramId { get; set; } // TableId
        public List<IFormFile> ImageFile { get; set; }
        public long PostId { get; set; } // TableId
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
            var instagram = await _repository.GetTracking(request.InstagramId);

            if (instagram == null)
                return OperationResult.NotFound();

            List<string> ImageNames = new List<string>();
            foreach (var item in instagram.Posts)
            {
                if (item.Id == request.PostId)
                {
                    for (global::System.Int32 i = 0; i < request.ImageFile.Count(); i++)
                    {
                        string imageName = await _fileService.
                            SaveFileAndGenerateName(request.ImageFile[i],
                            Directories.InstagramPostImages);
                        ImageNames.Add(imageName);

                    }
                    item.AddImage(ImageNames);

                }
            }
            await _repository.Save();
            return OperationResult.Success();

        }
    }
}
