using Common.Query;

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
    public class UserFriendAvatarDto : BaseDto
    {
        public string UserId { get; set; }
        public Domain.UserAgg.UserAvatar.Avatar Avatar { get; set; }
    }
}
