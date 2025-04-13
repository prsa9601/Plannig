using Common.Domain;
using Domain.SocialMediaAgg.TelegramAgg.Service;
using System.Runtime.Versioning;

namespace Domain.SocialMediaAgg.TelegramAgg
{
    public class Telegram : BaseEntity
    {
        //Telegram
        public string Token { get; set; } //token Telegram Bot
        public string Chat_Id { get; set; } //TelegramID
        public string UserName { get; set; } //CreatorUserName
        //public string ChannelName { get; set; } //ChannelName
        public List<TelegramProfile> TelegramProfiles { get; set; } = new List<TelegramProfile>();
        public List<Post.Post> Posts { get; set; }
        public SendMethodTelegram SendMethod { get; set; } //token Telegram
        public TelegramChannelMethod TelegramChannelMethod { get; set; }

        private Telegram()
        {

        }
        public void AddImage(string imagePath)
        {
            var model = new TelegramProfile(imagePath);
            model.TelegramId = Chat_Id;
            TelegramProfiles.Add(model);
        }
        public void RemoveImage(long id)
        {
            var model = TelegramProfiles.FirstOrDefault
                (i => i.Id == id);
            if (model == null)
                throw new Exception("خطای سمت سرور!");
            TelegramProfiles.Remove(model);
        }
        public Telegram(string? token, string chatId, string userName, bool DefaultToken
            , ITelegramService service)
        {
            Chat_Id = chatId;
            UserName = userName;

            if (!DefaultToken)
            {
                ExistAccount(token, chatId, userName, service);

                Task.Run(async () => await GuardAsync(token!, service))
                    .GetAwaiter().GetResult();
                if (token == null)
                    throw new ArgumentNullException(nameof(token));
                Token = token;
            }
            else
            {
                ExistAccount("8034643778:AAEEbUXrPRlpLtcPIPQulOxWMzCRVAQHbKw", chatId, userName, service);

                Token = "8034643778:AAEEbUXrPRlpLtcPIPQulOxWMzCRVAQHbKw";
            }

            Posts = new List<Post.Post>();
        }
        public void Edit(string? token, string chatId, string userName, bool DefaultToken
            , ITelegramService service)
        {
            Chat_Id = chatId;
            UserName = userName;

            if (!DefaultToken)
            {
                Task.Run(async () => await GuardAsync(token!, service))
                    .GetAwaiter().GetResult();
            
                if (token == null)
                    throw new ArgumentNullException(nameof(token));
                Token = token;
            }
            else
            {
                Token = "8034643778:AAEEbUXrPRlpLtcPIPQulOxWMzCRVAQHbKw";
            }

            Posts = new List<Post.Post>();
        }

        public void AddPost(Post.Post post)
        {
            post.TelegramUserName = UserName;
            Posts.Add(post);
        }
        public long RemovePost(long postId)
        {
            var post = Posts.Where(i => i.Id.Equals(postId)).FirstOrDefault();
            Posts.Remove(post);
            return post.Id;
        }
        //public void AddPost(List<Post.Post> post)
        //{
        //    Posts.AddRange(post);
        //}
        private void ExistAccount(string token, string chatId,
            string userName, ITelegramService service)
        {
            if(service.ExistAccount(token, chatId, userName) == true)
            {
                throw new Exception("حسابی با همین اطلاعات از قبل وجود دارد!");
            }
        }
        private async Task GuardAsync(string token, ITelegramService service)
        {
            if (await service.CheckBotExist(token) == false)
            {
                throw new Exception("Bot does not exist (maybe you entered the wrong BotToken).");
            }
        }
    }
    public enum SendMethodTelegram
    {
        SendImage,
        SendText,
        SendVideo
    }

    public enum TelegramChannelMethod
    {
        Channel,
        Group
    }
}
