namespace Domain.SocialMediaAgg.TelegramAgg.Service
{
    public interface ITelegramService 
    {
        Task<bool> CheckBotExist(string token);
        bool ExistAccount(string token, string chatId, string userName);
    }
}
