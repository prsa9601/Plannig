using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;
using Domain.UserAgg.Repository;
using Domain.UserAgg.Service;
using Hangfire;

namespace Application.User._UserPackage.DeActivePackage
{
    public class DeActiveUserPackageCommand : IBaseCommand
    {
        public string userId { get; set; }
        public long packageId { get; set; }
    }
    internal class DeActiveUserPackageCommandHandler : IBaseCommandHandler<DeActiveUserPackageCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;
        private readonly IUserService _service;

        public DeActiveUserPackageCommandHandler(IUserRepository<Domain.UserAgg.User> repository, IUserService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(DeActiveUserPackageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _repository.GetWithStringAsync(request.userId);
                if (user == null)
                    return OperationResult.NotFound();
                
                var packageUser = user.UserPackages.FirstOrDefault
                    (i => i.PackageId == request.packageId);

                if (packageUser == null)
                    return OperationResult.NotFound();

                DateTime expireDate = packageUser.CreationDate + packageUser.ExpiryDate;
                BackgroundJob.Schedule<IUserService>(service =>
                service.DeActiveUserPackage(request.userId),
                   expireDate );
                //BackgroundJob.Schedule<IUserService>(service =>
                //service.DeActiveUserPackage(request.userId),
                //DateTime.Now.AddSeconds(15));

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }

        }
    }
}
