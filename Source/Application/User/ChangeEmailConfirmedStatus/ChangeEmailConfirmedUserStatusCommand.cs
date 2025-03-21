using Common.Application;
using Domain.UserAgg.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.ChangeEmailConfirmedStatus
{
    public class ChangeEmailConfirmedUserStatusCommand : IBaseCommand
    {
        public required String UserId { get; set; }
    }
    internal class ChangeEmailConfirmedUserStatusCommandHandler : IBaseCommandHandler<ChangeEmailConfirmedUserStatusCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public ChangeEmailConfirmedUserStatusCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ChangeEmailConfirmedUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByFilterAsync(i => i.Id == request.UserId);
            if (user == null)
                return OperationResult.NotFound();

            user.ChangeEmailConfirmedStatus();
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
