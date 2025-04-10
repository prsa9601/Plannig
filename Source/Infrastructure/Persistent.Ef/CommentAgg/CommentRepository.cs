using Domain.CommentAgg.Repository;
using Infrastructure._Utilities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistent.Ef.CommentAgg
{
    public class CommentRepository : BaseRepository<Domain.CommentAgg.Comment>, ICommentRepository
    {
        public CommentRepository(PlanningContext context) : base(context)
        {
        }

        public async Task<bool> DeleteComment(long commentId)
        {
            var comment = await Context.Comments.FirstOrDefaultAsync(i => i.Id == commentId);
            if (comment == null)
                return false;
            Context.Comments.Remove(comment);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
