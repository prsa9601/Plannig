using Common.Domain;
using Common.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Domain.RoleAgg
{
    public class Role : IdentityRole
    {
        //public string Title { get; private set; }
        public DateTime CreationDate { get; private set; }
        public List<RolePermission> Permissions { get; private set; }

        private Role()
        {
        }

        public Role(string name, List<RolePermission> permissions)
        {
            Permissions = new List<RolePermission>();
            if (name == null) throw new ArgumentNullException(nameof(name));
            CreationDate = DateTime.Now;
            Name = name;
            Permissions = permissions;
        }

        public Role(string name)
        {
            NullOrEmptyDomainDataException.CheckString(name, nameof(name));

            Name = name;
            Permissions = new List<RolePermission>();
        }

        public void Edit(string name)
        {
            NullOrEmptyDomainDataException.CheckString(name, nameof(name));
            Name = name;
        }

        //public void SetPermissions(List<RolePermission> permissions)
        //{
        //   var RolePermission = new List<RolePermission>();
        //    foreach (var item in permissions)
        //    {
        //          var permission = new RolePermission();

        //          Permissions = permissions;
        //    }

        //}
        public void SetPermissions(List<RolePermission> permissions)
        {

            Permissions = permissions;
        }
    }
}