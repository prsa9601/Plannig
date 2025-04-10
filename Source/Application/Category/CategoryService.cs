using Domain.CategoryAgg.Repository;
using Domain.CategoryAgg.Service;

namespace Application.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public bool IsSlugExist(string slug)
        {
            return _repository.Exists(i => i.Slug.Equals(slug));
        }

        public bool IsTitleExist(string title)
        {
            return _repository.Exists(i => i.Title.Equals(title));
        }
    }
}
