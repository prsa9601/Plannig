using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Domain.SocialMediaAgg.TelegramAgg.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.SocialMedia.Telegram.Post.AddImageToPost
{
    public class AddImageCommand : IBaseCommand
    {
        public string TelegramId { get; set; }
        public IFormFile ImageFile { get; set; }
        public long ProductId { get; set; }
        public int Sequence { get; set; }
    }
    internal class AddImageCommandHandler : IBaseCommandHandler<AddImageCommand>
    {
        private readonly ITelegramRepository _repository;
        private readonly IFileService _fileService;

        public AddImageCommandHandler(ITelegramRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            var telegram = await _repository.
                GetTrackingWithString(request.TelegramId);
            if (telegram == null) 
                return OperationResult.NotFound();
            var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.TelegramImages);
            telegram.AddImage(imageName);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
