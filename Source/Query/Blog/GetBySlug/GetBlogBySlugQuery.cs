using Common.Application;
using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Blog.DTOs;

namespace Query.Blog.GetBySlug
{
    public class GetBlogBySlugQuery : IQuery<BlogDto?>
    {
        public string Slug { get; set; }
    }
    internal class GetBlogBySlugQueryHandler : IQueryHandler<GetBlogBySlugQuery, BlogDto?>
    {
        private readonly PlanningContext _context;

        public GetBlogBySlugQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<BlogDto?> Handle(GetBlogBySlugQuery request, CancellationToken cancellationToken)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(i => i.Slug.Equals(request.Slug), cancellationToken);
            if (blog == null)
                return null!;

            return blog.Map();
        }
    }
}
