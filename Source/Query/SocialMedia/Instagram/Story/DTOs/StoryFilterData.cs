using Common.Query.Filter;
using Common.Query;
using Domain.SocialMediaAgg.InstagramAgg;

namespace Query.SocialMedia.Instagram.Story.DTOs
{
    internal class StoryFilterData : BaseDto
    {
    }
    public class InstagramPostFilterData : BaseDto
    {
        public string storyId { get; set; } //InstagramPostId OR TelegramPostId
        public DateTime DateOfPosting { get; set; }
        public string Link { get; set; }
        public bool IsSend { get; set; }
        public string ImageName { get; set; } 
        public string InstagramUserName { get; set; } // UserName or PageName
        public string VideoName { get; set; }
        public StoryImageDto Images { get; set; }
        public StoryVideoDto Videos { get; set; }
        public SendMethodInstagram SendMethod { get; set; }
    }
    public class StoryFilterParam : BaseFilterParam
    {
        public required string InstagramId { get; set; }
        public string? Search { get; set; } = "";
        public StorySearchOrderBy? SearchOrderBy { get; set; }

    }
    public class StoryFilterResult : BaseFilter<StoryDto, StoryFilterParam>
    {
    }

    public enum StorySearchOrderBy
    {
        //visit,
        latest
    }
}
