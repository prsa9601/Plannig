using Common.Application;
using Domain.UserAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.ChangePhoneNumberConfirmedStatus
{
    public class ChangePhoneNumberConfirmedStatusCommand : IBaseCommand
    {
        public required string UserId { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
    }
    internal class ChangePhoneNumberConfirmedStatusCommandHandler : IBaseCommandHandler<ChangePhoneNumberConfirmedStatusCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public ChangePhoneNumberConfirmedStatusCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ChangePhoneNumberConfirmedStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByFilterAsync(i => i.Id == request.UserId);
            if (user == null)
                return OperationResult.NotFound();

            user.ChangePhoneNumberConfirmedStatus(request.PhoneNumberConfirmed);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
