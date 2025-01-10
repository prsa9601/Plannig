using Common.Query;
using Newtonsoft.Json;
using Query.User.DTOs;

namespace Query.User.UserTokens.GetByRefreshToken;

public record GetUserTokenByRefreshTokenQuery(string HashRefreshToken) : IQuery<UserTokenDto?>;