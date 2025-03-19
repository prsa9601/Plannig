using Common.Application;
using Domain.UserAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.SetRole
{
    public class SetUserRoleCommand : IBaseCommand
    {
        public string userId { get; set; }
        public List<string> rolesId { get; set; }
    }
    internal class SetUserRoleCommandHandler : IBaseCommandHandler<SetUserRoleCommand>
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public SetUserRoleCommandHandler(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTrackingWithString(request.userId);
            user.SetUserRoles(request.rolesId);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
