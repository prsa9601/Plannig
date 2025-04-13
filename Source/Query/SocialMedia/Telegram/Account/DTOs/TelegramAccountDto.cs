using Common.Domain;
using Common.Query;
using Common.Query.Filter;
using Domain.SocialMediaAgg.TelegramAgg;
using Domain.SocialMediaAgg.TelegramAgg.Post;
using System.Reflection.Metadata.Ecma335;

namespace Query.SocialMedia.Telegram.Account.DTOs
{
    public class TelegramAccountDto : BaseDto
    {
        public string Token { get; set; } //token Telegram Bot
        public string Chat_Id { get; set; } //TelegramID
        public string UserName { get; set; } //CreatorUserName
        //public string ChannelName { get; set; } //ChannelName
        public List<TelegramProfileDto> TelegramProfiles { get; set; }
        public List<PostDto> Posts { get; set; }
        public SendMethodTelegram SendMethod { get; set; } //token Telegram
        public TelegramChannelMethod TelegramChannelMethod { get; set; }
    }
    public class TelegramProfileDto : BaseDto
    {
        public string TelegramId { get; set; }
        public string ImageName { get; set; }
    }
    public class PostDto : BaseDto
    {
        public DateTime DateOfPosting { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string VideoName { get; set; }
        public string TelegramUserName { get; set; } //channelAddress Or Group
        //public string Link { get; set; }
        public bool IsSend { get; set; } = false;
        public string postId { get; set; } //InstagramPostId OR TelegramPostId
        //[NotMapped]
        public List<TelegramPostImageDto> Images { get; set; }
        //[NotMapped]
        public List<TelegramPostVideoDto> Videos { get; set; }
    }
    public class TelegramPostVideoDto : BaseDto
    {
        //public DateTime DateOfPosting { get; private set; }
        public string VideoName { get; set; }
        public long PostId { get; set; }
        public int Sequence { get; set; }
        //public string Link { get; set; }
    }
    public class TelegramPostImageDto : BaseDto
    {
        //public DateTime DateOfPosting { get; private set; }
        public string ImageName { get; set; }
        public long PostId { get; set; }
        public int Secuence { get; set; }
        //public string Link { get; set; }
    }
    public class TelegramAccountFilterData : BaseDto
    {

    }
    public class TelegramAccountFilterParam : BaseFilterParam
    {
        public long? TelegramAccountId { get; set; }
        public string? UserName { get; set; }
        public string? Chat_Id { get; set; }//ChanelName OR GroupName
        public TelegramAccountSearchOrderBy? SearchOrderBy { get; set; }

    }
    public class TelegramAccountFilterResult : 
        BaseFilter<TelegramAccountDto, TelegramAccountFilterParam>
    {

    }
    public enum TelegramAccountSearchOrderBy
    {
        //visit,
        latest
    }
}
