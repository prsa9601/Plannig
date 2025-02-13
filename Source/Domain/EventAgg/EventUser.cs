using Common.Domain;


namespace Domain.EventAgg
{
    public class EventUser : BaseEntity
    {
        public EventUser(string userName)
        {

            UserName = userName;
        }
        public string UserName { get; set; }
        public string CreatorUserName { get; set; }
        public long EventId { get; set; }
    }
}
