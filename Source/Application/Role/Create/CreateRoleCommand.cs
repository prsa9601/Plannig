using Common.Application;
using Domain.RoleAgg.Enums;

namespace Application.Role.Create
{
    public record CreateRoleCommand(string Name, List<Permission> Permissions) : IBaseCommand;

}
