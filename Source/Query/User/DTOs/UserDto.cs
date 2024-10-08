using Common.Query;
using Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.User.DTOs
{
    public class UserDto :BaseDto
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public UserAvatarDto avatar { get; set; }

        public List<FriendsDto> friends { get; set; }
    }
    public class FriendsDto :BaseDto
    {
        public string CurrentUserId { get; set; }
        public string UserFriend { get; set; }

        public UserAvatarDto avatar { get; set; }

    }
    public class UserAvatarDto : BaseDto
    {
        public string UserId { get; set; }
        public Domain.UserAgg.UserAvatar.Avatar Avatar { get; set; }
    }
}
