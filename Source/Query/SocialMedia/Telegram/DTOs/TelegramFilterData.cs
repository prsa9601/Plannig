using Common.Query;
using Common.Query.Filter;
using Domain.SocialMediaAgg.TelegramAgg;

namespace Query.SocialMedia.Telegram.DTOs
{
    internal class TelegramFilterData : BaseDto
    {
        public string ChannelName { get; set; }
        public string UserName { get; set; }
        public TelegramChannelMethod ChannelMethod { get; set; }
        public SendMethodTelegram SendMethod { get; set; } 

    }
    public class TelegramFilterParam : BaseFilterParam
    {
        public long Id { get; set; }
        public string? Search { get; set; } = "";
        public PostSearchOrderBy? SearchOrderBy { get; set; }
        public string? Title { get; set; }
    }
    public class TelegramFilterResult : BaseFilter<TelegramDto, TelegramFilterParam>
    {
   
    }

    //public enum TelegramChannelMethod
    //{
    //    Channel,
    //    Group
    //}
    public enum PostSearchOrderBy
    {
        //visit,
        latest
    }
}
