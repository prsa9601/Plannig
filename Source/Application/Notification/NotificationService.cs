using System.Net;
using System.Net.Mail;
using Common.Application.Schedule;
using Domain.EventAgg.Repository;
using Domain.Notification.Repository;
using Domain.Notification.Service;
using Domain.UserAgg;
using Domain.UserAgg.Repository;
using Microsoft.AspNet.Identity;
using Telegram.Bot.Types;

namespace Application.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;
        private readonly IEventRepository _eventRepository;

        private readonly EventScheduler _schedule;

        private readonly IUserRepository<Domain.UserAgg.User> _userRepository;
        //private readonly object _lock = new object();

        //private readonly SortedDictionary<DateTime, Queue<CalendarEvent>> _eventQueue =
        //    new SortedDictionary<DateTime, Queue<CalendarEvent>>();

        //private readonly Timer _schedulerTimer;
        //private bool _isDisposed;



        public NotificationService(IUserRepository<Domain.UserAgg.User> userRepository, INotificationRepository repository,
            EventScheduler schedule, IEventRepository eventRepository)
        {
            //_schedulerTimer = new Timer(CheckScheduledEvents, null, Timeout.Infinite, Timeout.Infinite);

            _repository = repository;
            _schedule = schedule;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
        }

        public async Task SendEmail(List<string> userIds, long eventId, DateTime startTime, DateTime sendTime)
        {
            try
            {
                //var q =   await _service.Schedule("411f8274-5ee7-4bcc-8d43-e5214aa79aa7","aaaaaaaaaaa",DateTime.Now.AddSeconds(5), cancellationToken);

                var eventClass = await _repository.GetTracking(eventId);
                if (eventClass == null)
                    throw new Exception("ایونت یافت نشد!");

                List<Domain.UserAgg.User?> users = new List<Domain.UserAgg.User?>();
                foreach (var item in userIds)
                {
                    users.Add((await _userRepository.GetTrackingWithString(item)));
                    //users.Add((await _userRepository.GetTrackingWithString(item))!);
                }
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

                foreach (var item in users)
                {
                    if (item != null && item.Email != null)
                    {
                        mailMessage.To.Add(item.Email);
                    }
                }
                //mailMessage.To.Add(user.Email);

                await smtpClient.SendMailAsync(mailMessage); // استفاده از نسخه Async
                                                             //await _schedule.ScheduleEvent((startTime-sendTime),"",)


            }
            catch (Exception ex)
            {
                // برای دیباگ بهتر میتوانید خطا را لاگ کنید
                throw new Exception($"Error sending email: {ex.Message}");
            }
        }

        public async Task SendEmailForEvent(List<string> userNames, long eventId,
            DateTime startTime, DateTime sendTime, string creatorUserName)
        {
            try
            {
                //var q =   await _service.Schedule("411f8274-5ee7-4bcc-8d43-e5214aa79aa7","aaaaaaaaaaa",DateTime.Now.AddSeconds(5), cancellationToken);

                var eventClass = await _eventRepository.GetTracking(eventId);
                if (eventClass == null)
                    throw new Exception("ایونت یافت نشد!");

                List<Domain.UserAgg.User?> users = new List<Domain.UserAgg.User?>();
                foreach (var item in userNames)
                {
                    users.Add((await _userRepository.GetTrackingByUserName(item)));
                    //users.Add((await _userRepository.GetTrackingWithString(item))!);
                }
                var creator = await _userRepository.GetTrackingByUserName(creatorUserName);
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
                int SendEmailCount = 0;
                foreach (var item in users)
                {
                    if (item != null && item.Email != null)
                    {
                        mailMessage.To.Add(item.Email);
                        SendEmailCount++;
                    }
                }
                //mailMessage.To.Add(user.Email);
                //var creator = users.Where(i => i.UserName.Equals(creatorUserName)).FirstOrDefault();
                //تست این متد توی سه چهار تا پکیج و جندین ارسال ایمیل
                await SetChange(SendEmailCount, creator!);
                await smtpClient.SendMailAsync(mailMessage); // استفاده از نسخه Async
                await _userRepository.Save();
                //await _schedule.ScheduleEvent((startTime-sendTime),"",)


            }
            catch (Exception ex)
            {
                // برای دیباگ بهتر میتوانید خطا را لاگ کنید
                throw new Exception($"Error sending email: {ex.Message}");
            }
        }

        public Task SendSms(List<string> userIds, long eventId, DateTime startTime, DateTime sendTime)
        {
            throw new NotImplementedException();
        }


        internal async Task SetChange(int SendEmailCount,
            Domain.UserAgg.User creator)
         {

            var creatorPackages = creator.UserPackages.Where(i =>
            i.CreationDate + i.ExpiryDate > DateTime.Now).OrderBy(i => i.CreationDate)
                .FirstOrDefault();

            if (creatorPackages == null)
                throw new Exception("کاربر عزیز شما پکیج فعالی ندارید!");
            for (int i = 1; creatorPackages.AllowedEmailCount - SendEmailCount <= -10; i++)
            {
                SendEmailCount -= creatorPackages.AllowedEmailCount;
                creatorPackages.AllowedEmailCount = 0;
                creatorPackages = creator.UserPackages.Where(i =>
                        i.CreationDate + i.ExpiryDate > DateTime.Now).OrderBy(i => i.CreationDate)
                    .Skip(i).Take(1)
                    .FirstOrDefault();
                if (creatorPackages == null && SendEmailCount >= 10)
                {
                    int CountResult = await DeActiveLatestEventEmail(creator, SendEmailCount);
                    if (CountResult <= 0)
                    {
                        throw new Exception(
                        "تعداد درخواست ها برای ارسال ایمیل بیش تر از حد مجاز مصرفی شما است " +
                        "، ما نوتیفیکیشن آخرین ایونت شما از نظر تایمی رو غیرفعال کردیم بعد" +
                        " از شارژ حساب خود می توانید به صورت دستی نوتیفیکیشن ایونت خود را فعال کنید !");
                    }
                    else
                        throw new Exception(
                            "تعداد درخواست ها برای ارسال ایمیل بیش تر از حد مجاز مصرفی شما است");

                }

                else if (SendEmailCount <= 10 && SendEmailCount > 0)
                {
                    creatorPackages.AllowedEmailCount = -SendEmailCount;
                }
            }


            creatorPackages.AllowedEmailCount -= SendEmailCount;
            await _userRepository.Save();
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
                            var count = item.eventUser.Count();
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
