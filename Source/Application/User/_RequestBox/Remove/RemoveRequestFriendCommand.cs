using Common.Application;
using Domain.UserAgg.Repository;

namespace Application.User._RequestBox.Remove
{
    public class RemoveRequestFriendCommand : IBaseCommand
    {
        public string userName { get; set; }
        public string userNameFriend { get; set; }
    }
    public class RemoveRequestFriendCommandHandler : IBaseCommandHandler<RemoveRequestFriendCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public RemoveRequestFriendCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }
        public async Task<OperationResult> Handle(RemoveRequestFriendCommand request, CancellationToken cancellationToken)
        {
            var receiver = await _repository.GetTrackingByUserName(request.userNameFriend);
            if (receiver != null)
            {
                var user = await _repository.GetTrackingByUserName(request.userName);
                user.RemoveRequest(receiver.Id, user.Id);
               // receiver.RemoveRequest(user.Id, receiver.Id);
                await _repository.Save();
                return OperationResult.Success();
            }
            else
            {
                return OperationResult.NotFound("کاربر مورد نظر یافت نشد!");
            }

            return OperationResult.NotFound();
        }
    }
}
