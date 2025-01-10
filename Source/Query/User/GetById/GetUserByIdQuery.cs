using Common.Query;
using Query.User.DTOs;

namespace Query.User.GetById;

public class GetUserByIdQuery : IQuery<UserDto?>
{
    public GetUserByIdQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; private set; }
}