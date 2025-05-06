using Common.Query.Filter;
using Query.User._Package.UsersPackagesDTOs;

namespace Query.SocialMedia.Instagram.Account.DTOs
{
    public class InstagramAccountFilterParam : BaseFilterParam
    {
        public string? UserName { get; set; }
        //public string PhoneNumbeer { get; set; }
        public string? InstagramUserName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public PostInstagramAccountSearchOrderBy? SearchOrderBy { get; set; }
        //public string? Title { get; set; }
    }
    public enum PostInstagramAccountSearchOrderBy
    {
        //visit,
        latest
    }
    public class InstagramAccountFilterResult : BaseFilter<InstagramAccountDto, InstagramAccountFilterParam>
    {
    }
}
