using Common.Application;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;

namespace Application.Notification.EmailSender
{
    public class SendNotificationByEmail : IBaseCommand
    {
        public string Id { get; set; }
    }
    public class SendNotificationByEmailHandler : IBaseCommandHandler<SendNotificationByEmail>
    {
        private readonly UserManager<Domain.UserAgg.User> _userManager;

        public SendNotificationByEmailHandler(UserManager<Domain.UserAgg.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OperationResult> Handle(SendNotificationByEmail request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.Id.ToString());
                if (user == null)
                    return OperationResult.NotFound();

                if (string.IsNullOrWhiteSpace(user.Email))
                    return OperationResult.Error("User email is not available");

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
                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                // برای دیباگ بهتر میتوانید خطا را لاگ کنید
                return OperationResult.Error($"Error sending email: {ex.Message}");
            }
        }
    }
}
