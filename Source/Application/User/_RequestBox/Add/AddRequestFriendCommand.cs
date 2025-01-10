using Common.Application;
using Domain.UserAgg.Repository;
using Domain.UserAgg.Service;

namespace Application.User._RequestBox.Add
{
    public class AddRequestFriendCommand : IBaseCommand
    {
        //public string Id { get; set; }
        public string userName { get; set; }
        public string userNameFriend { get; set; }
    }
    public class AddRequestFriendCommandHandler : IBaseCommandHandler<AddRequestFriendCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public AddRequestFriendCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }
        public async Task<OperationResult> Handle(AddRequestFriendCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var receiver = await _repository.GetTrackingByUserName(request.userNameFriend);
                if (string.IsNullOrWhiteSpace(request.userName))
                {
                    return OperationResult.NotFound("خطایی سمت سرور رخ داده در اسرع وقت درستش میکنیم.");
                }
                else if (string.IsNullOrWhiteSpace(request.userNameFriend))
                {
                    return OperationResult.NotFound("کاربر مورد نظر یافت نشد.");
                }
                if (receiver != null)
                {
                    var user = await _repository.GetTrackingByUserName(request.userName);
                    receiver.AddRequest(user.UserName,user.Id);
                    //user.AddRequest(receiver.Id);
                    await _repository.Save();
                    return OperationResult.Success();
                }
                else
                {
                    return OperationResult.NotFound("کاربر مورد نظر یافت نشد!");
                }

                return OperationResult.NotFound();
            }
            catch (Exception e)
            {
                return OperationResult.Error(e.Message.ToString());
            }
        }
    }
}
