using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Notification.EmailSender;
using Application.Notification.SmsSender;
using Common.Application;
using MediatR;

namespace Presentation.Facade.Notification
{
    public interface INotificationFacade
    {
        Task<OperationResult> SendEmail(SendNotificationByEmail command);
        Task<OperationResult> SendSms(SendNotificationWithSms command);
    }
    public class NotificationFacade : INotificationFacade
    {
        private readonly IMediator _mediator;

        public NotificationFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> SendEmail(SendNotificationByEmail command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SendSms(SendNotificationWithSms command)
        {
            return await _mediator.Send(command);
        }
    }
}
