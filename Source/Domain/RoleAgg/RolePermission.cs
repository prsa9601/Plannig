using Common.Domain;
using Domain.RoleAgg.Enums;

namespace Domain.RoleAgg
{
    public class RolePermission : BaseEntity
    {
        public RolePermission(Permission permission)
        {
            Permission = permission;
        }

        public string RoleId { get; internal set; }
        public Permission Permission { get; private set; }
    }
}
