using Application.User._Friend.Add;
using Application.User._Friend.Remove;
using Common.Application;
using MediatR;
using Query.User._Friend.DTOs;
using Query.User._Friend.GetFiendFilterForProfile;
using Query.User._Friend.GetListFriendByUserId;
using Query.User._Friend.GetListFriendByUserIdForProfile;
using Query.User._Friend.GetListFriendByUserName;
using Query.User.DTOs;
using Query.User.SearchUser;

namespace Presentation.Facade.User.Friend
{
    internal class FriendFacade : IFriendFacade
    {
        private readonly IMediator _mediator;
        public FriendFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> AddFriend(AddFriendsUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<List<FriendDto?>> GetFriendsByUserName(string userName)
        {
            return await _mediator.Send(new GtListFriendByUserNameQuery(userName));
        }
        public async Task<List<FriendDto?>> GetFriendsByUserId(string id)
        {
            return await _mediator.Send(new GetListFriendByUserIdQuery(id));
        }


        public async Task<UserFriendFilterResult> GetFriendsByUserIdForProfile(UserFriendFilterParam param)
        {
            return await _mediator.Send(new GetListFriendByUserIdForProfileQuery(param));
        }

        public async Task<FriendDtoForProfileResult?> GetFriendFilterForProfileQuery(FriendDtoForProfileParam param)
        {
            return await _mediator.Send(new GetFriendFilterForProfileQuery(param));
        }

        public async Task<OperationResult> RemoveFriend(RemoveFriendUserCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
