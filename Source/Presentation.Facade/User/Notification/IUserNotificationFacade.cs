using Application.User._Notification.Admin.Add;
using Application.User._Notification.Admin.Remove;
using Application.User._Notification.Admin.RemoveAll;
using Application.User._Notification.MarkAsRead;
using Application.User._Notification.RemoveAllForUser;
using Application.User._Notification.RemoveForUser;
using Common.Application;
using Domain.UserAgg;
using MediatR;
using Query.User._Notification.DTOs;
using Query.User._Notification.GetByFilter;
using Query.User._Notification.GetByFilterForAdmin;
using Query.User._Notification.GetByFilterForLayout;
using Query.User._Notification.GetById;
using Query.User._Notification.GetUserNames;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Presentation.Facade.User.Notification
{
    public interface IUserNotificationFacade
    {
        Task<OperationResult> Add(AddUserNotificationCommand command);
        Task<OperationResult> MarkAsRead(MarkUserNotificationAsReadCommand command);
        Task<OperationResult> Remove(RemoveUserNotificationCommand command);
        Task<OperationResult> RemoveAll(RemoveAllUserNotificationCommand command);
        Task<OperationResult> RemoveForUser(RemoveUserNotificationForUserCommand command);
        Task<OperationResult> RemoveAllForUser(RemoveAllUserNotificationForUserCommand command);
        Task<UserNotificationDto?> GetById(long UserNotificationId, string UserId);
        Task<UserNotificationFilterResult> GetByFilter(UserNotificationFilterParam param);
        Task<UserNotificationFilterResult> GetByFilterForLayout(UserNotificationFilterParam param);
        Task<UserNotificationFilterResultForAdmin> GetByFilterForAdmin(UserNotificationFilterParamForAdmin param);
        Task<Dictionary<string, string>> GetUserNamesForAdmin();
    }
    public class UserNotificationFacade : IUserNotificationFacade
    {
        private readonly IMediator _mediator;

        public UserNotificationFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Add(AddUserNotificationCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Remove(RemoveUserNotificationCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> RemoveAll(RemoveAllUserNotificationCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<UserNotificationDto?> GetById(long UserNotificationId, string UserId)
        {

            return await _mediator.Send(new GetUserNotificationByIdQuery()
            {
                UserId = UserId,
                UserNotificationId = UserNotificationId
            });
        }

        public async Task<UserNotificationFilterResult> GetByFilter(UserNotificationFilterParam param)
        {
            return await _mediator.Send(new GetUserNotificationByFilterQuery(param));
        }

        public async Task<UserNotificationFilterResultForAdmin> GetByFilterForAdmin(UserNotificationFilterParamForAdmin param)
        {
            return await _mediator.Send(new GetUserNotificationByFilterForAdminQuery(param));
        }

        public async Task<OperationResult> RemoveForUser(RemoveUserNotificationForUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> RemoveAllForUser(RemoveAllUserNotificationForUserCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> MarkAsRead(MarkUserNotificationAsReadCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<UserNotificationFilterResult> GetByFilterForLayout(UserNotificationFilterParam param)
        {
            return await _mediator.Send(new GetUserNotificationByFilterForLayoutQuery(param));
        }

        public async Task<Dictionary<string, string>> GetUserNamesForAdmin()
        {
            return await _mediator.Send(new GetUserNamesForAdminQuery());
        }
    }
}
