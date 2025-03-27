using Common.Application;
using Domain.UserAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User._RequestBox.RemoveRequestForSender
{
    public class RemoveRequestForSenderCommand:IBaseCommand
    {
        public string userName { get; set; }
        public string userNameFriend { get; set; }
    }
    internal class RemoveRequestForSenderCommandHandler : IBaseCommandHandler<RemoveRequestForSenderCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public RemoveRequestForSenderCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(RemoveRequestForSenderCommand request, CancellationToken cancellationToken)
        {
            var receiver = await _repository.GetTrackingByUserName(request.userNameFriend);
            if (receiver != null)
            {
                var user = await _repository.GetTrackingByUserName(request.userName);
                receiver.RemoveRequest(receiver.Id, user.Id);
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
