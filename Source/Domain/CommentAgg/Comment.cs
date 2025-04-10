using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Domain.CommentAgg
{
    public class Comment : BaseEntity
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public string Text { get; set; }
        public CommentStatus Status { get; set; }
        public DateTime UpdateDate { get; set; }

        public Comment(long userId, long postId, string text)
        {
            NullOrEmptyDomainDataException.CheckString(text, nameof(text));

            UserId = userId;
            PostId = postId;
            Text = text;
            Status = CommentStatus.Pending;
        }

        public void Edit(string text)
        {
            NullOrEmptyDomainDataException.CheckString(text, nameof(text));

            Text = text;
            UpdateDate = DateTime.Now;
        }

        public void ChangeStatus(CommentStatus status)
        {
            Status = status;
            UpdateDate = DateTime.Now;
        }
    }

    public enum CommentStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}