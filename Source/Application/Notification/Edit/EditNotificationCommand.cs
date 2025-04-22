using Common.Application;
using Domain.EventAgg.Repository;
using Domain.Notification;
using Domain.Notification.Repository;
using Domain.Notification.Service;
using Domain.UserAgg.Repository;
using FluentValidation;
using Hangfire;

namespace Application.Notification.Edit
{
    public class EditNotificationCommand : IBaseCommand
    {
        public long EventId { get; set; }
        public bool IsSend { get; set; }
        public bool IsActive { get; set; }
        public bool IsSeen { get; set; }
        //public int AllowedEmailCount { get; set; }
        //public int AllowedSmsCount { get; set; }
        public DateTime EventStartTime { get; set; }
        public  DateTime EventEndTime { get; set; }
        //public DateTime EventExpiredTime { get; set; }
        public DateTime SendTime { get; set; }
        public string creatorUserName { get; set; }
        //public string ScheduleId { get; set; }

        public NotificationType NotificationType { get; set; } 
        public ICollection<string> UserNames { get; set; }
    }
    internal class EditNotificationCommandHandler : IBaseCommandHandler<EditNotificationCommand>
    {
        private readonly INotificationRepository _repository;
        private readonly IUserRepository<Domain.UserAgg.User> _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly INotificationService _service;

        public EditNotificationCommandHandler(INotificationRepository repository, IEventRepository eventRepository, INotificationService service, IUserRepository<Domain.UserAgg.User> userRepository)
        {
            _repository = repository;
            _eventRepository = eventRepository;
            _service = service;
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Handle(EditNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.UserNames.Add(request.creatorUserName);

                var notification = await _repository.GetByFilterAsync(i => i.EventId.Equals(request.EventId));
                if (notification == null)
                    return OperationResult.NotFound();
                notification.Edit(request.EventId,
                       request.IsSend, request.IsSeen, request.EventStartTime,
                       request.SendTime, NotificationType.Email, request.UserNames, 
                       request.IsActive, request.EventEndTime);

                var eventClass = await _eventRepository.GetTracking(request.EventId);
                if (eventClass == null)
                {
                    return OperationResult.NotFound();
                }
                //else if (!eventClass.AccessNotification)
                //{
                //    BackgroundJob.Delete(notification.ScheduleId);
                //    return OperationResult.
                //        Error("شما به این ایونت دسترسی برای ارسال نوتیفیکیشن ندادید!");
                //}
                //else if (notification.NotificationType != NotificationType.Email)
                //{
                //    BackgroundJob.Delete(notification.ScheduleId);
                //    return OperationResult.
                //        Error("شما به این ایونت دسترسی برای ارسال ایمیل ندادید!");
                //}

                bool DeleteScheduleResult = BackgroundJob.Delete(notification.ScheduleId);
                bool DeleteNotificationScheduleResult = BackgroundJob.
                    Delete(notification.NotificationScheduleId);
                await _repository.Save();

                if (notification.IsSend == false && notification.IsActive == true
                                                 && notification.NotificationType == NotificationType.Email &&
                                                 notification.AllowedEmailCount >= 0)
                {
                    var creator = await _userRepository.GetByFilterAsync(i=>i.UserName.Equals(eventClass.EventUser.Select(i => i.CreatorUserId).FirstOrDefault()));
                    int SendEmailCount = 0;
                    foreach (var item in request.UserNames)
                    {
                        //if (item != null && item.Email != null)
                        //{
                        //    mailMessage.To.Add(item.Email);
                        SendEmailCount++;
                        //}
                    }
                    var NotificationScheduleId = BackgroundJob.Schedule(() => _service.SendNotification(notification.Id), request.SendTime);

                    notification.NotificationScheduleId = NotificationScheduleId;
                    var setChangeResult = await SetChange(SendEmailCount, creator!);
                    if (setChangeResult != OperationResult.Success())
                    {
                        notification.DisabledActive();
                        eventClass.DisableAccessNotification();
                        await _repository.Save();
                        return OperationResult.Error(setChangeResult.Message);
                    }

                    string scheduleId = BackgroundJob.Schedule(() => _service.SendEmailForEvent(request.UserNames.ToList()
                                      , request.EventId
                                      , request.EventStartTime,
                                      request.SendTime,
                                      eventClass.EventUser.Select(i => i.CreatorUserId).FirstOrDefault()), request.SendTime);

                    notification.ScheduleId = scheduleId;
                    //var scheduleid =
                        BackgroundJob.Schedule(() => _service.SendNotification(notification.Id), request.SendTime);

                    await _repository.Save();
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

    
       internal async Task<OperationResult> SetChange(int SendEmailCount,
      Domain.UserAgg.User creator)
        {

            var creatorPackages = creator.UserPackages.Where(i =>
               i.ExpiryDate > DateTime.Now).OrderBy(i => i.CreationDate)
                .FirstOrDefault();

            if (creatorPackages == null)
                return OperationResult.Error("کاربر عزیز شما پکیج فعالی ندارید!");
            for (int i = 1; creatorPackages.AllowedEmailCount - SendEmailCount <= -10; i++)
            {
                SendEmailCount -= creatorPackages.AllowedEmailCount;
                creatorPackages.AllowedEmailCount = 0;
                creatorPackages = creator.UserPackages.Where(i =>
                          i.ExpiryDate > DateTime.Now).OrderBy(i => i.CreationDate)
                    .Skip(i).Take(1)
                    .FirstOrDefault();
                if (creatorPackages == null && SendEmailCount >= 10)
                {
                    int CountResult = await DeActiveLatestEventEmail(creator, SendEmailCount);
                    if (CountResult <= 0)
                    {
                        return OperationResult.Error(
                        "تعداد درخواست ها برای ارسال ایمیل بیش تر از حد مجاز مصرفی شما است " +
                        "، ما نوتیفیکیشن آخرین ایونت شما از نظر تایمی رو غیرفعال کردیم بعد" +
                        " از شارژ حساب خود می توانید به صورت دستی نوتیفیکیشن ایونت خود را فعال کنید !");
                    }
                    else
                        return OperationResult.Error(
                            "تعداد درخواست ها برای ارسال ایمیل بیش تر از حد مجاز مصرفی شما است");

                }

                else if (SendEmailCount <= 10 && SendEmailCount > 0)
                {
                    creatorPackages.AllowedEmailCount = -SendEmailCount;
                }
            }


            creatorPackages.AllowedEmailCount -= SendEmailCount;
            await _userRepository.Save();
            return OperationResult.Success();
        }

        private async Task<int> DeActiveLatestEventEmail(Domain.UserAgg.User userCreator,
            int sendEmailCount)
        {
            try
            {
                var eventsId = userCreator.userEvents.Select(i => i);
                var events = await _eventRepository.
                    GetListByFilterAsync(i => i.Id.Equals(eventsId));
                //event
                var e = events!.OrderByDescending(i => i.CreationDate);
                if (sendEmailCount >= 0)
                {
                    var eventsIds = new List<long>();
                    foreach (var item in e)
                    {
                        if (item.AccessNotification == true)
                        {
                            var count = item.EventUser.Count();
                            item.AccessNotification = false;
                            sendEmailCount -= count;
                            eventsIds.Add(item.Id);
                            if (sendEmailCount <= 0)
                            {
                                Domain.EventAgg.Event? myevent = events!.FirstOrDefault(i => i.Id.Equals(eventsIds));
                                myevent!
                                    .DisableAccessNotification();
                                await _eventRepository.Save();
                                return sendEmailCount;
                                //break;
                            }
                            //else
                            //{
                            //    events = e.ToList();
                            //    await _eventRepository.Save();
                            //    return sendEmailCount;
                            //}
                        }

                    }


                }
                return sendEmailCount;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }
    }
}
//notification.SendEmailForEvent(request.UserNames.ToList()
                    //    , request.EventId
                    //    , request.EventStartTime, request.EventExpiredTime,
                    //    request.IsSend, request.AllowedEmailCount, request.IsActive
                    //    , eventClass.eventUser.Select(i => i.CreatorUserName).FirstOrDefault());
                    //if (DeleteScheduleResult)
                    //{
                    //    string scheduleId = BackgroundJob.Schedule(() => _service.SendEmailForEvent(request.UserNames.ToList()
                    //        , request.EventId
                    //        , request.EventStartTime,
                    //        request.SendTime,
                    //        eventClass.EventUser.Select(i => i.CreatorUserName).FirstOrDefault()), DateTime.Now.AddMinutes(1));

                    //    notification.ScheduleId = scheduleId;
                    //    await _repository.Save();
                    //}
                    //else
                    //{
                    //    for (int i = 0; i < 5; i++)
                    //    {
                    //        DeleteScheduleResult = BackgroundJob.Delete(notification.ScheduleId);
                    //        if (DeleteScheduleResult) 
                    //        {
                    //            string scheduleId = BackgroundJob.Schedule(() => _service.SendEmailForEvent(request.UserNames.ToList()
                    //                 , request.EventId
                    //                 , request.EventStartTime,
                    //                 request.SendTime,
                    //                 eventClass.EventUser.Select(i => i.CreatorUserName).FirstOrDefault()), DateTime.Now.AddMinutes(1));

                    //            notification.ScheduleId = scheduleId;
                    //            await _repository.Save();
                    //            break;
                    //        }
                    //    }
                    //    return OperationResult.NotFound("ScheduleId یافت نشد!");  
                    //}