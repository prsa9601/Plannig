using Common.Query.Filter;
using Common.Query;
using Domain.SocialMediaAgg.InstagramAgg;
using Query.SocialMedia.Instagram.Post.DTOs;
using Query.SocialMedia.Instagram.Story.DTOs;

namespace Query.SocialMedia.Instagram.DTOs
{
    public class InstagramFilterData : BaseDto
    {
        public string accessToken { get; set; } //AccessToken Instagram
        public List<StoryDto> Stories { get; set; } //token Telegram
        public List<PostDto> Posts { get; set; } //token Telegram
        public SendMethodInstagramForPost SendMethod { get; set; }
    }
    public class InstagramFilterParam : BaseFilterParam
    {
        public long Id { get; set; }
        public string? Search { get; set; } = "";
        public PostSearchOrderBy? SearchOrderBy { get; set; }
        public string? Title { get; set; }
    }
    public class InstagramFilterResult : BaseFilter<InstagramDto, InstagramFilterParam>
    {
    }

  
    public enum PostSearchOrderBy
    {
        //visit,
        latest
    }
}