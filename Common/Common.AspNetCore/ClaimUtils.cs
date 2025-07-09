using System.Security.Claims;
using System.Security.Principal;

namespace Common.AspNetCore;

public static class ClaimUtils
{
    public static long GetUserId(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return Convert.ToInt64(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
    public static string GetUserIdToString(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        //return Convert.ToString(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return Convert.ToString(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value)!;
    }
    public static string GetUserName(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return Convert.ToString(principal.FindFirst(ClaimTypes.Name)?.Value!);
    }
    public static string GetPhoneNumber(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return principal.FindFirst(ClaimTypes.MobilePhone)?.Value;
    }
    public static string GetEmail(this IIdentity identity)
    {
        ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst("Id");
        return claim?.Value ?? string.Empty;
    }
}