using Common.Query;
using Common.Query.Filter;
using Query.User.DTOs;

namespace Query.User._Friend.DTOs
{
    public class FriendDto : BaseDto
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public string FriendUserName { get; set; }
        public string FriendUrl { get; set; }

        public UserFriendAvatarDto avatar { get; set; }
    }
    public class FriendData : BaseDto
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public UserFriendAvatarDto avatar { get; set; }
    }
   
    public class UserFriendAvatarDto : BaseDto
    {
        public string UserId { get; set; }
        public Domain.UserAgg.UserAvatar.Avatar Avatar { get; set; }
    } 

    public class UserFriendFilterParam : BaseFilterParam
    {
        public string UserName { get; set; } = string.Empty;
        public string CurrentUserId { get; set; } = string.Empty;
    }
    public class UserFriendFilterResult : BaseFilter<FriendDto, UserFriendFilterParam>
    {
    }
    public class SearchFriendForEventData : BaseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        public UserFriendAvatarDto avatar { get; set; }
    }

  

    public class SearchFriendForEventFilterParam : BaseFilterParam
    {
        public string? UserName { get; set; } = string.Empty;
        public string CurrentUserId { get; set; } = string.Empty;
    }
    public class SearchFriendForEventFilterResult : BaseFilter<SearchFriendForEventData, SearchFriendForEventFilterParam>
    {
    }
}
