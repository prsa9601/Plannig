
using AngleSharp.Io;
using Common.Application;
using Common.Application.Validation;
using Domain.EventAgg.Repository;
using Domain.Notification;
using Domain.Notification.Repository;
using Domain.Notification.Service;
using FluentValidation;
using Hangfire;

namespace Application.Notification.Edit
{
    public class EditNotificationCommand : IBaseCommand
    {
        public long EventId { get; set; }
        public long NotificationId { get; set; }
        public bool IsSend { get; set; }
        public bool IsActive { get; set; }
        public bool IsSeen { get; set; }
        public int AllowedEmailCount { get; set; }
        public int AllowedSmsCount { get; set; }
        public DateTime EventStartTime { get; set; }
        public DateTime EventExpiredTime { get; set; }
        public DateTime SendTime { get; set; }
        public string ScheduleId { get; set; }

        public NotificationType NotificationType { get; set; }
        public ICollection<string> UserIds { get; set; }
    }
    internal class EditNotificationCommandHandler : IBaseCommandHandler<EditNotificationCommand>
    {
        private readonly INotificationRepository _repository;
        private readonly IEventRepository _eventRepository;
        private readonly INotificationService _service;

        public EditNotificationCommandHandler(INotificationRepository repository, IEventRepository eventRepository, INotificationService service)
        {
            _repository = repository;
            _eventRepository = eventRepository;
            _service = service;
        }

        public async Task<OperationResult> Handle(EditNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notification = await _repository.GetTracking(request.NotificationId);
                if (notification == null)
                    return OperationResult.NotFound();
                notification.Edit(request.EventId, request.IsSend, request.IsSeen,
                    request.AllowedEmailCount, request.AllowedSmsCount,
                    request.EventStartTime,
                    request.EventExpiredTime, request.SendTime,
                    request.NotificationType, request.UserIds, request.ScheduleId);

                var eventClass = await _eventRepository.GetTracking(request.EventId);

                if (eventClass == null)
                    return OperationResult.NotFound();

                else if (!eventClass.AccessNotification)
                    return OperationResult.
                        Error("شما به این ایونت دسترسی برای ارسال نوتیفیکیشن ندادید!");

                else if (notification.NotificationType != NotificationType.Email)
                    return OperationResult.
                        Error("شما به این ایونت دسترسی برای ارسال ایمیل ندادید!");

                await _repository.Save();

                if (notification.IsSend == false && notification.IsActive == true
                                                 && notification.NotificationType == NotificationType.Email &&
                                                 notification.AllowedEmailCount >= 0)
                {
                    //notification.SendEmailForEvent(request.UserNames.ToList()
                    //    , request.EventId
                    //    , request.EventStartTime, request.EventExpiredTime,
                    //    request.IsSend, request.AllowedEmailCount, request.IsActive
                    //    , eventClass.eventUser.Select(i => i.CreatorUserName).FirstOrDefault());
                    BackgroundJob.Delete(notification.ScheduleId);
                    BackgroundJob.Schedule(() => _service.SendEmailForEvent(request.UserIds.ToList()
                        , request.EventId
                        , request.EventStartTime,
                        request.SendTime,
                        eventClass.eventUser.Select(i => i.CreatorUserName).FirstOrDefault()), DateTime.Now.AddMinutes(1));

                    //jobId For Schedule
                   //BackgroundJob.ContinueJobWith<INotificationService>(
                   // jobId,
                   // () =>  _repository.Save(),
                   // JobContinuationOptions.OnAnyFinishedState);
                }

                return OperationResult.Success("نوتیفیکیشن با موفقیت ویرایش شد.");
            }
            catch (InvalidOperationException e)
            {
                return OperationResult.Error(e.Message);
            }
            catch (Exception e)
            { 
                return OperationResult.Error(e.Message);
                //throw new Exception(e.Message);
            }
          
        }
    }

    internal class EditNotificationCommandValidator : AbstractValidator<EditNotificationCommand>
    {
        public EditNotificationCommandValidator()
        {
            RuleFor(r => r.UserIds).NotEmpty().NotNull()
                .WithMessage(ValidationMessages.required("User"));

            RuleFor(b => b.AllowedEmailCount).NotNull()
                .WithMessage(ValidationMessages.minLength("تعداد ایمیل های مجاز", 0));

            RuleFor(r => r.AllowedEmailCount).NotNull()
                .WithMessage(ValidationMessages.minLength("تعداد پیامک های مجاز", 0));

        }
    }
}
