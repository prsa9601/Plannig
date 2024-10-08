using Common.Domain;

namespace Domain.UserAgg
{
    public class Request : BaseEntity
    {
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        
        public string Description { get; set; }
        public string Title { get; set; }

     
        public Request(string senderId, string recipientId, string description, string title)
        {
            SenderId = senderId;
            RecipientId = recipientId;
           
            Description = description;
            Title = title;
        }
        //public Request(long senderId, long recipientId, string description, string title)
        //{
        //    SenderId = senderId;
        //    RecipientId = recipientId;
         
        //    Description = description;
        //    Title = title;
        //}
    }
}
//public class InBox
//{
//    public bool Access { get; set; } = false;
//}