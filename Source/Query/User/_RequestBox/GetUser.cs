
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;

namespace Query.User._RequestBox
{
    public static class GetUser
    {
        public static string GetUserNameByIdUser(this string id, PlanningContext context)
        {
            var result = context.Users.Where(i => i.Id.Equals(id)).Select(i => i.UserName).FirstOrDefault();
            return result;
        }
    }
}