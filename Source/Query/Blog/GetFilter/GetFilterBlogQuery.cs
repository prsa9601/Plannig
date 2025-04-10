using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Blog.DTOs;

namespace Query.Blog.GetFilter
{
    public class GetFilterBlogQuery : QueryFilter<BlogFilterResult, BlogFilterParam>
    {
        public GetFilterBlogQuery(BlogFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal class GetFilterBlogQueryHandler : IQueryHandler<GetFilterBlogQuery, BlogFilterResult>
    {
        private readonly PlanningContext _context;

        public GetFilterBlogQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<BlogFilterResult> Handle(GetFilterBlogQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _context.Blogs.OrderByDescending(d => d.Id).AsQueryable();

            if (!string.IsNullOrWhiteSpace(@params.Slug))
                result = result.Where(r => r.Slug == @params.Slug);

            if (!string.IsNullOrWhiteSpace(@params.Title))
                result = result.Where(r => r.Title.Contains(@params.Title));

            if (!string.IsNullOrWhiteSpace(@params.Search))
                result = result.Where(r => r.Title.Contains(@params.Search) ||  
                r.Slug.Contains(@params.Search) || r.Title.Contains(@params.Search));

            //if (@params.CategoryId != 0)
            //    result = result.Where(r => r.CategoryId == @params.CategoryId);

            switch (@params.SearchOrderBy)
            {
                case PostSearchOrderBy.latest:
                    {
                        result = result.OrderByDescending(r => r.CreationDate);
                        break;
                    }
                case PostSearchOrderBy.visit:
                    {
                        result = result.OrderByDescending(r => r.Visit);
                        break;
                    }
            }

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new BlogFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take).Select(s => s.MapFilter()!)
                    .ToListAsync(cancellationToken),
                FilterParams = @params
            };
            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;

        }
    }
}
