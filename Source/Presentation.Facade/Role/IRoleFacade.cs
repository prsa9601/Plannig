using Application.Role.Create;
using Application.Role.Delete;
using Application.Role.Edit;
using Common.Application;
using Query.Role.DTOs;

namespace Presentation.Facade.Role
{
    public interface IRoleFacade
    {
        Task<OperationResult> CreateRole(CreateRoleCommand command);
        Task<OperationResult> EditRole(EditRoleCommand command);
        Task<OperationResult> DeleteRole(DeleteRoleCommand command);

        Task<RoleDto?> GetRoleById(string roleId);
        Task<RoleFilterResult?> GetRolesByFilter(RoleFilterParam param);
        Task<List<RoleDto>?> GetRoles();
    }
}
