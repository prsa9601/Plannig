using Common.Application;
using Domain.RoleAgg;
using Domain.RoleAgg.Repository;
using Microsoft.AspNetCore.Identity;

namespace Application.Role.Create
{
    public class CreateRoleCommandHandler : IBaseCommandHandler<CreateRoleCommand>
    {
        private readonly IRoleRepository<Domain.RoleAgg.Role> _repository;
        private readonly RoleManager<Domain.RoleAgg.Role> _roleManager;
        public CreateRoleCommandHandler(IRoleRepository<Domain.RoleAgg.Role> repository, RoleManager<Domain.RoleAgg.Role> roleManager)
        {
            _repository = repository;
            _roleManager = roleManager;
        }

        public async Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var permissions = new List<RolePermission>();
            request.Permissions.ForEach(f =>
            {
                permissions.Add(new RolePermission(f));
            });
            // var role = ;
            //_repository.Add(role);
            var result = await _roleManager.CreateAsync(new Domain.RoleAgg.Role(request.Name, permissions));

            //await _repository.Save();
            return OperationResult.Success();
        }
    }
}
