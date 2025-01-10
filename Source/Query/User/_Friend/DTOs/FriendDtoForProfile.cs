using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Query;
using Common.Query.Filter;

namespace Query.User._Friend.DTOs
{
    public class FriendDtoForProfile : BaseDto
    {
        public string? UserId { get; set; }
        public string? FriendId { get; set; }
        public string? FriendUserName { get; set; }
        //public string? FriendUrl { get; set; }
        public bool IsFriend { get; set; }
        public bool IsSendRequest { get; set; }
        public UserFriendAvatarDto? avatar { get; set; }
    }

    public class FriendDtoForProfileParam : BaseFilterParam
    {
        public string UserName { get; set; } = string.Empty;
        public required string CurrentUserId { get; set; }
    }

    public class FriendDtoForProfileResult : BaseFilter<FriendDtoForProfile, FriendDtoForProfileParam>
    {
    }
}
