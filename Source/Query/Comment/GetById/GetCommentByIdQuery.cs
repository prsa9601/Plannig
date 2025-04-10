using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Comment.DTOs;

namespace Query.Comment.GetById
{
    public class GetCommentByIdQuery : IQuery<CommentDto?>
    {
        public long CommentId { get; set; }
    }
    public class GetCommentByIdQueryHandler : IQueryHandler<GetCommentByIdQuery, CommentDto?>
    {
        private readonly PlanningContext _context;

        public GetCommentByIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<CommentDto?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(f => f.Id == request.CommentId, cancellationToken: cancellationToken);
            return comment.Map();
        }
    }
}
