using Common.Domain;
using Domain.SocialMediaAgg.InstagramAgg.Service;
using Domain.SocialMediaAgg.TelegramAgg;

namespace Domain.SocialMediaAgg.InstagramAgg
{
    public class Instagram : BaseEntity
    {
        //Instagram
        public string? InstagramId { get; set; } //InstagramAccountId
        public string? PageId { get; set; } //PageId
        public string accessToken { get; set; } //AccessToken Instagram
        public string InstagramUserName { get; set; }
        public string UserName { get; set; } //AccessToken Instagram
        public string UserId { get; set; }
        public List<Story.Story>? Stories { get; set; } //token Telegram
        public List<Post.Post>? Posts { get; set; } //token Telegram
        public string? Profile { get; set; } //token Telegram
        //public SendMethodInstagram SendMethod { get; set; }
        private Instagram()
        {
            
        }
        public Instagram(string accessToken, string instagramUserName, string userName, string userId, IInstagramService service)
        {
            UserId = userId;
            this.accessToken = accessToken;
            UserName = userName;
            InstagramUserName = instagramUserName;
            Stories = new List<Story.Story>();
            Posts = new List<Post.Post>();
            //SendMethod = sendMethod;, SendMethodInstagram sendMethod
        }
        public void Edit(string accessToken, string userName, string userId, IInstagramService service)
        {
            UserId = userId;
            this.accessToken = accessToken;
            UserName = userName;
            //SendMethod = sendMethod;, SendMethodInstagram sendMethod
        }
        //public void ChangeImage(string imagePath)
        //{
        //    var model = new InstagramProfile(imagePath);
        //    model.InstagramId = InstagramId;
        //    InstagramProfiles.Add(model);
        //}
        //public void RemoveImage(long id)
        //{
        //    var model = InstagramProfiles.FirstOrDefault
        //        (i => i.Id == id);
        //    if (model == null)
        //        throw new Exception("خطای سمت سرور!");
        //    InstagramProfiles.Remove(model);
        //}
        public void ChangeImage(string imageName)
        {
            Profile = imageName;
        }
        public void AddPost(Post.Post post)
        {
            post.InstagramUserName = UserName;
            post.InstagramId = InstagramId;
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
            //story.InstagramUserName = UserName;
            story.InstagramId = Id;
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
