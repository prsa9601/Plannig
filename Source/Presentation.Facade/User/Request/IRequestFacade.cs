using Application.User._RequestBox.Add;
using Application.User._RequestBox.Remove;
using Application.User._RequestBox.RemoveRequestForSender;
using Common.Application;
using Query.User._RequestBox.DTOs;

namespace Presentation.Facade.User.Request
{
    public interface IRequestFacade
    {
        Task<OperationResult> AddRequest(AddRequestFriendCommand command);
        Task<OperationResult> RemoveRequest(RemoveRequestFriendCommand command);
        Task<OperationResult> RemoveRequestForSender(RemoveRequestForSenderCommand command);

        Task<RequestBoxFilterResult?> GetRequestByFilter(RequestBoxFilterParam param);
        Task<RequestDto?> GetRequestById(long id, string userName);
        Task<List<RequestDto>?> GetRequestList(string userName);
    }
}
