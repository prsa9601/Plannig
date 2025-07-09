using Application._Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using Domain.BlogAgg.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Blog.SetImage
{
    public class SetImageBlogCommand : IBaseCommand
    {
        public IFormFile Image { get; set; }
        public long Id { get; set; }
    }
    public class SetImageBlogCommandHandler : IBaseCommandHandler<SetImageBlogCommand>
    {
        private readonly IBlogRepository _repository;
        private readonly IFileService _fileService;
        public SetImageBlogCommandHandler(IBlogRepository repository, IFileService service)
        {
            _repository = repository;
            _fileService = service;
        }

        public async Task<OperationResult> Handle(SetImageBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _repository.GetTracking(request.Id);
            if (blog == null)
                return OperationResult.NotFound();

            if (request.Image != null)
            {
                var imageName = await _fileService.SaveFileAndGenerateName
                    (request.Image, Directories.BlogImage);
                _fileService.DeleteFile(Directories.BlogImage, blog.ImageName);
                blog.SetImage(imageName);
            }
            await _repository.Save();
            return OperationResult.Success();
        }

    } 
}
