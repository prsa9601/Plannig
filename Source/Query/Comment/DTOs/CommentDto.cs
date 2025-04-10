using Common.Query;
using Domain.CommentAgg;

namespace Query.Comment.DTOs
{
    public class CommentDto : BaseDto
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public string Text { get; set; }
        public CommentStatus Status { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
