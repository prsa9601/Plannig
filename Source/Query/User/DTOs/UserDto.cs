using Common.Query;
using Domain.UserAgg;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Query.User.DTOs
{
    public class UserDto :BaseDto
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Family { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public UserAvatarDto avatar { get; set; }
        public List<UserRoleDto> Roles { get; set; } = new List<UserRoleDto>();
        public List<FriendsDto> friends { get; set; }
        public List<UserPackageDto> userPackageDto { get; set; }

    }
    public class FriendsDto :BaseDto
    {
        public string CurrentUserId { get; set; }
        public string UserFriend { get; set; }

        public UserAvatarDto avatar { get; set; }

    }
    public class UserRoleDto : IdentityRole
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }

    public class UserAvatarDto : BaseDto
    {
        public string UserId { get; set; }
        public Domain.UserAgg.UserAvatar.Avatar Avatar { get; set; }
    }
    public class UserPackageDto : BaseDto
    {
        //public TimeSpan ExpireDate { get; set; }
        public long PackageId { get; set; }
        public string UserId { get; set; }
        public int AllowedEmailCount { get; set; } = 10;
        public int AllowedSmsCount { get; set; } = 0;
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class UserSinglePackageDto : BaseDto
    {
        //public string UserName { get; set; }
        //public string PhoneNumber { get; set; }
        //public string Email { get; set; }
        public long PackageId { get; set; }
        public string UserId { get; set; }
        public int AllowedEmailCount { get; set; } = 10;
        public int AllowedSmsCount { get; set; } = 0;
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }
}
