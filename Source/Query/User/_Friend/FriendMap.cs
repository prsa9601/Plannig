using Infrastructure.Persistent.Ef;
using Query.User._Friend.DTOs;

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
    }
}
