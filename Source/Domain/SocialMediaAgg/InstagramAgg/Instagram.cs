using Common.Domain;
using Domain.SocialMediaAgg.TelegramAgg;

namespace Domain.SocialMediaAgg.InstagramAgg
{
    public class Instagram : BaseEntity
    {
        //Instagram
        public string InstagramId { get; set; } //AccessToken Instagram
        public string accessToken { get; set; } //AccessToken Instagram
        public string UserName { get; set; } //AccessToken Instagram
        public List<Story.Story> Stories { get; set; } //token Telegram
        public List<Post.Post> Posts { get; set; } //token Telegram
        public List<InstagramProfile> InstagramProfiles { get; set; } //token Telegram
        public SendMethodInstagram SendMethod { get; set; }

        public Instagram(string accessToken, string userName, SendMethodInstagram sendMethod)
        {
            this.accessToken = accessToken;
            UserName = userName;
            Stories = new List<Story.Story>();
            Posts = new List<Post.Post>();
            SendMethod = sendMethod;
        }
        public void Edit(string accessToken, string userName, SendMethodInstagram sendMethod)
        {
            this.accessToken = accessToken;
            UserName = userName;
            SendMethod = sendMethod;
        }
        public void ChangeImage(string imagePath)
        {
            var model = new InstagramProfile(imagePath);
            model.TelegramId = InstagramId;
            InstagramProfiles.Add(model);
        }
        public void RemoveImage(long id)
        {
            var model = InstagramProfiles.FirstOrDefault
                (i => i.Id == id);
            if (model == null)
                throw new Exception("خطای سمت سرور!");
            InstagramProfiles.Remove(model);
        }
        public void AddPost(Post.Post post)
        {
            post.InstagramUserName = UserName;
            Posts.Add(post);
        }
        //public void EditPost(Post.Post post)
        //{
        //    Stories.ForEach(i=>i.Id==st);
        //    post.InstagramUserName = UserName;
        //    Posts.Add(post);
        //}
        public long RemovePost(long postId)
        {
            var post = Posts.Where(i => i.Id.Equals(postId)).FirstOrDefault();
            Posts.Remove(post);
            return post.Id;
        }
        public void AddStory(Story.Story story)
        {
            story.InstagramUserName = UserName;
            Stories.Add(story);
        }
        public long RemoveStory(long storyId)
        {
            var story = Stories.Where(i => i.Id.Equals(storyId)).FirstOrDefault();
            Stories.Remove(story);
            return story.Id;
        }

    }
    public enum SendMethodInstagram
    {
        Post,
        Story
    }
}
