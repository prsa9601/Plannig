using Common.Query;
using Domain.CommentAgg;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Comment.DTOs;

namespace Query.Comment.GetByFilter
{
    public class GetCommentByFilterQuery : QueryFilter<CommentFilterResult, CommentFilterParam>
    {
        public GetCommentByFilterQuery(CommentFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal class GetCommentByFilterQueryHandler : IQueryHandler<GetCommentByFilterQuery, CommentFilterResult>
    {
        private readonly PlanningContext _context;

        public GetCommentByFilterQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<CommentFilterResult> Handle(GetCommentByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;

            var result = _context.Comments.OrderByDescending(d => d.CreationDate).AsQueryable();

            if (@params.BlogId != null)
                result = result.Where(r => r.Id == @params.BlogId);

            switch (@params.CommentStatus)
            {
                case CommentStatus.Accepted:
                    {
                        result = result.OrderByDescending(r => r.Status == CommentStatus.Accepted);
                        break;
                    }
                case CommentStatus.Pending:
                    {
                        result = result.Where(r => r.Status == CommentStatus.Pending);
                        break;
                    }
                case CommentStatus.Rejected:
                    {
                        result = result.Where(r => r.Status == CommentStatus.Rejected);
                        break;
                    }

            }

            if (@params.UserId != null)
                result = result.Where(r => r.UserId == @params.UserId);

            if (@params.StartDate != null)
                result = result.Where(r => r.CreationDate.Date >= @params.StartDate.Value.Date);

            if (@params.EndDate != null)
                result = result.Where(r => r.CreationDate.Date <= @params.EndDate.Value.Date);


            var skip = (@params.PageId - 1) * @params.Take;
            var model = new CommentFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(comment => comment.MapFilterComment())
                    .ToListAsync(cancellationToken),
                FilterParams = @params
            };
            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}