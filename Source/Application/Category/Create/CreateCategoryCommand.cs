using Common.Application;
using Common.Domain.ValueObjects;
using Domain.CategoryAgg;
using Domain.CategoryAgg.Repository;
using Domain.CategoryAgg.Service;

namespace Application.Category.Create
{
    public record class CreateCategoryCommand(string Title, string Slug, SeoData SeoData) : IBaseCommand;

    internal class CreateCategoryCommandHandler : IBaseCommandHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _repository;
        private readonly ICategoryService _service;

        public CreateCategoryCommandHandler(ICategoryRepository repository, ICategoryService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Domain.CategoryAgg.Category(
                request.Title, request.Slug, request.SeoData, _service);
            
            _repository.Add(category);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
