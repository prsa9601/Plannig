using Common.Application;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using Application.User.SendVerificationEmailToken;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Formats.Asn1;
using Common.Application.SecurityUtil;
using Domain.UserAgg.Repository;

namespace Application.User.VerificationEmail
{
    public class VerificationEmailCommand : IBaseCommand
    {
        public required string UserId { get; set; }
        public required string VerificationEmailToken { get; set; }
    }
    internal class VerificationEmailCommandHandler : IBaseCommandHandler<VerificationEmailCommand>
    {
        private readonly IMemoryCache _cache;
        private readonly UserManager<Domain.UserAgg.User> _userManager;
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public VerificationEmailCommandHandler(UserManager<Domain.UserAgg.User> manager,
            IMemoryCache cache,
            IUserRepository<Domain.UserAgg.User> repository)
        {
            _userManager = manager;
            _cache = cache;
            _repository = repository;
        }

        public async Task<OperationResult> Handle(VerificationEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingWithString(request.UserId);
            if (user == null)
                return OperationResult.NotFound();

            var token = _cache.Get($"VerificationEmailToken-{Sha256Hasher.Hash(user.UserName!)}");

            if (token == null)
                return OperationResult.NotFound("خطایی سمت سرور رخ داده لطفا مجددا تلاش نمایید!");


            if (request.VerificationEmailToken.Equals(token.ToString()))
            {
                user.ChangeEmailConfirmedStatus(true);
                await _repository.Save();
                return OperationResult.Success("ایمیل شما با موفقیت تایید شد.");
            }
            else
            {

                return OperationResult.Error("توکن ها با هم مطابقت ندارن!");
            }




        }
    }



}
//using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:5009"))
//{
//    IDatabase db = redis.GetDatabase();
//    // عملیات‌های Redis خود را اینجا انجام دهید


//    // بازیابی داده از Redis
//    string tokenFromRedis = db.StringGet($"KeyForMe:" +
//        $"{Sha256Hasher.Hash(user.UserName!)}")!;

//    // تبدیل از JSON به شیء
//    string tokenredis = JsonConvert.DeserializeObject<string>(tokenFromRedis);


//}