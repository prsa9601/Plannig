using Application.User._Notification.Services;
using Common.Application;
using Domain.NotificationAgg;
using Domain.NotificationAgg.Repository;
using Domain.UserAgg;
using Domain.UserAgg.Repository;
using Domain.UserAgg.Service;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Application.User._Notification.Admin.Add
{
    public class AddUserNotificationCommand : IBaseCommand
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; } //ارسال بشه یانه
        public DateTime SendTime { get; set; }
        public required string UserId { get; set; }
        public bool SendToAllUser { get; set; }
        public required UserNotificationType NotificationType { get; set; }

        public List<string>? UserIds { get; set; }
    }
    internal class AddAllNotificationCommandHandler : IBaseCommandHandler<AddUserNotificationCommand>
    {
        private readonly INotificationRepository _repository;
        private readonly IUserRepository<Domain.UserAgg.User> _userRepository;
        private readonly IUserNotificationDomainService _service;
        private readonly ILogger _logger;
        public AddAllNotificationCommandHandler(INotificationRepository repository,
            ILogger logger, IUserRepository<Domain.UserAgg.User> userRepository, IUserNotificationDomainService service)
        {
            _repository = repository;
            _logger = logger;
            _userRepository = userRepository;
            _service = service;
        }

        public async Task<OperationResult> Handle(AddUserNotificationCommand request, CancellationToken cancellationToken)
        {

            var creator = await _userRepository.GetTrackingWithString(request.UserId);
            var users = await _userRepository.GetAllUser();

            var User = request.SendToAllUser ? users.Select(i => i.Id)
                : request.UserIds;
            if (User == null)
                return OperationResult.NotFound();
            if (creator == null)
                return OperationResult.NotFound();

            var notification = new UserNotification(request.Title,
                request.Description, request.IsActive, request.SendTime, User.ToList(), request.NotificationType);


            creator.AddNotification(notification);
            //await _repository.Save();

            //if (request.SendToAllUser)
            //{
            //    foreach (var item in users)
            //    {
            //        item.AddNotification(notification);
            //    }
            //}
            //else
            //{
            //    foreach (var item in User)
            //    {
            //        users.FirstOrDefault(i => i.Id.Equals(item)).AddNotification(notification);
            //    }
            //}

            await _repository.Save();

            if (request.NotificationType == UserNotificationType.Sms)
            {
                BackgroundJob.Schedule(() => _service.SendSms(notification.Id), request.SendTime);
            }
            if (request.NotificationType == UserNotificationType.Email)
            {
                BackgroundJob.Schedule(() => _service.SendEmail(notification.Id), request.SendTime);
            }
            if (request.NotificationType == UserNotificationType.Website)
            {
                BackgroundJob.Schedule(() => _service.SendNotification(notification.Id), request.SendTime);
            }
            //if (request.SendTime <= DateTime.Now)
            //{
            //    BackgroundJob.Schedule(() => _service.SendNotification(notification.Id), DateTime.Now);
            //}

            //BackgroundJob.Schedule(() => _service.SendNotification(notification.Id), request.SendTime);

            return OperationResult.Success();

        }
    }
}
