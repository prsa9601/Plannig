using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Query;
using Query.User.DTOs;

namespace Query.User._Package.UsersPackagesDTOs
{
    public class UsersPackagesDto :BaseDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public List<UserPackageDto> userPackages { get; set; }

    }
    public class UsersPackagesFilterDataDto :BaseDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public UserPackageDto userPackages { get; set; }

    }
    public class UsersSinglePackagesDto :BaseDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public UserSinglePackageDto userPackages { get; set; }

    }
}
