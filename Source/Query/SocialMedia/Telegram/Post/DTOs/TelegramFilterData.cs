using Common.Query.Filter;
using Common.Query;

namespace Query.SocialMedia.Telegram.Post.DTOs
{
    public class TelegramFilterData : BaseDto
    {
        public string token { get; set; } //token Telegram
        public string chat_id { get; set; } //TelegramID
        public string UserName { get; set; } //token Telegram
        public List<PostDto> Posts { get; set; }
    }
    public class TelegramFilterParam : BaseFilterParam
    {
        public long Id { get; set; }
        public string? Search { get; set; } = "";
        public TelegramSearchOrderBy? SearchOrderBy { get; set; }
        public string? Title { get; set; }
    }
    public class TelegramPostFilterResult : BaseFilter<PostDto, TelegramFilterParam>
    {
    }

    public enum TelegramSearchOrderBy
    {
        //visit,
        latest
    }
}