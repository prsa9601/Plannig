using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
using Common.Domain.ValueObjects;
using Domain.BlogAgg;
using Domain.BlogAgg.Repository;
using Domain.BlogAgg.Service;
using Microsoft.AspNetCore.Http;
namespace Application.Blog.Add
{
    public class AddBlogCommand : IBaseCommand
    {
        public string Slug { get; set; }
        public IFormFile ImageName { get; set; }
        public DateTime SendTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatorUserName { get; set; }
        public SeoData SeoData { get; set; }
        public bool IsSend { get; set; }
        public long CategoryId { get; set; }
    }
    internal class AddBlogCommandHandler : IBaseCommandHandler<AddBlogCommand>
    {
        private readonly IBlogService _service;
        private readonly IBlogRepository _repository;
        private readonly IFileService _fileService;
        public AddBlogCommandHandler(IBlogService service, IBlogRepository repository, IFileService fileService)
        {
            _service = service;
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(AddBlogCommand request, CancellationToken cancellationToken)
        {
            var blogImage = await _fileService.SaveFileAndGenerateName(request.ImageName, Directories.BlogImage);

            var blog = new Domain.BlogAgg.Blog(request.SendTime, request.Title,
                request.Description, request.CreatorUserName, request.SeoData, blogImage,
                request.IsSend, request.Slug, request.CategoryId, _service);

            _repository.Add(blog);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
