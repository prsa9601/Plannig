using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Category.DTOs;

namespace Query.Category.GetList
{
    public record class GetListCategoryQuery : IQuery<List<CategoryDto?>>;
    internal class GetCategoryListQueryHandler : IQueryHandler<GetListCategoryQuery, List<CategoryDto?>>
    {
        private readonly PlanningContext _context;

        public GetCategoryListQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto?>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            //var model = await _context.Categories
            //     .Where(r => r.ParentId == null)
            //     .Include(c => c.Childs)
            //     .ThenInclude(c => c.Childs)
            //     .OrderByDescending(d => d.Id).ToListAsync(cancellationToken);
            var model = await _context.Categories
                 .OrderByDescending(d => d.Id).ToListAsync(cancellationToken);
            return model!.Map()!;
        }
    }
}
