using Common.Application;
using Domain.EventAgg.Repository;
using Domain.Notification;
using Domain.Notification.Repository;
using Domain.Notification.Service;
using Domain.UserAgg.Repository;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notification.ChangeDate
{
    public class ChangeDateNotificationCommand : IBaseCommand
    {
        public long EventId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime SendTime { get; set; }
        public DateTime EndTime { get; set; }
    }
    internal class ChangeDateNotificationCommandHandler : IBaseCommandHandler<ChangeDateNotificationCommand>
    {
        private readonly INotificationRepository _repository;
        private readonly IUserRepository<Domain.UserAgg.User> _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly INotificationService _service;

        public ChangeDateNotificationCommandHandler(INotificationService service, IEventRepository eventRepository, IUserRepository<Domain.UserAgg.User> userRepository, INotificationRepository repository)
        {
            _service = service;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ChangeDateNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                 var notification = await _repository.GetByFilterAsync(i => i.EventId.Equals(request.EventId));
                if (notification == null)
                    return OperationResult.NotFound();
                notification.ChangeDate(request.StartTime, request.SendTime, request.EndTime);

                var eventClass = await _eventRepository.GetTracking(request.EventId);
                if (eventClass == null)
                {
                    return OperationResult.NotFound();
                }
                //else if (!eventClass.AccessNotification)
                //{
                //    BackgroundJob.Delete(notification.ScheduleId);
                //    BackgroundJob.Delete(notification.NotificationScheduleId);
                //    return OperationResult.
                //        Error("شما به این ایونت دسترسی برای ارسال نوتیفیکیشن ندادید!");
                //}
                //else if (notification.NotificationType != NotificationType.Email)
                //{
                //    BackgroundJob.Delete(notification.ScheduleId);
                //    BackgroundJob.Delete(notification.NotificationScheduleId);
                //    return OperationResult.
                //        Error("شما به این ایونت دسترسی برای ارسال ایمیل ندادید!");
                //}

                bool DeleteScheduleResult = BackgroundJob.Delete(notification.ScheduleId);
                BackgroundJob.Delete(notification.NotificationScheduleId);

                    var NotificationScheduleId =  BackgroundJob.Schedule(() => _service.SendNotification(notification.Id), request.SendTime);
                    notification.NotificationScheduleId = NotificationScheduleId;
                await _repository.Save();

                if (notification.IsSend == false && notification.IsActive == true
                                                 && notification.NotificationType == NotificationType.Email &&
                                                 notification.AllowedEmailCount >= 0)
                {
            
                    notification.NotificationScheduleId = NotificationScheduleId;
                    string scheduleId = BackgroundJob.Schedule(() => _service.SendEmailForEvent(notification.UserNames.ToList()
                                      , request.EventId
                                      , request.StartTime,
                                      notification.SendTime,
                                      eventClass.EventUser.Select(i => i.CreatorUserName).FirstOrDefault()), request.SendTime);

                    notification.ScheduleId = scheduleId;
                    //var scheduleid =
                        //BackgroundJob.Schedule(() => notification.EnabledActiveAsync(), request.SendTime);
                  

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