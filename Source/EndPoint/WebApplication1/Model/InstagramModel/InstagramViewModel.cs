using Common.Application;
using Common.Query.Filter;
using Query.SocialMedia.Instagram.Account.DTOs;

namespace Planning.Api.Model.InstagramModel
{
    public class InstagramViewModel
    {
        public class AddPostInstagramViewModel: IBaseCommand
        {
            public long InstagramAccountId { get; set; } 
            public string DateOfPosting { get; set; }
            public string Link { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public List<IFormFile>? Images { get; set; }
            public List<IFormFile>? Videos { get; set; }
        }
        public class AddInstagramAccountCommandViewModel : IBaseCommand
        {
            //public string InstagramId { get; set; } //InstagramAccountId
            //public string PageId { get; set; } //PageId
            public string accessToken { get; set; } //AccessToken Instagram
            public string InstagramUserName { get; set; } //AccessToken Instagram
            public IFormFile Profile { get; set; } //token Telegram
        }
        public class InstagramAccountFilterParamViewModel : BaseFilterParam
        {
            //public string PhoneNumbeer { get; set; }
            public string? InstagramUserName { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
            public PostInstagramAccountSearchOrderBy? SearchOrderBy { get; set; }
            //public string? Title { get; set; }
        }
    }
}
