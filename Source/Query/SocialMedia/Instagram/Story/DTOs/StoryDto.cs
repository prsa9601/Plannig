using Common.Domain;
using Common.Query;
using Common.Query.Filter;
using Domain.SocialMediaAgg.InstagramAgg;
using Domain.SocialMediaAgg.InstagramAgg.Story;

namespace Query.SocialMedia.Instagram.Story.DTOs
{
    public class StoryDto : BaseDto
    {
        public long InstagramId { get; set; } //InstagramPostId OR TelegramPostId
        public string InstagramStoryId { get; set; } //InstagramPostId OR TelegramPostId

        public DateTime DateOfPosting { get; set; }
        public string Link { get; set; }
        public bool IsSend { get; set; }
        //public string ImageName { get;  set; }
        public string InstagramUserName { get; set; }
        //public string InstagramId { get; set; }
        //public string VideoName { get; set; }
        public StoryImageDto? Images { get; set; }
        public StoryVideoDto? Videos { get; set; }
        public SendMethodInstagramForPost SendMethod { get; set; }
    }

    public class StoryImageDto : BaseDto
    {
        // public DateTime DateOfPosting { get; private set; }
        public string PictureName { get; set; }
       // public long StoryId { get; set; }
        public string? Link { get; set; }
    }

    public class StoryVideoDto : BaseDto
    {
        public string VideoPath { get; set; }
        public string? Link { get; set; }
        //public long StoryId { get; set; }
    }



}