using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Notification.Add;
using Application.Notification.ChangeDate;
using Application.Notification.Edit;
using Application.Notification.EmailSender;
using Application.Notification.Remove;
using Application.Notification.SmsSender;
using Common.Application;
using MediatR;
using Query.Notification.DTOs;
using Query.Notification.GetById;
using Query.Notification.GetList;
using Query.User._Package.GetFilterByUserId;

namespace Presentation.Facade.Notification
{
    public interface INotificationFacade
    {
        Task<OperationResult> SendEmail(SendNotificationByEmailCommand command);
        Task<OperationResult> SendSms(SendNotificationWithSms command);
        Task<OperationResult> ChangeDate(ChangeDateNotificationCommand command);
        Task<OperationResult<long>> AddNotification(AddNotificationCommand command);
        Task<OperationResult> EditNotification(EditNotificationCommand command);
        Task<OperationResult> RemoveNotification(RemoveNotificationCommand command);

        Task<NotificationFilterResult?> GetFilterNotificationsCurrentUser(NotificationFilterParam param);
        Task<NotificationDto?> GetByIdNotificationsCurrentUser(string UserName, long NotificationId);

    }
    public class NotificationFacade : INotificationFacade
    {
        private readonly IMediator _mediator;

        public NotificationFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> SendEmail(SendNotificationByEmailCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult<long>> AddNotification(AddNotificationCommand command)
        {
            return await _mediator.Send(command);
        }
        public async Task<OperationResult> EditNotification(EditNotificationCommand command)
        {
            return await _mediator.Send(command);
        }
        public async Task<OperationResult> RemoveNotification(RemoveNotificationCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SendSms(SendNotificationWithSms command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> ChangeDate(ChangeDateNotificationCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<NotificationFilterResult?> GetFilterNotificationsCurrentUser(NotificationFilterParam param)
        {
            return await _mediator.Send(new GetListFilterNotificationByCurrentUserIdQuery(param));
        }

        public async Task<NotificationDto?> GetByIdNotificationsCurrentUser(string UserName, long NotificationId)
        {
            return await _mediator.Send(new GetNotificationByIdQuery
            {
                NotificationId = NotificationId,
                UserName = UserName
            });
        }
    }
}
