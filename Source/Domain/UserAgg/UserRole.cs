using Common.Domain;

namespace Domain.UserAgg;

public class UserRole : BaseEntity
{
    public UserRole(string roleId)
    {
        RoleId = roleId;
    }
    public string UserId { get; set; }
    //public string RoleId { get; set; }
    public string RoleId { get; set; }

}