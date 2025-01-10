using Common.Application;
using Domain.RoleAgg.Enums;

namespace Application.Role.Edit
{
    public record EditRoleCommand(string Id, string Name, List<Permission> Permissions) : IBaseCommand;

}
