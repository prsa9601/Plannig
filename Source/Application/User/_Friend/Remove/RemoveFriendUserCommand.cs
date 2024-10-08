using Common.Application;
using Domain.UserAgg.Repository;

namespace Application.User._Friend.Remove
{
    public record class RemoveFriendUserCommand(string UserName, string FriendName) : IBaseCommand;

    public class RemoveFriendUserCommandHandler : IBaseCommandHandler<RemoveFriendUserCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public RemoveFriendUserCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveFriendUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingByUserName(request.UserName);
            var friend = await _repository.GetTrackingByUserName(request.FriendName);
            if (friend == null)
                return OperationResult.NotFound();
            if (user == null)
                return OperationResult.NotFound();
            //friend.RemoveFriend(user.Id);
            user.RemoveFriend(friend.Id);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
