using Application.Package.SetActivePackage;
using Application.User._UserPackage.DeActivePackage;
using Application.User._UserPackage.EditPackage;
using Application.User._UserPackage.SetPackage;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Facade.User.Package;
using Query.User._Package.UsersPackagesDTOs;
using Query.User.DTOs;

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPackageController : ApiController
    {
        private readonly IUserPackageFacade _facade;

        public UserPackageController(IUserPackageFacade facade)
        {
            _facade = facade;
        }

        [HttpPost("SetPackageForUser")]
        public async Task<ApiResult> SetActivePackage(SetUserPackageCommand command)
        {
            var result = CommandResult(await _facade.SetUserPackage(command));
            //var r = _facade.DeActiveUserPackage(new DeActiveUserPackageCommand()
            //{
            //    userId = command.userId
            //});
            return result;
        }
        [HttpPatch]
        public async Task<ApiResult> EditUserPackage(EditUserPackageCommand command)
        {
            return CommandResult(await _facade.EditUserPackage(command));
        }
        [HttpPatch("DeActivePackageForUser")]
        public async Task<ApiResult> DeActiveUserPackage(DeActiveUserPackageCommand command)
        {
            return CommandResult(await _facade.DeActiveUserPackage(command));
        }
        [Authorize]
        [HttpGet("GetPackageCurrentUser")]
        public async Task<ApiResult<List<UserPackageDto>?>> GetPackagesCurrentUser()
        {
            return QueryResult(await _facade.GetPackagesCurrentUser(User.GetUserIdToString()));
        }
        [Authorize]
        [HttpGet("GetPackageByUserId")]
        public async Task<ApiResult<UsersSinglePackagesDto?>> GetPackagesByUserId(string userId, long packageId)
        {
            return QueryResult(await _facade.GetByIdUsersPackages(userId, packageId));
        }
        [Authorize]
        [HttpGet("GetFilterPackageUser")]
        public async Task<ApiResult<UsersPackagesFilterResult?>> GetFilterUserPackages([FromQuery]UsersPackagesFilterParam param)
        {
            return QueryResult(await _facade.GetFilterUsersPackages(param));
        }
        [Authorize]
        [HttpGet("GetFilterPackageUserByUserId")]
        public async Task<ApiResult<UsersPackagesByUserIdFilterResult?>> GetFilterUserPackagesByUserId([FromQuery]UsersPackagesByUserIdFilterParam param)
        {
            return QueryResult(await _facade.GetFilterUsersPackagesByUserId(param));
        }
    }
}
