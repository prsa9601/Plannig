using Application.User._Friend.Add;
using Application.User._Friend.Remove;
using Application.User.Delete;
using Application.User.Edit;
using Application.User.Login;
using Application.User.Logout;
using Application.User.Register;
using Application.User.SetEvent;
using Common.Application;
using MediatR;
using Query.User.DTOs;
using Query.User.GetByPhoneNumber;
using Query.User.GetByUserName;
using Query.User.GetCurrentUser;

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

        //public async Task<UserDto?> GetUserById(long Id)
        //{
        //    return await _mediator.Send(new GetUserById(Id));
        //}
    }
}
