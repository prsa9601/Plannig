using System.Linq;
using System.Net.Mail;
using System.Net;
using Domain.UserAgg.Repository;
using Microsoft.Extensions.Logging;
using Domain.UserAgg;
using Domain.UserAgg.Service;

namespace Application.User._Notification.Services
{
    public class UserNotificationDomainService : IUserNotificationDomainService
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;
        private readonly ILogger _logger;
        public UserNotificationDomainService(IUserRepository<Domain.UserAgg.User> repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task SendEmail(long UserNotificationId)
        {
            var users = await _repository.GetListByFilterAsync
                (i => i.UserNotifications.Any(x => x.Id.Equals(UserNotificationId)));

            users.
                ForEach(i => i.UserNotifications.Where(n => n.Id == UserNotificationId)
                .ToList().ForEach(x => x.SendNotification()));

            var userNotification = users.
                 Where(i => i.UserNotifications.Any(n => n.Id == UserNotificationId))
                 .SelectMany(i => i.UserNotifications).ToList();


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

                Subject = "تایید ایمیل",

                //Body = ,

                IsBodyHtml = true
            };
            foreach (var notification in userNotification)
            {
                foreach (var userId in notification.UserIds)
                {
                    var user = users.FirstOrDefault(i => i.Id.Equals(userId));
                    mailMessage.Body = $@"
<html dir='rtl' lang='fa'>
<head>
    <style>
        @font-face {{
            font-family: 'IRANSansWeb';
            src: url('https://cdn.fontiran.com/public/woff2/IRANSansWeb.woff2') format('woff2');
        }}
        body {{
            font-family: 'IRANSansWeb', Tahoma, Arial, sans-serif;
            background-color: #f4f5f7;
            margin: 0;
            padding: 0;
            color: #333;
        }}
        .email-container {{
            max-width: 700px;
            margin: 50px auto;
            background-color: #ffffff;
            border-radius: 15px;
            overflow: hidden;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            background-color: #00bcd4;
            padding: 20px;
            text-align: center;
            color: white;
        }}
        .header h1 {{
            font-size: 28px;
            margin: 0;
        }}
        .content {{
            padding: 30px;
            text-align: right;
        }}
        .content p {{
            font-size: 18px;
            margin-bottom: 20px;
            line-height: 1.8;
            color: #555;
        }}
        .content .btn {{
            display: inline-block;
            padding: 15px 30px;
            font-size: 18px;
            color: white;
            background-color: #00bcd4;
            text-decoration: none;
            border-radius: 50px;
            transition: all 0.3s ease-in-out;
            margin-top: 20px;
        }}
        .content .btn:hover {{
            background-color: #008c9e;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.2);
        }}
        .footer {{
            padding: 25px;
            text-align: center;
            font-size: 14px;
            color: #999;
            background-color: #f9f9f9;
        }}
        .footer a {{
            color: #00bcd4;
            text-decoration: none;
        }}
        .footer a:hover {{
            text-decoration: underline;
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='header'>
            <h1>{notification.Title}</h1>
        </div>
        <div class='content'>
            <p>،سلام کاربر گرامی</p>
<p> {user.UserName}</p>
            <p>{notification.Description}</p>

           <a href='https://localhost:5250/auth/VerifyEmail?token={users}' class='btn'>تأیید ایمیل</a>
            <p>توجه: این لینک فقط تا مدت محدودی معتبر است. لطفاً هرچه سریع‌تر اقدام کنید.</p>
            <p>در صورت دریافت این ایمیل به اشتباه، نیازی به انجام هیچ کاری نیست.</p>
        </div>
        <div class='footer'>
            <p>© 2025 شرکت شما. تمامی حقوق محفوظ است.</p>
            <p>برای اطلاعات بیشتر، لطفاً به <a href='https://yoursite.com'>وب‌سایت ما</a> مراجعه کنید.</p>
        </div>
    </div>
</body>
</html>";
                    mailMessage.To.Add("emailuser");
                }
            }



            await smtpClient.SendMailAsync(mailMessage); // استفاده از نسخه Async

        }

        public async Task SendNotification(long UserNotificationId)
        {
            var usernotification = await _repository.GetListByFilterAsync
                (i => i.UserNotifications.Any(x => x.Id.Equals(UserNotificationId)));

            //if (usernotification.Count() == 0)

            usernotification.ForEach(i => i.UserNotifications.ForEach(x => x.SendNotification()));
        }

        public Task SendSms(long UserNotificationId)
        {
            throw new NotImplementedException();
        }
    }
}
