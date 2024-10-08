using Common.Application;
using Domain.UserAgg.Repository;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;

namespace Application.User.ForgotPassword
{
    public class UserForgotPasswordCommand : IBaseCommand
    {
        public string Email { get; set; }
    }
    public class UserForgotPasswordCommandHandler : IBaseCommandHandler<UserForgotPasswordCommand>
    {
        private readonly UserManager<Domain.UserAgg.User> _userManager;
        private readonly SignInManager<Domain.UserAgg.User> _signInManager;
        private readonly IUserRepository<Domain.UserAgg.User> _repository;
        //private readonly IEmailSender _emailSender;
        public UserForgotPasswordCommandHandler(SignInManager<Domain.UserAgg.User> signInManager, UserManager<Domain.UserAgg.User> userManager, IUserRepository<Domain.UserAgg.User> repository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            //_emailSender = emailSender;, IEmailSender emailSender
            _repository = repository;
        }
        public async Task<OperationResult> Handle(UserForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                 var user = await _userManager.FindByEmailAsync(request.Email);
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
                mailMessage.To.Add(request.Email);

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
 