using Common.Query;

namespace Query.User._RequestBox.DTOs
{
    public class RequestDto : BaseDto
    {
        public string SenderId { get; set; }
        public string RecipientId { get; set; }

        public string Description { get; set; }
        public string Title { get; set; }
    }
}
