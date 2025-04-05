using Common.Application;
using Common.Application.SecurityUtil;
using Domain.UserAgg.Repository;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.SendEmailForForgotPassword
{
    public class VerifiedEmailForgotPasswordCommand : IBaseCommand
    {
        public required string Email { get; set; }
        public required string VerificationEmailToken { get; set; }
    }
    internal class VerifiedEmailForgotPasswordCommandHandler : IBaseCommandHandler<VerifiedEmailForgotPasswordCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;
        private readonly IMemoryCache _cache;

        public VerifiedEmailForgotPasswordCommandHandler(IUserRepository<Domain.UserAgg.User> repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<OperationResult> Handle(VerifiedEmailForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByFilterAsync(i=>i.Email.Equals(request.Email));
            if (user == null)
                return OperationResult.NotFound();

            var token = _cache.Get($"ForgotPassword-{Sha256Hasher.Hash(user.UserName!)}");

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
