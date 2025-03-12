using Common.AspNetCore;
using Domain.RoleAgg.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Presentation.Facade.Package;
using Presentation.Facade.Role;
using Presentation.Facade.User;

namespace Planning.Api.Infrastructure.Security
{
    public class PackageCkecker(int sendCount) : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private IUserFacade _userFacade = null!;
        private IPackageFacade _packageFacade = null!;
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {

            _userFacade = context.HttpContext.RequestServices.GetRequiredService<IUserFacade>();
            _packageFacade = context.HttpContext.RequestServices.GetRequiredService<IPackageFacade>();
            if (context.HttpContext.User.Identity != null && context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (await UserHasPackage(context) == false)
                {
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                context.Result = new UnauthorizedObjectResult("Unauthorize");
            }
        }

        private bool HasAllowAnonymous(AuthorizationFilterContext context)
        {
            //comment
            if (_userFacade == null)
            {
                return false;
                throw new Exception("مشکل سمت سرور به وجود آمده");
            }
            var metaData = context.ActionDescriptor.EndpointMetadata.OfType<dynamic>().ToList();
            bool hasAllowAnonymous = false;
            foreach (var f in metaData)
            {
                try
                {
                    hasAllowAnonymous = f.TypeId.Name == "AllowAnonymousAttribute";
                    if (hasAllowAnonymous)
                        break;
                }
                catch
                {
                    // ignored
                }
            }

            return hasAllowAnonymous;
        }
        private async Task<bool> UserHasPackage(AuthorizationFilterContext context)
        {
            var user = await _userFacade.GetUserById(context.HttpContext.User.GetUserIdToString());
            if (user == null)
                return false;

            var allowedEmail = user.userPackageDto.Select(s => s.AllowedEmailCount).FirstOrDefault();
            //var roles = await _packageFacade.GetRoles();
            if (allowedEmail >= sendCount)
            {
                return true;
            }
            return false;
        }
    }
}