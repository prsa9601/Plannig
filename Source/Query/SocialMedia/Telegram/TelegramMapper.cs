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
                accessToken = model.Token,
                CreationDate = model.CreationDate,
                TelegramName = model.Chat_Id,
                SendMethod = model.SendMethod,
                ChannelMethod = model.TelegramChannelMethod
            };
        }
    }
}
