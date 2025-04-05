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
using Application.User.AddToken;
using Application.User.RemoveToken;
using Query.User._Friend.DTOs;
using Application.User.SetRole;
using Application.User.ChangeActivityUserStatus;
using Application.User.ChangeEmailConfirmedStatus;
using Application.User.ChangePhoneNumberConfirmedStatus;
using Application.User.EditForAdmin;
using Application.User.SetAvatar;
using Application.User.SendVerificationEmailToken;
using Application.User.VerificationEmail;
using Application.User.SendEmailForForgotPassword;

namespace Presentation.Facade.User
{
    public interface IUserFacade
    {
        Task<OperationResult> RegisterUser(RegisterUserCommand command);
        Task<OperationResult> SendVerificationEmailToken(SendVerificationEmailCodeCommand command);
        Task<OperationResult> VerificationEmail(VerificationEmailCommand command);
        Task<OperationResult> EditUser(EditUserCommand command);
        Task<OperationResult> EditUserForAdmin(EditUserForAdminCommand command);
        Task<OperationResult> SetEvent(SetUserEventCommand command);
        Task<OperationResult> LoginUser(UserLoginCommand command);
        Task<OperationResult> LogoutUser(LogoutUserCommand command);
        Task<OperationResult> Delete(DeleteUserCommand command);
        Task<OperationResult> SetAvatar(SetAvatarCommand command);
        Task<OperationResult> AddFriend(AddFriendsUserCommand command);
        Task<OperationResult> SetRole(SetUserRoleCommand command);
        Task<OperationResult> ChangeActivityStatusUser(ChangeActivityUserStatusCommand command);
        Task<OperationResult> ChangeEmailConfirmedUserStatus(ChangeEmailConfirmedUserStatusCommand command);
        Task<OperationResult> ChangePhoneNumberConfirmedStatus(ChangePhoneNumberConfirmedStatusCommand command);
        Task<OperationResult> SendEmailForForgotPassword(SendEmailForForgotPasswordCommand command);
        Task<OperationResult> VerifiedEmailForgotPassword(VerifiedEmailForgotPasswordCommand command);

        Task<OperationResult> RemoveFriend(RemoveFriendUserCommand command);

        Task<UserFilterResult> SearchUser(UserFilterParam param);
        Task<UserDto?> GetUserById(string userId);
        Task<UserFilterResultForAdmin> GetUsersForAdmin(UserFilterParamForAdmin param);
        Task<UserDto?> GetCurrentUser(string Id);
        Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);
        Task<UserDto?> GetUserByUserName(string userName); 
        Task<OperationResult> AddToken(AddUserTokenCommand command);
        Task<OperationResult> RemoveToken(RemoveUserTokenCommand command);
        Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken);

        //Task<UserDto?> GetUserById(long Id);


    }
}
