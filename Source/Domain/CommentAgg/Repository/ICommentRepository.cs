using Common.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommentAgg.Repository
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<bool> DeleteComment(long commentId);
    }
}
