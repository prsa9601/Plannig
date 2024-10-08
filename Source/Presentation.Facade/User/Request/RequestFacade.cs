using Application.User._RequestBox.Add;
using Application.User._RequestBox.Remove;
using Common.Application;
using MediatR;
using Query.User._RequestBox.DTOs;
using Query.User._RequestBox.GetByFilter;
using Query.User._RequestBox.GetId;
using Query.User._RequestBox.GetList;

namespace Presentation.Facade.User.Request
{
    internal class RequestFacade : IRequestFacade
    {
        private readonly IMediator _mediator;

        public RequestFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> AddRequest(AddRequestFriendCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<RequestBoxFilterResult?> GetRequestByFilter(RequestBoxFilterParam param)
        {
            return await _mediator.Send(new GetRequestByFilterQuery(param));
        }

        public async Task<RequestDto?> GetRequestById(long id, string userName)
        {
            return await _mediator.Send(new GetRequestByIdQuery(id, userName));
        }

        public async Task<List<RequestDto>?> GetRequestList(string userName)
        {
            return await _mediator.Send(new GetRequestListQuery(userName));
        }

        public async Task<OperationResult> RemoveRequest(RemoveRequestFriendCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
