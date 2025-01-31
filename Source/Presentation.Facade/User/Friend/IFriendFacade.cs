using Application.User._Friend.Add;
using Application.User._Friend.Remove;
using Common.Application;
using Query.User._Friend.DTOs;
using Query.User._Friend.GetListFriendByUserId;
using Query.User._Friend.GetListFriendByUserIdForProfile;

namespace Presentation.Facade.User.Friend
{
    public interface IFriendFacade
    { 
        Task<OperationResult> AddFriend(AddFriendsUserCommand command);
        Task<OperationResult> RemoveFriend(RemoveFriendUserCommand command);
        Task<List<FriendDto?>> GetFriendsByUserName(string userName);
        Task<List<FriendDto?>> GetFriendsByUserId(string id);
        Task<UserFriendFilterResult> GetFriendsByUserIdForProfile(UserFriendFilterParam param);
        Task<SearchFriendForEventFilterResult> SearchFriendForEvent(SearchFriendForEventFilterParam param);
        Task<FriendDtoForProfileResult?> GetFriendFilterForProfileQuery(FriendDtoForProfileParam param);
    }
}
