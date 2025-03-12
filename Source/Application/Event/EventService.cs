using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Notification.EmailSender;
using Common.Application;
using Domain.EventAgg.Service;
//using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Event
{
    public class EventService : IEventService
    {
        //private readonly IBackgroundJobClient _backgroundJob;
        private readonly UserManager<Domain.UserAgg.User> _userManager;

        public EventService(UserManager<Domain.UserAgg.User> userManager)
        {
            //_backgroundJob = backgroundJob;
            _userManager = userManager;
        }

        //public async Task<string> Schedule(string id, string contentMessage, DateTime startTime, CancellationToken cancel)
        //{
        //    //if (command == null) throw new ArgumentNullException(nameof(command));
        //    var time = DateTime.Now.AddSeconds(10);
        //    var jobId = _backgroundJob.Schedule<IEventService>(
        //        service => service.SendEmail(id,contentMessage, startTime, cancel),
        //        time
        //    );

        //    return jobId;

        //}

        public async Task<string> SendEmail(string id, string contentMessage, DateTime startTime, CancellationToken cancel)
        {

            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                    return "کاربری یافت نشد.";

                if (string.IsNullOrWhiteSpace(user.Email))
                    return "User email is not available";

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
                mailMessage.To.Add(user.Email);

                await smtpClient.SendMailAsync(mailMessage); // استفاده از نسخه Async
                return "عملیات با موفقیت انجام شد.";
            }
            catch (Exception ex)
            {
                // برای دیباگ بهتر میتوانید خطا را لاگ کنید
                return $"Error sending email: {ex.Message}";
            }

        }
    }
}
