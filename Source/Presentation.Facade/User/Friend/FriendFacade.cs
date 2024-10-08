using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.User._Friend.Add;
using Application.User._Friend.Remove;
using Common.Application;
using MediatR;
using Query.User._Friend.DTOs;
using Query.User._Friend.GetListFriendByUserId;

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

        public async Task<List<FriendDto?>> GetFriendsByUserId(string id)
        {
            return await _mediator.Send(new GtListFriendByUserIdQuery(id));
        }

        public async Task<OperationResult> RemoveFriend(RemoveFriendUserCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
