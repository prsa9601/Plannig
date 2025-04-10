using Common.Application;
using Common.Domain.ValueObjects;
using Domain.CategoryAgg.Repository;
using Domain.CategoryAgg.Service;

namespace Application.Category.Edit
{
    public record class EditCategoryCommand(long CategoryId, string Title, 
        SeoData SeoData, string Slug) : IBaseCommand;
    
    internal class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
    {
        private readonly ICategoryRepository _repository;
        private readonly ICategoryService _service;
        public EditCategoryCommandHandler(ICategoryRepository repository, ICategoryService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetTracking(request.CategoryId);
            if (category == null)
                return OperationResult.NotFound();

            category.Edit(request.Title, request.Slug, request.SeoData, _service);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
