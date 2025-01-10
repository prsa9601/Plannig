 using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User._Friend.DTOs;
using Query.User.DTOs;

namespace Query.User._Friend
{
    public static class FriendMap
    {
        public static UserFriendAvatarDto MapFriendAvatar(this string id, PlanningContext _context)
        {
            var avatar = _context.Users.Where(i => i.Id == id).Select(i => i.Avatar).FirstOrDefault();
            return new UserFriendAvatarDto()
            {
                Id = avatar.Id,
                Avatar = avatar.avatar,
                CreationDate = avatar.CreationDate,
                UserId = avatar.UserId,
            };
        }
        //public static FriendDtoForProfile MapFriend(this Domain.UserAgg.User user, string CurrentUserId, PlanningContext _context)
        //{
        //    return new FriendDtoForProfile()
        //    {
        //        UserId = user.Id,
        //        FriendUserName = user.UserName,
        //        CreationDate = user.CreationDate,
        //        FriendId = CurrentUserId,
        //        IsFriend = false

        //    };
        //}
    }
}
