using System.Net.Mail;
using System.Net;
using Domain.EventAgg.Repository;
using Domain.UserAgg;
using Domain.UserAgg.Repository;

namespace Domain.NotificationAgg.NotificationSchedule
{
    public class EventNotificationSchedule
    {
        public long NotificationId { get; private set; }
        public DateTime EventStartTime { get; private set; }
        public DateTime EventEndTime { get; private set; }
        public DateTime SendTime { get; private set; }
        public string CreatorUserName { get; private set; }
        public string CreatorUserEmail { get; private set; }
        public Dictionary<string, string> UsersDetails { get; private set; }
        public string EventTitle { get; private set; }
        public string EventDescription { get; private set; }

        public EventNotificationSchedule(DateTime eventStartTime,
            DateTime eventEndTime, DateTime sendTime, long notificationId,
            string creatorUserName, Dictionary<string, string> usersDetails,
            string eventTitle, string eventDescription, string creatorUserEmail)
        {
            EventStartTime = eventStartTime;
            EventEndTime = eventEndTime;
            SendTime = sendTime;
            NotificationId = notificationId;
            CreatorUserName = creatorUserName;
            UsersDetails = usersDetails;
            EventTitle = eventTitle;
            EventDescription = eventDescription;
            CreatorUserEmail = creatorUserEmail;
        }
    }

    public class EventNotificationScheduler : IDisposable
    {
        private readonly object _lock = new object();

        private readonly SortedDictionary<DateTime, Queue<EventNotificationSchedule>> _eventQueue =
            new SortedDictionary<DateTime, Queue<EventNotificationSchedule>>();

        private readonly Timer _schedulerTimer;
        private bool _isDisposed;

        public EventNotificationScheduler()
        {
            _schedulerTimer = new Timer(CheckScheduledEvents, null, Timeout.Infinite, Timeout.Infinite);
        }

        public Task<bool> AddScheduleEvent(DateTime eventStartTime
            , DateTime eventEndTime, DateTime sendTime, long notificationId,
            Dictionary<string, string> usersDetails, string eventDescription,
            string eventTitle, string creatorUserName, string creatorUserEmail)
        {

            var calendarEvent = new EventNotificationSchedule(eventStartTime
                , eventEndTime, sendTime, notificationId, creatorUserName,
                usersDetails, eventTitle, eventDescription, creatorUserEmail);
            //{
            //    Id = id,
            //    EventDetails = eventDetails,
            //    EventTime = eventTime,
            //    UserEmail = userEmail,
            //    EventTitle = eventTitle
            //});
            int retryCount = 0;
            bool success = false;

            lock (_lock)
            {
                while (retryCount < 3 && !success)
                {
                    try
                    {

                        if (!_eventQueue.TryGetValue(sendTime, out var events))
                        {
                            events = new Queue<EventNotificationSchedule>();
                            _eventQueue.Add(sendTime, events);
                        }

                        events.Enqueue(calendarEvent);

                        // اگر این رویداد زودترین رویداد است، تایمر را آپدیت کن
                        if (sendTime < _eventQueue.Keys.Min() || _eventQueue.Count() == 1)
                        {
                            UpdateTimer(sendTime);
                            success = true;
                            return Task.FromResult(true);
                        }

                        success = true;
                        return Task.FromResult(true);
                    }
                    catch (Exception e)
                    {
                        retryCount++;
                        Console.WriteLine(e);
                        throw;
                    }
                }

                if (!success && retryCount >= 3)
                {
                    return Task.FromResult(false);
                }

                return Task.FromResult(false);
            }
        }

        private void UpdateTimer(DateTime nextEventTime)
        {

            var delay = nextEventTime - DateTime.Now;
            _schedulerTimer.Change(delay > TimeSpan.Zero ? delay : TimeSpan.Zero, Timeout.InfiniteTimeSpan);
        }

        private void CheckScheduledEvents(object? state)
        {
            lock (_lock)
            {
                while (_eventQueue.Count > 0 && _eventQueue.Keys.Min() <= DateTime.Now)
                {
                    var nextEventTime = _eventQueue.Keys.Min();
                    var events = _eventQueue[nextEventTime];
                    _eventQueue.Remove(nextEventTime);

                    foreach (var evt in events)
                    {
                        Task.Run(() => SendEmail(evt.CreatorUserName, evt.CreatorUserEmail
                          , evt.UsersDetails, evt.EventStartTime,
                          evt.EventEndTime, evt.SendTime, evt.EventTitle, evt.EventDescription,
                          evt.NotificationId));
                    }

                    if (_eventQueue.Count > 0)
                    {
                        UpdateTimer(_eventQueue.Keys.Min());
                    }
                }
            }
        }

        private async Task SendEmail(string creatorUserName, string creatorUserEmail,
            Dictionary<string, string> userDetails, DateTime eventStartTime,
            DateTime eventEndTime, DateTime sendTime, string eventTitle, string eventDescription,
            long notificationId)
        {
            try
            {
                //var q =   await _service.Schedule("411f8274-5ee7-4bcc-8d43-e5214aa79aa7","aaaaaaaaaaa",DateTime.Now.AddSeconds(5), cancellationToken);

                //var eventClass = await _repository.GetTracking(eventId);


                //if (eventClass == null)
                //    throw new Exception("ایونت یافت نشد!");

                //List<Domain.UserAgg.User?> users = new List<Domain.UserAgg.User?>();
                //foreach (var item in userIds)
                //{
                //    users.Add((await _userRepository.GetTrackingWithString(item)));
                //    //users.Add((await _userRepository.GetTrackingWithString(item))!);
                //}
                //if (string.IsNullOrWhiteSpace(eventClass))
                //    throw new Exception("User email is not available");

                using var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(
                        "parsa9601m@gmail.com",
                        "leis nqek fthg fqbo"), // استفاده از پسورد مخصوص اپلیکیشن
                    EnableSsl = true
                };


                //using var mailMessage = new MailMessage
                //{
                //    From = new MailAddress("parsa9601m@gmail.com"),
                //    //To = {  "parsahavaset1@gmail.com","1234z1234@gmail.com"},
                //    To =
                //    {
                //        "parsahavaset1@gmail.com",
                //        "www.a1234z1234@gmail.com",
                //        "parham1234122@gmail.com",
                //        "parham09332294129@gmail.com"
                //    },
                //    Subject = "Reminded",
                //    Body = "<i>عاااااااااااااااااااااااا</i>",
                //    IsBodyHtml = true
                //};


                foreach (var item in userDetails)
                {

                    if (item.Value != null && item.Key != null)
                    {//value = Email; key = UserName
                        using var mailMessage = new MailMessage
                        {
                            From = new MailAddress("parsa9601m@gmail.com"),
                            //To = {  "parsahavaset1@gmail.com","1234z1234@gmail.com"},
                            To = { item.Value },
                            Subject = "Reminded",
                            Body = $"<i>عاااااااااااااااااااااااا</i>{item.Key}",
                            IsBodyHtml = true
                        };
                        await smtpClient.SendMailAsync(mailMessage); // استفاده از نسخه Async

                        //mailMessage.To.Add(item.Email);
                    }
                }
                //mailMessage.To.Add(user.Email);

                //await smtpClient.SendMailAsync(mailMessage); // استفاده از نسخه Async
                //await _schedule.ScheduleEvent((startTime-sendTime),"",)

            }
            catch (Exception ex)
            {
                // برای دیباگ بهتر میتوانید خطا را لاگ کنید
                throw new Exception($"Error sending email: {ex.Message}");
            }
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _schedulerTimer.Dispose();
                _isDisposed = true;
            }
            //var oldScheduler = new EventScheduler();
            //oldScheduler.Dispose(); // 🔥 شیء نابود شد

            //// ایجاد نمونه جدید
            //var newScheduler = new EventScheduler();
            //newScheduler.ScheduleEvent(...); // ✅ کار می‌کند
        }

        public Task<bool> UpdateEvent(long notificationId,
            DateTime eventStartTime, DateTime eventEndTime, DateTime newSendTime,
            Dictionary<string, string> usersDetails, string eventDescription,
            string eventTitle, string creatorUserName, string creatorUserEmail)
        {
            lock (_lock)
            {
                // 1. جستجوی رویداد در صف
                var oldEvent = FindEventById(notificationId);
                if (oldEvent == null)
                {
                    AddScheduleEvent(eventStartTime, eventEndTime,
                        newSendTime, notificationId,
                        usersDetails, eventDescription
                        , eventTitle, creatorUserName, creatorUserEmail);
                    return Task.FromResult(true);
                }

                else
                {
                    RemoveEventFromQueue(oldEvent);

                    // 3. ایجاد رویداد جدید با زمان به‌روزرسانی شده
                    var updatedEvent = new EventNotificationSchedule(eventStartTime, eventEndTime,
                        newSendTime, oldEvent.NotificationId, creatorUserName,
                        usersDetails, eventTitle, eventDescription, creatorUserEmail);
                    //{

                    //    Id = oldEvent.Id,
                    //    UserEmail = oldEvent.UserEmail,
                    //    EventTime = newTime,
                    //    EventDetails = oldEvent.EventDetails,
                    //    EventTitle = oldEvent.EventTitle
                    //};

                    // 4. افزودن رویداد جدید به صف
                    AddScheduleEvent(eventStartTime, updatedEvent.EventEndTime,
                        updatedEvent.SendTime, notificationId,
                        usersDetails, eventDescription, eventTitle, creatorUserName,
                        creatorUserEmail);

                    return Task.FromResult(true);
                }
                // 2. حذف رویداد قدیمی از صف

            }
        }



        private EventNotificationSchedule? FindEventById(long notificationId)
        {
            foreach (var queue in _eventQueue.Values)
            {
                foreach (var evt in queue)
                {
                    if (evt.NotificationId == notificationId) return evt;
                }
            }

            return null;
        }

        private void RemoveEventFromQueue(EventNotificationSchedule oldEvent)
        {
            if (_eventQueue.TryGetValue(oldEvent.SendTime, out var queue))
            {
                var list = queue.ToList();
                list.RemoveAll(e => e.NotificationId
                                    == oldEvent.NotificationId);
                queue.Clear();
                foreach (var item in list) queue.Enqueue(item);

                // اگر صف خالی شد، کلید را حذف کن
                if (queue.Count == 0) _eventQueue.Remove(oldEvent.SendTime);
            }
        }

        public Task<bool> DeleteEvent(long notificationId)
        {
            lock (_lock)
            {
                // پیدا کردن رویداد
                var targetEvent = FindEventById(notificationId);
                if (targetEvent == null) return Task.FromResult(false);

                // حذف رویداد از صف
                RemoveEventFromQueue(targetEvent);
                if (_eventQueue.Count() == 0)
                {

                    _schedulerTimer.Change(Timeout.Infinite, Timeout.Infinite);
                }
                else
                {
                    UpdateTimer(_eventQueue.Keys.Min());
                }



                return Task.FromResult(true);
            }
        }
    }
}
