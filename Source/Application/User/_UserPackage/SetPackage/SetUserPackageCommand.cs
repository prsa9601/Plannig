using Common.Application;
using Domain.UserAgg;
using Domain.UserAgg.Repository;
using Hangfire;
using static Domain.PackageAgg.Package;

namespace Application.User._UserPackage.SetPackage
{
    public class SetUserPackageCommand : IBaseCommand
    {
        public long packageId { get; set; }
        public string packageTitle { get; set; }
        public ExpiryTime expireTime { get; set; }
        public int AllowedSmsCount { get; set; }
        public int AllowedEmailCount { get; set; }
        public string userId { get; set; }
    }
    internal class SetUserPackageCommandHandler : IBaseCommandHandler<SetUserPackageCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public SetUserPackageCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SetUserPackageCommand request, CancellationToken cancellationToken)
        {
            DateTime expiryDate = request.expireTime switch
            {
                ExpiryTime.روزانه => DateTime.Now.AddDays(1),
                ExpiryTime.ماهانه => DateTime.Now.AddMonths(1),
                ExpiryTime.سه_ماهه => DateTime.Now.AddMonths(3),
                ExpiryTime.شش_ماهه => DateTime.Now.AddMonths(6),
                ExpiryTime.یک_ساله => DateTime.Now.AddYears(1),
                _ => throw new ArgumentException("Invalid expiry time")
            };
            var user = await _repository.GetTrackingWithString(request.userId);
            if (user == null)
                return OperationResult.NotFound("کاربر یافت نشد.");
            user.SetUserPackage
            (expiryDate, request.packageId
                , request.AllowedSmsCount, request.AllowedEmailCount, request.packageTitle);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
