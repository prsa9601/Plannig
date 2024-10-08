using Common.Domain;

namespace Domain.SocialMediaAgg.TelegramAgg
{
    public class Telegram : BaseEntity
    {
        //Telegram
        public string token { get; set; } //token Telegram
        public string chat_id { get; set; } //TelegramID
        public string UserName { get; set; } //UserName
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
            model.TelegramId = chat_id;
            TelegramProfiles.Add(model);
        }
        public void RemoveImage(long id)
        {
            var model = TelegramProfiles.FirstOrDefault
                (i => i.Id == id );
            if (model == null)
                throw new Exception("خطای سمت سرور!");
            TelegramProfiles.Remove(model);
        }
        public Telegram(string token, string chatId, string userName, List<Post.Post> posts, SendMethodTelegram sendMethod)
        {
            this.token = token;
            chat_id = chatId;
            UserName = userName;
            Posts = new List<Post.Post>();
            SendMethod = sendMethod;
        }
        public void Edit(string token, string chatId, string userName, List<Post.Post> posts, SendMethodTelegram sendMethod)
        {
            this.token = token;
            chat_id = chatId;
            UserName = userName;
            SendMethod = sendMethod;
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
