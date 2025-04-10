using Common.Query;
using Domain.CommentAgg;

namespace Query.Comment.DTOs
{
    public class CommentFilterData : BaseDto
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public string Text { get; set; }
        public CommentStatus Status { get; set; }
        public DateTime UpdateDate { get; set; }
    }
    //public class CommentFilterDataProduct : BaseDto
    //{
    //    public long UserId { get; set; }
    //    public string userAvatar { get; set; }
    //    public string userName { get; set; }
    //    public long PostId { get; set; }
    //    public string Text { get; set; }
    //    public CommentStatus Status { get; set; }
    //    public DateTime UpdateDate { get; set; }
    //}
}
