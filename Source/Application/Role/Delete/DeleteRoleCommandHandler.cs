using Common.Application;
using Domain.RoleAgg.Repository;
using Microsoft.AspNetCore.Identity;

namespace Application.Role.Delete
{
    public class DeleteRoleCommandHandler : IBaseCommandHandler<DeleteRoleCommand>
    {
        private readonly IRoleRepository<Domain.RoleAgg.Role> _repository;
        private readonly RoleManager<Domain.RoleAgg.Role> _roleManager;
        public DeleteRoleCommandHandler(IRoleRepository<Domain.RoleAgg.Role> repository, RoleManager<Domain.RoleAgg.Role> roleManager)
        {
            _repository = repository;
            _roleManager = roleManager;
        }
        public async Task<OperationResult> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            //var result = await _repository.DeleteRole(request.roleId);
            //if (!result)
            //    return OperationResult.Error();
            var role = await _repository.GetTrackingWithString(request.RoleId);
            if (role == null)
                return OperationResult.NotFound();
            await _roleManager.DeleteAsync(role);
            return OperationResult.Success();
        }
    }
}
