using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Query;
using Common.Query.Filter;

namespace Query.User.DTOs
{
    public class UserFilterData : BaseDto
    {
        public string Id { get; set; }
        public string CurrentUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public UserAvatarDto avatar { get; set; }
    }


    public class UserFilterParam : BaseFilterParam
    {
        public string UserName { get; set; } = string.Empty;
        public required string CurrentUserId { get; set; }
    }
    public class UserFilterParamViewModel : BaseFilterParam
    {
        public string UserName { get; set; } = string.Empty;
        public required string CurrentUserId { get; set; }
    }
    public class UserFilterResult : BaseFilter<UserFilterData, UserFilterParam>
    {
    }


    #region FilterForAdmin
    public class UserFilterDataForAdmin : BaseDto
    {
        public string Id { get; set; }
        //public string CurrentUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        //public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public UserAvatarDto avatar { get; set; }
        public List<UserPackageDto> userPackages { get; set; }

    }
    public class UserFilterParamForAdmin : BaseFilterParam
    {
        public bool ActivePackage { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Family { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
    public class UserFilterResultForAdmin : BaseFilter<UserFilterDataForAdmin, UserFilterParamForAdmin>
    {
    }
    #endregion

}
