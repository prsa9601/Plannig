using Application.Blog.Add;
using Application.Blog.Edit;
using Application.Blog.IncreaseVisit;
using Application.Blog.Remove;
using Application.Blog.SetImage;
using Common.Application;
using MediatR;
using Query.Blog.DTOs;
using Query.Blog.GetById;
using Query.Blog.GetBySlug;
using Query.Blog.GetFilter;

namespace Presentation.Facade.Blog
{
    public interface IBlogFacade
    {
        Task<OperationResult> Create(AddBlogCommand command);
        Task<OperationResult> Edit(EditBlogCommand command);
        Task<OperationResult> SetImage(SetImageBlogCommand command);
        Task<OperationResult> Remove(RemoveBlogCommand command);
        Task<OperationResult> IncreaseVisit(IncreaseBlogVisitCommand command);
        Task<BlogDto?> GetBlogById(long BlogId);
        Task<BlogDto?> GetBlogBySlug(string Slug);
        Task<BlogFilterResult?> GetBlogByFilter(BlogFilterParam param);
    }
    internal class BlogFacade : IBlogFacade
    {
        private readonly IMediator _mediator;

        public BlogFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(AddBlogCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditBlogCommand command)
        {
            return await _mediator.Send(command);
        } 

        public async Task<BlogFilterResult?> GetBlogByFilter(BlogFilterParam param)
        {
            return await _mediator.Send(new GetFilterBlogQuery(param));
        }

        public async Task<BlogDto?> GetBlogById(long BlogId)
        {
            return await _mediator.Send(new GetBlogByIdQuery(BlogId));
        }

        public async Task<BlogDto?> GetBlogBySlug(string Slug)
        {
            return await _mediator.Send(new GetBlogBySlugQuery
            {
                Slug = Slug, 
            });
        }

        public async Task<OperationResult> IncreaseVisit(IncreaseBlogVisitCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Remove(RemoveBlogCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SetImage(SetImageBlogCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
