using Common.Application;
using Domain.PackageAgg.Repository;
using Domain.UserAgg;
using Domain.UserAgg.Repository;
using Hangfire;
using static Domain.PackageAgg.Package;

namespace Application.User._UserPackage.SetPackage
{
    public class SetUserPackageCommand : IBaseCommand
    {
        public long packageId { get; set; }
        //public string packageTitle { get; set; }
        //public ExpiryTime expireTime { get; set; }
        //public int AllowedSmsCount { get; set; }
        //public int AllowedEmailCount { get; set; }
        public string userId { get; set; }
    }
    internal class SetUserPackageCommandHandler : IBaseCommandHandler<SetUserPackageCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;
        private readonly IPackageRepository _packageRepository;

        public SetUserPackageCommandHandler(IUserRepository<Domain.UserAgg.User> repository, IPackageRepository packageRepository)
        {
            _repository = repository;
            _packageRepository = packageRepository;
        }

        public async Task<OperationResult> Handle(SetUserPackageCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingWithString(request.userId);
            if (user == null)
                return OperationResult.NotFound("کاربر یافت نشد.");
            var package = await _packageRepository.GetTracking(request.packageId);
            if (package == null)
                return OperationResult.NotFound("پکیج یافت نشد.");

            DateTime expiryDate = package.ExpiryDate switch
            {
                ExpiryTime.روزانه => DateTime.Now.AddDays(1),
                ExpiryTime.ماهانه => DateTime.Now.AddMonths(1),
                ExpiryTime.سه_ماهه => DateTime.Now.AddMonths(3),
                ExpiryTime.شش_ماهه => DateTime.Now.AddMonths(6),
                ExpiryTime.یک_ساله => DateTime.Now.AddYears(1),
                _ => throw new ArgumentException("Invalid expiry time")
            };
        
            user.SetUserPackage
            (expiryDate, request.packageId
                , package.AllowedSmsCount, package.AllowedEmailCount, package.Title);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
