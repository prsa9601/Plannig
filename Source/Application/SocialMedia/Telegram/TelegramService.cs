using Domain.SocialMediaAgg.TelegramAgg.Repository;
using Domain.SocialMediaAgg.TelegramAgg.Service;
using Telegram.Bot;

namespace Application.SocialMedia.Telegram
{
    public class TelegramService : ITelegramService
    {
        private readonly TelegramBotClient _botClient;
        private readonly ITelegramRepository _repository;
        public TelegramService(TelegramBotClient botClient, ITelegramRepository repository)
        {
            _botClient = botClient;
            _repository = repository;
        }

        public async Task<bool> CheckBotExist(string token)
        {
            try
            {
                var bot = new TelegramBotClient(token);
                var me = await bot.GetMeAsync();
                return me != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExistAccount(string token, string chatId, string userName)
        {
            return _repository.Exists(i => i.Chat_Id.Equals(chatId)
            && i.Token.Equals(token) && i.UserName.Equals(userName));
        }
    }
}
