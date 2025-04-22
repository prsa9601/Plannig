using Common.Domain;


namespace Domain.EventAgg
{
    public class EventUser : BaseEntity
    {
        public EventUser(string UserId)
        {

            this.UserId = UserId;
        }
        public string UserId { get; set; }
        public string CreatorUserId { get; set; }
        public long EventId { get; set; }
    }
}
