using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;
using IPE.SmsIrClient.Models.Requests;
using IPE.SmsIrClient;

namespace Application.Notification.SmsSender
{
    public class SendNotificationWithSms :IBaseCommand
    {
    }
    internal class SedNotificationWithSmsHandler :IBaseCommandHandler<SendNotificationWithSms>
    {
        public async Task<OperationResult> Handle(SendNotificationWithSms request, CancellationToken cancellationToken)
        {

            //HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("x-api-key", "kj8zpqqfqaTEbMVSmg1AvRxAehpeVJdcthktKFHykuBGFbhP"); // API Key واقعی شما

            //var payload = @"{" + "\n" +
            //              @"  ""lineNumber"": 3000000000," + "\n" + // شماره خط اختصاصی شما
            //              @"  ""messageTexts"": [" + "\n" +
            //              @"    ""سلام آقای احمدی، قرار فردا ساعت 16 به 18 تغییر کرد.""," + "\n" +
            //              @"    ""کد ورود شما: 4859""," + "\n" +
            //              @"    ""کد ورود شما: 4859""" + "\n" +
            //              @"  ]," + "\n" +
            //              @"  ""mobiles"": [" + "\n" +
            //              @"    ""+9101450424""," + "\n" + // پیش شماره 98 برای ایران + شماره 9121234567
            //              @"    ""+9197082714""," + "\n" +
            //              @"    ""+9193400726""" + "\n" +
            //              @"  ]," + "\n" +
            //              @"  ""sendDateTime"": null" + "\n" + // یا زمان خاص مثل "2024-03-15T14:30:00"
            //              @"}";

            //HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            //var response = await httpClient.PostAsync("https://api.sms.ir/v1/send/likeToLike", content);
            //var result = await response.Content.ReadAsStringAsync();
            SmsIr smsIr = new SmsIr("kj8zpqqfqaTEbMVSmg1AvRxAehpeVJdcthktKFHykuBGFbhP");
            //var bulkSendResult = await smsIr.BulkSendAsync(95007079000006
            //    , "پیام تستی عاااااااااا", new string[] { "989101450424" });
       
            var verificationSendResult = await smsIr.VerifySendAsync("989101450424", 
                100000, new VerifySendParameter[] { new("Code", "12345") });
            return OperationResult.Success();
        }
    }
}
