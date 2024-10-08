using Application.Event.Add;
using Application.Event.Delete;
using Application.Event.Edit;
using Application.User._Friend.Add;
using Application.User._Friend.Remove;
using Application.User.Delete;
using Application.User.Edit;
using Application.User.Login;
using Application.User.Register;
using Application.User.SetEvent;
using Common.Application;
using Query.User.DTOs;
using System.Security.Claims;
using Application.User.Logout;

namespace Presentation.Facade.User
{
    public interface IUserFacade
    {
        Task<OperationResult> RegisterUser(RegisterUserCommand command);
        Task<OperationResult> EditUser(EditUserCommand command);
        Task<OperationResult> SetEvent(SetUserEventCommand command);
        Task<OperationResult> LoginUser(UserLoginCommand command);
        Task<OperationResult> LogoutUser(LogoutUserCommand command);
        Task<OperationResult> Delete(DeleteUserCommand command);
        Task<OperationResult> AddFriend(AddFriendsUserCommand command);
        Task<OperationResult> RemoveFriend(RemoveFriendUserCommand command);

        Task<UserDto?> GetCurrentUser(string Id);
        Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);
        Task<UserDto?> GetUserByUserName(string userName);
        //Task<UserDto?> GetUserById(long Id);


    }
}
