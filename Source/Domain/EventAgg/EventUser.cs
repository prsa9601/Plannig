using Common.Domain;


namespace Domain.EventAgg
{
    public class EventUser : BaseEntity
    {
        public EventUser(string userNumber)
        {

            UserNumber = userNumber;
        }
        public string UserNumber { get; set; }
        public long EventId { get; set; }
    }
}
