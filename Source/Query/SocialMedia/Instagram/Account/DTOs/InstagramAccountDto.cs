using Common.Query;
using Domain.SocialMediaAgg.InstagramAgg;
using Domain.SocialMediaAgg.InstagramAgg.Post;
using Domain.SocialMediaAgg.InstagramAgg.Story;

namespace Query.SocialMedia.Instagram.Account.DTOs
{
    public class InstagramAccountDto : BaseDto
    {
        public string InstagramId { get; set; } //InstagramAccountId
        public string PageId { get; set; } //PageId
        public string accessToken { get; set; } //AccessToken Instagram
        public string UserName { get; set; } //AccessToken Instagram
        public List<InstagramAccountStoryDto>? Stories { get; set; } //token Telegram
        public List<InstagramAccountPostDto>? Posts { get; set; } //token Telegram
        public string Profile { get; set; } //image name
        //public SendMethodInstagram SendMethod { get; set; }
    }
    //public class InstagramAccountProfileDto : BaseDto
    //{
    //    public string InstagramId { get; set; }
    //    public string ImageName { get; set; }
    //}
    public class InstagramAccountPostDto:BaseDto
    {
        public DateTime DateOfPosting { get;  set; }
        public string Description { get;  set; }
        //public string ImageName { get;  set; }
        public string PostId { get; set; }
        //public string VideoName { get;  set; }
        public string InstagramUserName { get; set; }
        public string Link { get;  set; }
        public bool IsSend { get;  set; } = false;
        public string InstagramId { get;  set; }
        public List<InstagramPostImageDto> Images { get;  set; }
        public List<InstagramPostVideoDto> Videos { get;  set; }
    }
    public class InstagramAccountStoryDto : BaseDto
    {
        public string storyId { get;  set; }
        public DateTime DateOfPosting { get;  set; }
        public string Link { get;  set; }
        public bool IsSend { get;  set; }
        public string ImageName { get;  set; }
        public string InstagramUserName { get; set; }
        public string InstagramId { get; set; }
        public InstagramStoryImageDto Images { get;  set; }
        public InstagramStoryVideoDto Videos { get;  set; }
    }
    public class InstagramStoryImageDto : BaseDto
    {
        public string PictureName { get;  set; }
        public string Link { get;  set; }
    }
    public class InstagramStoryVideoDto : BaseDto
    {
        public string VideoPath { get;  set; }
        public string Link { get;  set; }
    }
    public class InstagramPostImageDto : BaseDto
    {
        public string ImageName { get;  set; }
        public long PostId { get;  set; }
        public int Sequence { get;  set; }
        public string Link { get;  set; } 
    }
    public class InstagramPostVideoDto : BaseDto
    {
        public string VideoName { get;  set; }
        public long PostId { get;  set; }
        public int Sequence { get;  set; }
        public string Link { get;  set; }
    }
}