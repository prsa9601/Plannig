using Common.Query.Filter;
using Query.User._RequestBox.DTOs;

namespace Planning.Api.Model.RequestBox
{
    public class RequestBoxParamViewModel : BaseFilterParam
    {
        public filter filter { get; set; }
    }
}
