using Common.Application;
using Domain.UserAgg;
using Domain.UserAgg.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.User.SetAvatar
{
    public class SetAvatarCommand : IBaseCommand
    {
        public string UserName { get; set; }
        public string Avatar { get; set; }
        //public UserAvatar Avatar { get; set; }
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
            UserAvatar.Avatar avatar = UserAvatar.Avatar.Default;
            switch (request.Avatar)
            {
                case "Default":
                    avatar = UserAvatar.Avatar.Default;
                    break;
                case "Girl":
                    avatar = UserAvatar.Avatar.Girl;
                    break;
                case "Boy":
                    avatar = UserAvatar.Avatar.Boy;
                    break;
                case "Man":
                    avatar = UserAvatar.Avatar.Man;
                    break;
                case "Woman":
                    avatar = UserAvatar.Avatar.Woman;
                    break;
                default:
                    avatar = UserAvatar.Avatar.Default;
                    break;

            }
            if (user == null)
                return OperationResult.NotFound();

            user.SetAvatar(new UserAvatar(avatar));
            await _repository.Save();

            return OperationResult.Success();
        }
    }
}
