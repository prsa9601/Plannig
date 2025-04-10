using Application.Category.Create;
using Application.Category.Edit;
using Application.Category.Remove;
using Common.Application;
using MediatR;
using Query.Category.DTOs;
using Query.Category.GetById;
using Query.Category.GetList;

namespace Presentation.Facade.Category
{
    public interface ICategoryFacade
    {
        Task<OperationResult> Create(CreateCategoryCommand command);
        Task<OperationResult> Edit(EditCategoryCommand command);
        Task<OperationResult> Remove(RemoveCategoryCommand command);
        Task<CategoryDto?> GetCategoryById(long CategoryId);
        Task<List<CategoryDto?>> GetListCategory();
    }
    internal class CategoryFacade : ICategoryFacade
    {
        private readonly IMediator _mediator;

        public CategoryFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(CreateCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<CategoryDto?> GetCategoryById(long CategoryId)
        {
            return await _mediator.Send(new GetCategoryByIdQuery(CategoryId));
        }

        public async Task<List<CategoryDto?>> GetListCategory()
        {
            return await _mediator.Send(new GetListCategoryQuery());
        }

        public async Task<OperationResult> Remove(RemoveCategoryCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
