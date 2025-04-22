using Common.Query.Filter;
using Query.User._Package.UsersPackagesDTOs;

namespace Query.SocialMedia.Instagram.Account.DTOs
{
    public class InstagramAccountFilterParam : BaseFilterParam
    {
        public string? UserName { get; set; }
        //public string PhoneNumbeer { get; set; }
        public PostInstagramSearchOrderBy? SearchOrderBy { get; set; }
        //public string? Title { get; set; }
    }
    public enum PostInstagramSearchOrderBy
    {
        //visit,
        latest
    }
    public class InstagramAccountFilterResult : BaseFilter<InstagramAccountDto, InstagramAccountFilterParam>
    {
    }
}
