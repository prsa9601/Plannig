using Common.Application;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;

namespace Application.Notification.EmailSender
{
    public class SendNotificationByEmail : IBaseCommand
    {
        public long Id { get; set; }
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

                var password = user.Password;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("parsa9601m@gmail.com", "@mp9601"),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("parsa9601m@gmail.com"),
                    Subject = "Reminded",
                    Body = "<i>This is a test email from your C# application.</i>"
                };
                mailMessage.To.Add(user.Email);

                smtpClient.Send(mailMessage);
                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Error sending email: {ex.Message}");
            }
        }
    }
}
