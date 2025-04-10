using Domain.BlogAgg.Repository;
using Domain.BlogAgg.Service;

namespace Application.Blog
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository;

        public BlogService(IBlogRepository repository)
        {
            _repository = repository;
        }

        public bool SlugExist(string slug)
        {
            return _repository.Exists(i => i.Slug.Equals(slug));
        }
    }
}
