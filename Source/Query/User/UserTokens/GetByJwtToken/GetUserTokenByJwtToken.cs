using Common.Query;
using Query.User.DTOs;

namespace Query.User.UserTokens.GetByJwtToken;

public record GetUserTokenByJwtTokenQuery(string HashJwtToken) : IQuery<UserTokenDto?>;