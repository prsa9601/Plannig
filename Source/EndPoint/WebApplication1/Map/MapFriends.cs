using Query.User.DTOs;

namespace Planning.Api.Map
{
    public static class MapFriends
    {
        public static List<FriendsDto> FriendsMap(this List<Domain.UserAgg.UserFriends> user)
        {
            var friends = new List<FriendsDto>();
            foreach (var item in user)
            {
                var model = new FriendsDto()
                {
                    CreationDate = item.CreationDate,
                    CurrentUserId = item.CurrentUserId,
                    Id = item.Id,
                    UserFriend = item.UserFriendId
                };
                friends.Add(model);
            }
            return friends;
        }
    }
}
