using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Notification.Add;
using Application.Notification.Edit;
using Application.Notification.EmailSender;
using Application.Notification.Remove;
using Application.Notification.SmsSender;
using Common.Application;
using MediatR;

namespace Presentation.Facade.Notification
{
    public interface INotificationFacade
    {
        Task<OperationResult> SendEmail(SendNotificationByEmailCommand command);
        Task<OperationResult> SendSms(SendNotificationWithSms command);
        Task<OperationResult<long>> AddNotification(AddNotificationCommand command);
        Task<OperationResult> EditNotification(EditNotificationCommand command);
        Task<OperationResult> RemoveNotification(RemoveNotificationCommand command);

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
    }
}
