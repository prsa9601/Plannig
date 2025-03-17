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
        public long? packageId { get; set; }
        public string? packageTitle { get; set; }
        public bool ActivePackages { get; set; }
        public string? phoneNumber { get; set; }
        public SearchUserPackage search { get; set; } = SearchUserPackage.None;
        public string? userName { get; set; }
    }
    public enum SearchUserPackage 
    {
        None,
        Latest, 
        //BestSeller
    }



    public class UsersPackagesFilterResult : BaseFilter<UsersPackagesDto, UsersPackagesFilterParam>
    {

    }
}