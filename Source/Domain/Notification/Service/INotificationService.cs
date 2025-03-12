using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Notification.Service
{
    public interface INotificationService
    {
        //میتونم endTime  هم بزار
        Task SendEmail(List<string> userIds, long eventId,
            DateTime startTime, DateTime sendTime);

        Task SendEmailForEvent(List<string> userIds, 
            long eventId, DateTime startTime, DateTime sendTime, string creatorUserName);

        Task SendSms(List<string> userIds, long eventId,
            DateTime startTime, DateTime sendTime);
    
    }
}

//try
//{
//    //var q =   await _service.Schedule("411f8274-5ee7-4bcc-8d43-e5214aa79aa7","aaaaaaaaaaa",DateTime.Now.AddSeconds(5), cancellationToken);

//    var user = await _userManager.FindByIdAsync(request.Id.ToString());
//    if (user == null)
//        return OperationResult.NotFound();

//    if (string.IsNullOrWhiteSpace(user.Email))
//        return OperationResult.Error("User email is not available");

//    using var smtpClient = new SmtpClient("smtp.gmail.com")
//    {
//        Port = 587,
//        Credentials = new NetworkCredential(
//            "parsa9601m@gmail.com",
//            "leis nqek fthg fqbo"), // استفاده از پسورد مخصوص اپلیکیشن
//        EnableSsl = true
//    };

//    using var mailMessage = new MailMessage
//    {
//        From = new MailAddress("parsa9601m@gmail.com"),
//        //To = {  "parsahavaset1@gmail.com","1234z1234@gmail.com"},
//        To =
//        {
//            "parsahavaset1@gmail.com",
//            "www.a1234z1234@gmail.com",
//            "parham1234122@gmail.com",
//            "parham09332294129@gmail.com"
//        },
//        Subject = "Reminded",
//        Body = "<i>عاااااااااااااااااااااااا</i>",
//        IsBodyHtml = true
//    };
//    mailMessage.To.Add(user.Email);

//    await smtpClient.SendMailAsync(mailMessage); // استفاده از نسخه Async
//    return OperationResult.Success();
//}
//catch (Exception ex)
//{
//    // برای دیباگ بهتر میتوانید خطا را لاگ کنید
//    return OperationResult.Error($"Error sending email: {ex.Message}");
//}
