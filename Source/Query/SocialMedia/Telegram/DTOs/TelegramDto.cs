using Common.Query;
using Domain.SocialMediaAgg.InstagramAgg;
using Domain.SocialMediaAgg.TelegramAgg;

namespace Query.SocialMedia.Telegram.DTOs
{
    public class TelegramDto : BaseDto
    {
        public string accessToken { get; set; } //AccessToken Telegram
        public string UserName { get; set; }
        public string TelegramName { get; set; }
        public SendMethodTelegram SendMethod { get; set; }
        public TelegramChannelMethod ChannelMethod { get; set; }

    }
}
