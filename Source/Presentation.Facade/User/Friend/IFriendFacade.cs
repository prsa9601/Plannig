using Application.User._Friend.Add;
using Application.User._Friend.Remove;
using Common.Application;
using Query.User._Friend.DTOs;

namespace Presentation.Facade.User.Friend
{
    public interface IFriendFacade
    { 
        Task<OperationResult> AddFriend(AddFriendsUserCommand command);
        Task<OperationResult> RemoveFriend(RemoveFriendUserCommand command);
        Task<List<FriendDto?>> GetFriendsByUserId(string UserName);
    }
}
