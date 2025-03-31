using Microsoft.Extensions.Caching.Memory;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Common.Application;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Formats.Asn1;
using Common.Application.SecurityUtil;
using AngleSharp.Dom;
using Newtonsoft.Json.Linq;

namespace Application.User.SendVerificationEmailToken
{
    public class SendVerificationEmailCodeCommand : IBaseCommand
    {
        public string? UserId { get; set; }
    }
    public class SendVerificationEmailCodeCommandHandler : IBaseCommandHandler<SendVerificationEmailCodeCommand>
    {
        private readonly IMemoryCache _cache;
        private readonly UserManager<Domain.UserAgg.User> _userManager;

        public SendVerificationEmailCodeCommandHandler(UserManager<Domain.UserAgg.User> manager,
            IMemoryCache cache)
        {
            _userManager = manager;
            _cache = cache;
        }

        public async Task<OperationResult> Handle(SendVerificationEmailCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId!);
            if (user == null)
                return OperationResult.NotFound();


            var verificationEmailToken = GenerateSecureRandomCode(37);

            if (verificationEmailToken == null)
                return OperationResult.Error();



            _cache.Set($"VerificationEmailToken-{Sha256Hasher.Hash(user.UserName)}", verificationEmailToken, TimeSpan.FromMinutes(3));
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
                Body = $@"
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
            <h1>تأیید ایمیل شما</h1>
        </div>
        <div class='content'>
            <p>،سلام کاربر گرامی</p>
            <p>از ثبت‌نام شما سپاسگزاریم! برای تکمیل فرایند ثبت‌نام و فعال‌سازی حساب کاربری خود، لطفاً روی دکمه زیر کلیک کنید:</p>

           <a href='https://localhost:5250/auth/VerifyEmail?token={verificationEmailToken}' class='btn'>تأیید ایمیل</a>
            <p>توجه: این لینک فقط تا مدت محدودی معتبر است. لطفاً هرچه سریع‌تر اقدام کنید.</p>
            <p>در صورت دریافت این ایمیل به اشتباه، نیازی به انجام هیچ کاری نیست.</p>
        </div>
        <div class='footer'>
            <p>© 2025 شرکت شما. تمامی حقوق محفوظ است.</p>
            <p>برای اطلاعات بیشتر، لطفاً به <a href='https://yoursite.com'>وب‌سایت ما</a> مراجعه کنید.</p>
        </div>
    </div>
</body>
</html>",

                IsBodyHtml = true
            };

            mailMessage.To.Add(user.Email);

            await smtpClient.SendMailAsync(mailMessage); // استفاده از نسخه Async
                                                         //await _schedule.ScheduleEvent((startTime-sendTime),"",)

            return OperationResult.Success(verificationEmailToken);
        }
        public string GenerateSecureRandomCode(int length)
        {
            const string chars = "aqwszxdecrfvbgtyhnjumk@+_-iol/pABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var data = new byte[length];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            return new string(data.Select(b => chars[b % chars.Length]).ToArray());
        }
    }
}


//using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:5009"))
//{
//    IDatabase db = redis.GetDatabase();
//    // عملیات‌های Redis خود را اینجا انجام دهید

//    // سریال‌سازی به JSON
//    string userJson = JsonConvert.SerializeObject(verificationEmailToken);

//    // ذخیره در Redis
//    db.StringSet($"KeyForMe:{Common.Application.
//        SecurityUtil.Sha256Hasher.Hash(user.UserName)}",
//        verificationEmailToken,TimeSpan.FromMinutes(3));
//}           