using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Category.DTOs;

namespace Query.Category.GetById
{
    public record class GetCategoryByIdQuery(long CategoryId) : IQuery<CategoryDto?>;
    
    internal class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto?>
    {
        private readonly PlanningContext _context;

        public GetCategoryByIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _context.Categories
           .FirstOrDefaultAsync(f => f.Id == request.CategoryId, cancellationToken);
            return model.Map();
        }
    }
}
