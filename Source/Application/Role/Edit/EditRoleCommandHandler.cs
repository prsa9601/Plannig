using Common.Application;
using Domain.RoleAgg;
using Domain.RoleAgg.Repository;
using Microsoft.AspNetCore.Identity;

namespace Application.Role.Edit
{
    public class EditRoleCommandHandler : IBaseCommandHandler<EditRoleCommand>
    {
        private readonly IRoleRepository<Domain.RoleAgg.Role> _repository;
        private readonly RoleManager<Domain.RoleAgg.Role> _roleManager;
        public EditRoleCommandHandler(IRoleRepository<Domain.RoleAgg.Role> repository, RoleManager<Domain.RoleAgg.Role> roleManager)
        {
            _repository = repository;
            _roleManager = roleManager;
        }

        public async Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetTrackingWithString(request.Id);
            if (role == null)
                return OperationResult.NotFound();
            //role.Edit(request.Title);

            var permissions = new List<RolePermission>();
            request.Permissions.ForEach(f =>
            {
                permissions.Add(new RolePermission(f));
            });
            role.SetPermissions(permissions);
            role.Name = request.Name;
            await _roleManager.UpdateAsync(role);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
