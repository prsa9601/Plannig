using Common.Application;
using Domain.UserAgg.Repository;

namespace Application.User._UserPackage.EditPackage
{
    public class EditUserPackageCommand : IBaseCommand
    {
        public long packageId { get; set; }
        public string userId { get; set; }
    }
    internal class EditUserPackageCommandHandler : IBaseCommandHandler<EditUserPackageCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public EditUserPackageCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(EditUserPackageCommand request, CancellationToken cancellationToken)
        {
            //Get Redis
            var user = await _repository.GetTrackingWithString(request.userId);
            if (user == null) 
                return OperationResult.NotFound();
            //user.EditUserPackage(request.packageId);
            return OperationResult.Success();
        }
    }
}
