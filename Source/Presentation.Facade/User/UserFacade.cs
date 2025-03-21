using Application.User._Friend.Add;
using Application.User._Friend.Remove;
using Application.User.AddToken;
using Application.User.ChangeActivityUserStatus;
using Application.User.ChangeEmailConfirmedStatus;
using Application.User.ChangePhoneNumberConfirmedStatus;
using Application.User.Delete;
using Application.User.Edit;
using Application.User.Login;
using Application.User.Logout;
using Application.User.Register;
using Application.User.RemoveToken;
using Application.User.SetEvent;
using Application.User.SetRole;
using Common.Application;
using Common.Application.SecurityUtil;
using Common.Domain.ValueObjects;
using MediatR;
using Query.User._Friend.DTOs;
using Query.User.DTOs;
using Query.User.GetById;
using Query.User.GetByPhoneNumber;
using Query.User.GetByUserName;
using Query.User.GetCurrentUser;
using Query.User.SearchUser;
using Query.User.UserFilterForAdmin;
using Query.User.UserTokens.GetByJwtToken;

namespace Presentation.Facade.User
{
    public class UserFacade : IUserFacade
    {
        private readonly IMediator _mediator;

        public UserFacade(IMediator mediator)
        {
            _mediator = mediator;
        }
        //public async Task<OperationResult> CreateUser(AddUse instagramCommand)
        //{
        //    return await _mediator.Send(instagramCommand);
        //}

      
        //public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand instagramCommand)
        //{
        //    //await _cache.RemoveAsync(CacheKeys.User(instagramCommand.UserId));
        //    return await _mediator.Send(instagramCommand);
        //}

     
        public async Task<OperationResult> EditUser(EditUserCommand command)
        {
            var result = await _mediator.Send(command);
            //if (result.Status == OperationResultStatus.Success)
            //await _cache.RemoveAsync(CacheKeys.User(instagramCommand.UserId));
            return result;
        }

        //public async Task<UserDto?> GetUserById(long userId)
        //{
        //    //return await _cache.GetOrSet(CacheKeys.User(userId), () =>
        //    //{
        //    return await _mediator.Send(new GetUserByIdQuery(userId));
        //    //});
        //}


        public async Task<OperationResult> SetRole(SetUserRoleCommand command)
        {
            return await _mediator.Send(command);
        }


        //public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
        //{
        //    return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
        //}

        public async Task<OperationResult> RegisterUser(RegisterUserCommand command)
        {
            return await _mediator.Send(command);
        }
        
        public async Task<OperationResult> SetEvent(SetUserEventCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> LogoutUser(LogoutUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Delete(DeleteUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> LoginUser(UserLoginCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<UserDto?> GetUserById(string userId)
        {
            return await _mediator.Send(new GetUserByIdQuery(userId));

        }

        public async Task<UserFilterResult> SearchUser(UserFilterParam param)
        {
            return await _mediator.Send(new SearchUserFilterQuery(param));
        }


        public async Task<UserDto?> GetCurrentUser(string Id)
        {
            return await _mediator.Send(new GetCurrentUserQuery(Id));
        }

        public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
        {
            return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
        }
        
        public async Task<UserDto?> GetUserByUserName(string userName)
        {
            return await _mediator.Send(new GetUserByUserNameQuery(userName));
        }

        public async Task<OperationResult> AddFriend(AddFriendsUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> RemoveFriend(RemoveFriendUserCommand command)
        {
            return await _mediator.Send(command);
        }
        public async Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken)
        {
            var hashJwtToken = Sha256Hasher.Hash(jwtToken);
            //return await _cache.GetOrSet(CacheKeys.UserToken(hashJwtToken), () =>
            //{
            return await _mediator.Send(new GetUserTokenByJwtTokenQuery(hashJwtToken));
            //});
        }
        public async Task<OperationResult> AddToken(AddUserTokenCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> RemoveToken(RemoveUserTokenCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Status != OperationResultStatus.Success)
                return OperationResult.Error();

            //await _cache.RemoveAsync(CacheKeys.UserToken(result.Data));
            return OperationResult.Success();
        }

        public async Task<UserFilterResultForAdmin> GetUsersForAdmin(UserFilterParamForAdmin param)
        {
            return await _mediator.Send(new GetUserFilterForAdminQuery(param));
        }

        public async Task<OperationResult> ChangeActivityStatusUser(ChangeActivityUserStatusCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> ChangeEmailConfirmedUserStatus(ChangeEmailConfirmedUserStatusCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> ChangePhoneNumberConfirmedStatus(ChangePhoneNumberConfirmedStatusCommand command)
        {
            return await _mediator.Send(command);
        }

        //public async Task<UserDto?> GetUserById(long Id)
        //{
        //    return await _mediator.Send(new GetUserById(Id));
        //}
    }
}
