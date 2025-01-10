using Common.Query.Filter;

namespace Planning.Api.Model.Friend
{
    public class FriendDtoForProfileParamViewModel : BaseFilterParam
    {
        public string UserName { get; set; } = string.Empty;
    }
}
