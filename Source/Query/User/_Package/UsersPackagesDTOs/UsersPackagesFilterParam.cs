using AngleSharp.Dom;
using Common.Query.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.User._Package.UsersPackagesDTOs
{
    public class UsersPackagesFilterParam : BaseFilterParam
    {
        //public long? packageId { get; set; } = 0;
        //public string? packageTitle { get; set; }
        public bool ActivePackages { get; set; }
        public DateTime FilterStartTime { get; set; }
        public DateTime FilterEndTime { get; set; }
        public string? phoneNumber { get; set; }
        public SearchUserPackage search { get; set; } = SearchUserPackage.None;
        public string? userName { get; set; }
    }
    public class UsersPackagesByUserIdFilterParam : BaseFilterParam
    {
        public string? UserId { get; set; }
        public bool ActivePackages { get; set; }
        public DateTime FilterStartTime { get; set; }
        public DateTime FilterEndTime { get; set; }
        public SearchUserPackage search { get; set; } = SearchUserPackage.None;
    }
    public enum SearchUserPackage 
    {
        None,
        Latest, 
        //BestSeller
    }



    public class UsersPackagesFilterResult : BaseFilter<UsersPackagesFilterDataDto, UsersPackagesFilterParam>
    {

    }


    public class UsersPackagesByUserIdFilterResult : BaseFilter<UsersPackagesDto, UsersPackagesByUserIdFilterParam>
    {

    }
}