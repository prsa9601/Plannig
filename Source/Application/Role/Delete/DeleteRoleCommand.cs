using Common.Application;

namespace Application.Role.Delete
{
    public record class DeleteRoleCommand(string RoleId) : IBaseCommand;
}
