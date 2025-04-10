using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.ValueObjects;
using Domain.BlogAgg.Repository;
using Domain.BlogAgg.Service;
using Microsoft.AspNetCore.Http;

namespace Application.Blog.Edit
{
    public class EditBlogCommand : IBaseCommand
    {
        public long BlogId { get; set; }
        public string Slug { get; set; }
        public IFormFile Image { get; set; }
        public DateTime SendTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatorUserName { get; set; }
        public SeoData SeoData { get; set; }
        public bool IsSend { get; set; }
        public long CategoryId { get; set; }
    }
    internal class EditBlogCommandHandler : IBaseCommandHandler<EditBlogCommand>
    {
        private readonly IBlogService _service;
        private readonly IBlogRepository _repository;
        private readonly IFileService _fileService;

        public EditBlogCommandHandler(IBlogRepository repository, IBlogService service, IFileService fileService)
        {
            _repository = repository;
            _service = service;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(EditBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _repository.GetTracking(request.BlogId);
            if (blog == null)
                return OperationResult.NotFound();
            string oldImage = blog.ImageName;
            blog.Edit(request.SendTime, request.Title, request.Description, 
                request.CreatorUserName, request.SeoData, request.IsSend, 
                request.Slug, request.CategoryId, _service);

            if (request.Image != null)
            {
                var imageName = await _fileService.SaveFileAndGenerateName
                    (request.Image, Directories.BlogImage);
                RemoveOldImage(request.Image, blog.ImageName);
                blog.SetImage(imageName);
            }
            await _repository.Save();
            return OperationResult.Success();
        }
        private void RemoveOldImage(IFormFile imageFile, string oldImageName)
        {
            if (imageFile != null)
            {
                _fileService.DeleteFile(Directories.BlogImage, oldImageName);
            }
        }
    }
}
