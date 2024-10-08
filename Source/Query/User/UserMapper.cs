using Infrastructure.Persistent.Ef;
using Query.User.DTOs;

namespace Query.User
{
    public static class UserMapper
    {
        public static UserDto? Map(this Domain.UserAgg.User? user, PlanningContext context)
        {
            var model = new UserDto()
            {
                Email = user.Email,
                CreationDate = user.CreationDate,
                Family = user.Family,
                friends = user.friends.FriendsMap(context),
                Id=user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                avatar = user.Id.MapAvatar(context),
            };
            return model;
        }
        public static List<FriendsDto> FriendsMap(this List<Domain.UserAgg.UserFriends> user, PlanningContext context)
        {
            var friends = new List<FriendsDto>();
            foreach (var item in user)
            {
                var model = new FriendsDto()
                {
                    CreationDate = item.CreationDate,
                    CurrentUserId = item.CurrentUserId,
                    Id = item.Id,
                    UserFriend = item.UserFriendId,
                    avatar = item.UserFriendId.MapAvatar(context),
                };
                friends.Add(model);
            }
            return friends;
        }
        public static UserAvatarDto MapAvatar(this string id, PlanningContext context)
        {
            var avatar = context.Users.Where(i => i.Id == id).Select(i => i.Avatar).FirstOrDefault();
            return new UserAvatarDto()
            {
                Id = avatar.Id,
                Avatar = avatar.avatar,
                CreationDate = avatar.CreationDate,
                UserId = avatar.UserId,
            };
        }
    }
}
