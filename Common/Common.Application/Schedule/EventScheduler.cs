using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;


namespace Common.Application.Schedule;

public class CalendarEvent
{
    public CalendarEvent(long id, string userEmail, DateTime eventTime,
        string eventDetails, string eventTitle, List<string>? usersEmail)
    {
        Id = id;
        UserEmail = userEmail;
        EventDetails = eventDetails;
        EventTime = eventTime;
        EventTitle = eventTitle;
        UsersEmail = usersEmail;
    }


    public long Id { get; set; } // 🔑 شناسه منحصربه‌فرد
    public string UserEmail { get; set; }
    public List<string>? UsersEmail { get; set; }
    public DateTime EventTime { get; set; }
    public string EventDetails { get; set; }
    public string EventTitle { get; set; }
}

public class EventScheduler : IDisposable
{
    private readonly object _lock = new object();
    private readonly SortedDictionary<DateTime, Queue<CalendarEvent>> _eventQueue =
        new SortedDictionary<DateTime, Queue<CalendarEvent>>();
    private readonly Timer _schedulerTimer;
    private bool _isDisposed;

    public EventScheduler()
    {
        _schedulerTimer = new Timer(CheckScheduledEvents, null, Timeout.Infinite, Timeout.Infinite);
    }

    public bool ScheduleEvent(DateTime eventTime, string userEmail, long id
        , string eventDetails, string eventTitle, List<string>? usersEmail)
    {

        var calendarEvent = new CalendarEvent(id, userEmail, eventTime, eventDetails, eventTitle, usersEmail);
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

                    if (!_eventQueue.TryGetValue(eventTime, out var events))
                    {
                        events = new Queue<CalendarEvent>();
                        _eventQueue.Add(eventTime, events);
                    }
                    events.Enqueue(calendarEvent);

                    // اگر این رویداد زودترین رویداد است، تایمر را آپدیت کن
                    if (eventTime < _eventQueue.Keys.Min() || _eventQueue.Count() == 1)
                    {
                        UpdateTimer(eventTime);
                        success = true;
                        return true;
                    }

                    success = true;
                    return true;
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
                return false;
            }

            return false;
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
                    Task.Run(() => SendEmail(evt.UserEmail, evt.EventDetails, evt.UsersEmail));
                }

                if (_eventQueue.Count > 0)
                {
                    UpdateTimer(_eventQueue.Keys.Min());
                }
            }
        }
    }

    private  async Task SendEmail(string email, string details, List<string>? usersEmail)
    {
        try
        {
            //var user = await _userManager.FindByIdAsync(id.ToString());
            //if (user == null)
            //    Console.WriteLine("کاربری یافت نشد.");

            if (string.IsNullOrWhiteSpace(email))
                Console.WriteLine("User email is not available");

            using var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(
                    "parsa9601m@gmail.com",
                    "leis nqek fthg fqbo"), // استفاده از پسورد مخصوص اپلیکیشن
                EnableSsl = true
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress("parsa9601m@gmail.com"),
                //To = {  "parsahavaset1@gmail.com","1234z1234@gmail.com"},
                To =
                {
                    "parsahavaset1@gmail.com",
                    "www.a1234z1234@gmail.com",
                    "parham1234122@gmail.com",
                    "parham09332294129@gmail.com"
                },
                Subject = "Reminded",
                Body = "<i>عاااااااااااااااااااااااا</i>",
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);
            if (usersEmail.Count() > 0)
            {
                foreach (var item in usersEmail)
                {
                    mailMessage.To.Add(item);
                }
            }
            

            await smtpClient.SendMailAsync(mailMessage).ConfigureAwait(false); // استفاده از نسخه Async
            
        }
        catch (Exception ex)
        {
            // برای دیباگ بهتر میتوانید خطا را لاگ کنید
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
        // شبیه‌سازی ارسال ایمیل (پیاده‌سازی واقعی نیاز به SMTP Client دارد)
        //Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Email to {email}: {details}");
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

    public bool UpdateEvent(long eventId, DateTime newTime, string userEmail,
        string eventTitle, string eventDetails, List<string>? usersEmail)
    {
        lock (_lock)
        {
            // 1. جستجوی رویداد در صف
            var oldEvent = FindEventById(eventId);
            if (oldEvent == null)
            {
                ScheduleEvent(newTime, userEmail
                    , eventId, eventDetails, eventTitle, usersEmail);
                return true;
            }

            else
            {
                RemoveEventFromQueue(oldEvent);

                // 3. ایجاد رویداد جدید با زمان به‌روزرسانی شده
                var updatedEvent = new CalendarEvent(oldEvent.Id, oldEvent.UserEmail,
                    oldEvent.EventTime, oldEvent.EventDetails, oldEvent.EventTitle, usersEmail);
                //{

                //    Id = oldEvent.Id,
                //    UserEmail = oldEvent.UserEmail,
                //    EventTime = newTime,
                //    EventDetails = oldEvent.EventDetails,
                //    EventTitle = oldEvent.EventTitle
                //};

                // 4. افزودن رویداد جدید به صف
                ScheduleEvent(newTime, updatedEvent.UserEmail
                    , updatedEvent.Id, updatedEvent.EventDetails, updatedEvent.EventTitle, usersEmail);

                return true;
            }
            // 2. حذف رویداد قدیمی از صف

        }
    }



    private CalendarEvent? FindEventById(long eventId)
    {
        foreach (var queue in _eventQueue.Values)
        {
            foreach (var evt in queue)
            {
                if (evt.Id == eventId) return evt;
            }
        }
        return null;
    }

    private void RemoveEventFromQueue(CalendarEvent oldEvent)
    {
        if (_eventQueue.TryGetValue(oldEvent.EventTime, out var queue))
        {
            var list = queue.ToList();
            list.RemoveAll(e => e.Id == oldEvent.Id);
            queue.Clear();
            foreach (var item in list) queue.Enqueue(item);

            // اگر صف خالی شد، کلید را حذف کن
            if (queue.Count == 0) _eventQueue.Remove(oldEvent.EventTime);
        }
    }
    public bool DeleteEvent(long eventId)
    {
        lock (_lock)
        {
            // پیدا کردن رویداد
            var targetEvent = FindEventById(eventId);
            if (targetEvent == null) return false;

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



            return true;
        }
    }

    //private CalendarEvent? FindEventById(long eventId)
    //{
    //    foreach (var queue in _eventQueue.Values)
    //    {
    //        foreach (var evt in queue)
    //        {
    //            if (evt.Id == eventId) return evt;
    //        }
    //    }
    //    return null;
    //}

    //private void RemoveEventFromQueue(CalendarEvent targetEvent)
    //{
    //    if (_eventQueue.TryGetValue(targetEvent.EventTime, out var queue))
    //    {
    //        var list = queue.ToList();
    //        list.RemoveAll(e => e.Id == targetEvent.Id);
    //        queue.Clear();
    //        foreach (var item in list) queue.Enqueue(item);

    //        // اگر صف خالی شد، کلید را حذف کن
    //        if (queue.Count == 0) _eventQueue.Remove(targetEvent.EventTime);
    //    }
    //}

}

// مثال استفاده
//class Program
//{
//    static async Task Main(string[] args)
//    {
//        using var scheduler = new EventScheduler();

//        // زمان‌های رویداد (تاریخ + ساعت)
//        var eventTimes = new[]
//        {
//            DateTime.Now.AddSeconds(10),
//            DateTime.Now.AddSeconds(20),
//            DateTime.Now.AddSeconds(30)
//        };

//        // افزودن 1000 رویداد تستی
//        for (int i = 0; i < 1000; i++)
//        {
//            scheduler.ScheduleEvent(
//                eventTimes[i % 3],
//                $"user{i}@example.com",
//                i += 6,
//                $"Event {i + 1}",
//                "uh"
//            );
//        }

//        Console.WriteLine("Scheduler is running...");
//        await Task.Delay(TimeSpan.FromMinutes(1));
//    }
//}
//[ApiController]
//[Route("api/events")]
//public class EventsController : ControllerBase
//{
//    private readonly EventSchedulerService _eventScheduler;

//    [HttpDelete("{eventId}")]
//    public IActionResult DeleteEvent(int eventId)
//    {
//        var result = _eventScheduler.DeleteEvent(eventId);
//        return result ? Ok() : NotFound();
//    }
//}
//public class EventSchedulerService
//{
//    private readonly SortedDictionary<DateTime, Queue<CalendarEvent>> _eventQueue = new();
//    private readonly object _lock = new();

//    public bool UpdateEvent(int eventId, DateTime newTime)
//    {
//        lock (_lock)
//        {
//            // 1. جستجوی رویداد در صف
//            var oldEvent = FindEventById(eventId);
//            if (oldEvent == null) return false;

//            // 2. حذف رویداد قدیمی از صف
//            RemoveEventFromQueue(oldEvent);

//            // 3. ایجاد رویداد جدید با زمان به‌روزرسانی شده
//            var updatedEvent = new CalendarEvent
//            {
//                Id = oldEvent.Id,
//                UserEmail = oldEvent.UserEmail,
//                EventTime = newTime,
//                EventDetails = oldEvent.EventDetails
//            };

//            // 4. افزودن رویداد جدید به صف
//            ScheduleEvent(updatedEvent);

//            return true;
//        }
//    }

//    private CalendarEvent? FindEventById(int eventId)
//    {
//        foreach (var queue in _eventQueue.Values)
//        {
//            foreach (var evt in queue)
//            {
//                if (evt.Id == eventId) return evt;
//            }
//        }
//        return null;
//    }

//    private void RemoveEventFromQueue(CalendarEvent oldEvent)
//    {
//        if (_eventQueue.TryGetValue(oldEvent.EventTime, out var queue))
//        {
//            var list = queue.ToList();
//            list.RemoveAll(e => e.Id == oldEvent.Id);
//            queue.Clear();
//            foreach (var item in list) queue.Enqueue(item);

//            // اگر صف خالی شد، کلید را حذف کن
//            if (queue.Count == 0) _eventQueue.Remove(oldEvent.EventTime);
//        }
//    }
//}