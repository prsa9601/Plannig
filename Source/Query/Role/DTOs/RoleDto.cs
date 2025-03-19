using Common.Query;
using Domain.RoleAgg.Enums;
using Microsoft.AspNetCore.Identity;

namespace Query.Role.DTOs
{
    public class RoleDto : IdentityRole
    {
        //public string Id { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
