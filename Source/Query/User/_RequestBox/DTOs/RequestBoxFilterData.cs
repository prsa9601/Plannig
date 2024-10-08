using Common.Query;
using Common.Query.Filter;

namespace Query.User._RequestBox.DTOs
{
    public class RequestBoxFilterData : BaseDto
    {
        public long Id { get; set; }
        public string UserNameSender { get; set; }
        public string UserNameReceived { get; set; }
        public string SenderId { get; set; }

        public string ReceivedId { get; set; }

    }
    public class RequestBoxFilterParam : BaseFilterParam
    {
        public string UserName { get; set; }
        public filter filter { get; set; }
    }
    public class RequestBoxFilterResult : BaseFilter<RequestBoxFilterData, RequestBoxFilterParam>
    {


    }
    public enum filter 
    {
        SendRequest,
        ReceiveRequest
    }
}
