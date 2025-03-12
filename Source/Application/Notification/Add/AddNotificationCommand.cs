using Common.Application;
using Common.Application.Validation;
using Domain.EventAgg.Repository;
using Domain.Notification;
using Domain.Notification.Repository;
using Domain.Notification.Service;
using FluentValidation;
using FluentValidation.Validators;
using Hangfire;
using System.Net.WebSockets;

namespace Application.Notification.Add
{
    public class AddNotificationCommand : IBaseCommand<long>
    {
        public long EventId { get; set; }
        public bool IsSend { get; set; }
        public bool IsActive { get; set; }
        public bool IsSeen { get; set; }
        //public int AllowedEmailCount { get; set; }
        //public int AllowedSmsCount { get; set; }
        public DateTime EventStartTime { get; set; }
        //public DateTime EventExpiredTime { get; set; }
        public DateTime SendTime { get; set; }
        public string creatorUserName { get; set; }
        //public string ScheduleId { get; set; }

        public NotificationType NotificationType { get; set; } = NotificationType.Email;
        public ICollection<string> UserNames { get; set; }
    }
    internal class AddNotificationCommandHandler : IBaseCommandHandler<AddNotificationCommand, long>
    {
        private readonly INotificationRepository _repository;
        private readonly IEventRepository _eventRepository;
        private readonly INotificationService _service;


        public AddNotificationCommandHandler(INotificationRepository repository, IEventRepository eventRepository, INotificationService service)
        {
            _repository = repository;
            _eventRepository = eventRepository;
            _service = service;
        }

        public async Task<OperationResult<long>> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notification = new Domain.Notification.Notification(request.EventId,
                       request.IsSend, request.IsSeen, request.EventStartTime,
                       request.SendTime, NotificationType.Email, request.UserNames, request.IsActive
                       );

                await _repository.AddAsync(notification);
                var eventClass = await _eventRepository.GetTracking(request.EventId);

                if (eventClass == null)
                    return OperationResult<long>.NotFound();

                else if (!eventClass.AccessNotification)
                    return OperationResult<long>.
                        Error("شما به این ایونت دسترسی برای ارسال نوتیفیکیشن ندادید!");

                else if (notification.NotificationType != NotificationType.Email)
                    return OperationResult<long>.
                        Error("شما به این ایونت دسترسی برای ارسال ایمیل ندادید!");

                await _repository.Save();
                if (notification.IsSend == false && notification.IsActive == true
                    && notification.NotificationType == NotificationType.Email &&
                    notification.AllowedEmailCount >= 0)
                {
                    //notification.SendEmailForEvent(request.UserNames.ToList()
                    //    , request.EventId
                    //, notification.EventStartTime, notification.EventExpiredTime,
                    //notification.IsSend, notification.AllowedEmailCount, notification.IsActive
                    //, eventClass.eventUser.Select(i => i.CreatorUserName).FirstOrDefault());

                    BackgroundJob.Schedule(() => _service.SendEmailForEvent(request.UserNames.ToList()
                        , request.EventId
                    , notification.EventStartTime,
                    notification.SendTime,
                    eventClass.eventUser.Select(i => i.CreatorUserName).FirstOrDefault()), request.EventStartTime-request.SendTime);
                
                    
                }

                return OperationResult<long>.Success(notification.Id);
            }
            catch (InvalidOperationException e)
            {
                return OperationResult<long>.Error(e.Message);
            }
            catch (Exception e)
            {
                return OperationResult<long>.Error(e.Message);
            }
        }
    }

    internal class AddNotificationCommandValidator : AbstractValidator<AddNotificationCommand>
    {
        public AddNotificationCommandValidator()
        {
            RuleFor(r => r.UserNames).NotEmpty().NotNull()
                .WithMessage(ValidationMessages.required("User"));

            //RuleFor(b => b.AllowedEmailCount).NotNull()
            //    .WithMessage(ValidationMessages.minLength("تعداد ایمیل های مجاز", 0));

            //RuleFor(r => r.AllowedEmailCount).NotNull()
            //    .WithMessage(ValidationMessages.minLength("تعداد پیامک های مجاز", 0));
            //RuleFor(r => r.Text)
            //    .NotNull()
            //    .MinimumLength(5).WithMessage(ValidationMessages.minLength("متن نظر", 5));
        }
    }
}
