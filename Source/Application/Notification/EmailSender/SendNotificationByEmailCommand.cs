using Common.Application;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using Domain.EventAgg.Repository;
using Domain.EventAgg.Service;
using Domain.Notification;
using Domain.Notification.Repository;
using Domain.Notification.Service;
using Hangfire;

namespace Application.Notification.EmailSender
{
    public class SendNotificationByEmailCommand : IBaseCommand
    {
        public long notificationId { get; set; }
        public long EventId { get; set; }
        public List<string>? userNames { get; set; }
        public DateTime startTime { get; set; }
    }
    public class SendNotificationByEmailHandler : IBaseCommandHandler<SendNotificationByEmailCommand>
    {
        private readonly UserManager<Domain.UserAgg.User> _userManager;
        //private readonly IEventService _service;
        private readonly INotificationService _service;
        private readonly INotificationRepository _repository;
        private readonly IEventRepository _eventRepository;

        public SendNotificationByEmailHandler(UserManager<Domain.UserAgg.User> userManager,
            INotificationService service, INotificationRepository repository,
            IEventRepository eventRepository)
        {
            _userManager = userManager;
            _service = service;
            _repository = repository;
            _eventRepository = eventRepository;
        }

        public async Task<OperationResult> Handle(SendNotificationByEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notification = await _repository.GetTracking(request.notificationId);
                var eventClass = await _eventRepository.GetTracking(request.EventId);

                if (eventClass == null)
                    return OperationResult.NotFound();

                else if (!eventClass.AccessNotification)
                    return OperationResult.
                        Error("شما به این ایونت دسترسی برای ارسال نوتیفیکیشن ندادید!");

                else if (notification.NotificationType != NotificationType.Email)
                    return OperationResult.
                        Error("شما به این ایونت دسترسی برای ارسال ایمیل ندادید!");

                BackgroundJob.Schedule(() => notification.SendEmail(request.userNames, request.EventId
                , notification.EventStartTime, notification.EventExpiredTime,
                notification.IsSend, notification.AllowedEmailCount, notification.IsActive,
                _service), notification.EventStartTime);


                return OperationResult.Success();
            }
            catch (Exception e)
            {
                OperationResult.Error(e.Message);
                throw;
            }
        }

        //public async Task<OperationResult> Handle(SendNotificationByEmail request, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //       //var q =   await _service.Schedule("411f8274-5ee7-4bcc-8d43-e5214aa79aa7","aaaaaaaaaaa",DateTime.Now.AddSeconds(5), cancellationToken);

        //            var user = await _userManager.FindByIdAsync(request.Id.ToString());
        //        if (user == null)
        //            return OperationResult.NotFound();

        //        if (string.IsNullOrWhiteSpace(user.Email))
        //            return OperationResult.Error("User email is not available");

        //        using var smtpClient = new SmtpClient("smtp.gmail.com")
        //        {
        //            Port = 587,
        //            Credentials = new NetworkCredential(
        //                "parsa9601m@gmail.com",
        //                "leis nqek fthg fqbo"), // استفاده از پسورد مخصوص اپلیکیشن
        //            EnableSsl = true
        //        };

        //        using var mailMessage = new MailMessage
        //        {
        //            From = new MailAddress("parsa9601m@gmail.com"),
        //            //To = {  "parsahavaset1@gmail.com","1234z1234@gmail.com"},
        //            To =
        //            {
        //                "parsahavaset1@gmail.com",
        //                "www.a1234z1234@gmail.com",
        //                "parham1234122@gmail.com",
        //                "parham09332294129@gmail.com"
        //            },
        //            Subject = "Reminded",
        //            Body = "<i>عاااااااااااااااااااااااا</i>",
        //            IsBodyHtml = true
        //        };
        //        mailMessage.To.Add(user.Email);

        //        await smtpClient.SendMailAsync(mailMessage); // استفاده از نسخه Async
        //        return OperationResult.Success();
        //    }
        //    catch (Exception ex)
        //    {
        //        // برای دیباگ بهتر میتوانید خطا را لاگ کنید
        //        return OperationResult.Error($"Error sending email: {ex.Message}");
        //    }
        //}

    }
}
