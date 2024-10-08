using Common.Application;
using Domain.UserAgg;
using Domain.UserAgg.Repository;

namespace Application.User.SetAvatar
{
    public class SetAvatarCommand : IBaseCommand
    {
        public string UserName { get; set; }
        public UserAvatar Avatar { get; set; }
    }
    public class SetAvatarCommandHandler : IBaseCommandHandler<SetAvatarCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public SetAvatarCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SetAvatarCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingByUserName(request.UserName);
            
            if (user == null)
                return OperationResult.NotFound();

            user.SetAvatar(request.Avatar);
            await _repository.Save();

            return OperationResult.Success();
        }
    }
}
