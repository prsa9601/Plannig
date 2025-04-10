using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Blog.DTOs;

namespace Query.Blog.GetById
{
    public record class GetBlogByIdQuery(long BlogId) : IQuery<BlogDto?>;

    internal class GetBlogByIdQueryHandler : IQueryHandler<GetBlogByIdQuery, BlogDto?>
    {
        private readonly PlanningContext _context;

        public GetBlogByIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<BlogDto?> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync
                (i => i.Id.Equals(request.BlogId), cancellationToken);
            if (blog == null) 
                return null;

            return blog.Map();
        }
    }
}
