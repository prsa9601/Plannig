using Common.Query.Filter;
using Common.Query;
using Domain.SocialMediaAgg.InstagramAgg;
using Query.SocialMedia.Instagram.Story.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.SocialMedia.Instagram.Post.DTOs
{
    public class PostFilterData
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
        public class InstagramFilterResult : BaseFilter<InstagramFilterData, InstagramFilterParam>
        {
        }

        public class InstagramPostFilterData : BaseDto
        {
            public DateTime DateOfPosting { get; set; }
            //public string Picture { get; private set; }
            public string Description { get; set; }
            public string ImageName { get; set; }
            public string Link { get; set; }
            public string InstagramUserName { get; set; } // UserName or PageName
            public string? InstagramPostId { get; set; } // UserName or PageName

            //public string Slug { get; set; }
            public bool IsSend { get; set; }
            public string postId { get; set; } //InstagramPostId OR TelegramPostId
            public List<PostVideoDto> Videos { get; set; }
            public List<PostImageDto> Images { get; set; }
        }
        public class InstagramPostFilterParam : BaseFilterParam
        {
            public required long InstagramId { get; set; }
            public string? Search { get; set; } 
            public InstagramPostSearchOrderBy? InstagramPostSearchOrderBy { get; set; }

        }
        public class InstagramPostFilterResult : BaseFilter<InstagramPostFilterData, InstagramPostFilterParam>
        {
        }

        public enum PostSearchOrderBy
        {
            //visit,
            latest
        }
        public enum InstagramPostSearchOrderBy
        {
            //visit,
            latest
        }
    }
}
