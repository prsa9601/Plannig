using Common.Application;
using Domain.UserAgg.Repository;

namespace Application.User._Friend.Add
{
    public record class AddFriendsUserCommand(string FriendUserName, string SenderId) : IBaseCommand;

    public class AddFriendsUserCommandHandler : IBaseCommandHandler<AddFriendsUserCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public AddFriendsUserCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(AddFriendsUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingWithString(request.SenderId);
            var friend = await _repository.GetTrackingByUserName(request.FriendUserName);

            if (friend == null)
                return OperationResult.NotFound();

            if (user == null)
                return OperationResult.NotFound();

            user.AddFriend(friend.Id);
            //friend.AddFriend(user.Id);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
