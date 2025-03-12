using Common.Domain;

namespace Domain.UserAgg
{
    public class UserEvent : BaseEntity
    {
        public UserEvent(long eventId)
        {
            EventId = eventId;
        }
        public string UserId { get; set; }
        public long EventId { get; set; }

    }
}
