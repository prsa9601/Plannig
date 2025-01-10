using Query.Role.DTOs;

namespace Query.Role
{
    public static class RoleMapper
    {
        public static RoleFilterData MapListData(this Domain.RoleAgg.Role role)
        {
            return new RoleFilterData()
            {
                Name = role.Name,
                Permissions = role.Permissions.Select(s => s.Permission).ToList(),
                Id = role.Id,
                CreationDate = role.CreationDate
            };
        }
    }
}
