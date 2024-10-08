using Common.Domain;

namespace Domain.UserAgg
{
    public class UserFriends : BaseEntity
    {
        private UserFriends()
        {
            
        }
        public string CurrentUserId { get; set; }
        public string UserFriendId { get; set; }

        //public UserAvatar AvatarFriend { get; set; }
        ////public long CurrentUserId { get; set; }
        public UserFriends(string userFriendId)
        {
            UserFriendId = userFriendId;
        }
    }

    //REDIS//
}

