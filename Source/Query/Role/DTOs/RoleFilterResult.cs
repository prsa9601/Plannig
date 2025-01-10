using Common.Query.Filter;
using Microsoft.AspNetCore.Identity;

namespace Query.Role.DTOs
{
    public class RoleFilterResult : BaseFilter<RoleFilterData, RoleFilterParam>
    {
    }
    public class BaseFilter<TData, TParam> : BaseFilter
        where TParam : BaseFilterParam
        where TData : IdentityRole
    {
        public List<TData> Data { get; set; } = new List<TData>();
        public TParam FilterParams { get; set; } = null!;
    }
}
