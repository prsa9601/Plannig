using Query.SocialMedia.Telegram.DTOs;

namespace Query.SocialMedia.Telegram
{
    public static class TelegramMapper
    {
        public static TelegramDto? MapTelegram(this Domain.SocialMediaAgg.TelegramAgg.Telegram? model)
        {
            return new TelegramDto() 
            {
                Id= model.Id,
                UserName= model.UserName,
                accessToken = model.token,
                CreationDate = model.CreationDate,
                TelegramName = model.chat_id,
                SendMethod = model.SendMethod,
                ChannelMethod = model.TelegramChannelMethod
            };
        }
    }
}
