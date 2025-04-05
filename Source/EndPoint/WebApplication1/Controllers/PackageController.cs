using Application.Package.Add;
using Application.Package.Delete;
using Application.Package.Edit;
using Application.Package.RemoveActivePackage;
using Application.Package.SetActivePackage;
using Application.User._RequestBox.Add;
using Application.User._RequestBox.Remove;
using Common.AspNetCore;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planning.Api.Model;
using Presentation.Facade.Package;
using Presentation.Facade.User.Request;
using Query.Package.DTOs;
using Query.User._RequestBox.DTOs;

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ApiController
    {
        private readonly IPackageFacade _facade;

        public PackageController(IPackageFacade facade)
        {
            _facade = facade;
        }
        [Authorize]
        [HttpPost]
        public async Task<ApiResult> Add([FromForm] CreatePackageViewModel command)
        {
            //if (TimeSpan.TryParse(command.ExpiryTime, out TimeSpan expiryTime))

            var result = await _facade.Add(new AddPackageCommand()
            {
                Title = command.Title,
                Link = command.Link,
                AllowedSmsCount = command.AllowedSmsCount,
                AllowedEmailCount = command.AllowedEmailCount,
                ExpiryTime = command.ExpiryTime,
                Picture = command.Picture,
                Price = command.Price,
                Specifications = command.GetSpecification(),
            });
            return CommandResult(result);

        }
        [Authorize]
        [HttpPost("SetActivePackage")]
        public async Task<ApiResult> SetActivePackage(SetActivePackageCommand command)
        {
            var result = await _facade.SetActivePackage(new SetActivePackageCommand()
            {
                Id = command.Id
            });
            return CommandResult(result);
        }
        [Authorize]
        [HttpPatch("RemoveActivePackage")]
        public async Task<ApiResult> RemoveActivePackage(RemoveActivePackageCommand command)
        {
            var result = await _facade.RemoveActivePackage(new RemoveActivePackageCommand()
            {
                Id = command.Id
            });
            return CommandResult(result);
        }
        [Authorize]
        [HttpPatch]
        public async Task<ApiResult> Edit([FromForm] EditPackageViewModel command)
        {
            var result = await _facade.Edit(new EditPackageCommand()
            {
                Id = command.packageId,
                AllowedEmailCount = command.AllowedEmailCount,
                AllowedSmsCount = command.AllowedSmsCount,
                ExpiryTime = command.ExpiryTime,
                Title = command.Title,
                Link = command.Link,
                Picture = command.Picture,
                Price = command.Price,
                Specifications = command.GetSpecification(),
            });
            return CommandResult(result);
        }
        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<ApiResult> Remove(long Id)
        {
            var result = await _facade.Delete(new RemovePackageCommand(Id));
            return CommandResult(result);
        }
        [HttpGet("GetById")]
        [Authorize]
        public async Task<ApiResult<PackageDto?>> GetPackage([FromQuery] long id)
        {
            var result = await _facade.GetPackage(id);
            return QueryResult(result);
        }
        [Authorize]
        [HttpGet("GetList")]
        public async Task<ApiResult<List<PackageDto?>>> GetPackages()
        {
            var result = await _facade.GetListPackages();
            return QueryResult(result);
        }
        [Authorize]
        [HttpGet("GetListActiveForCurrentUser")]
        public async Task<ApiResult<List<PackageDtoForUserProfile?>>> GetListActiveForCurrentUser()
        {
            var result = await _facade.
                GetListPackagesActiveByUserId(User.GetUserIdToString());
            return QueryResult(result);
        }
        
        [Authorize]
        [HttpGet("GetPackagesByUserId/{UserId}")]
        public async Task<ApiResult<List<PackageDto>?>> GetPackagesByUserId(string UserId)
        {
            var result = await _facade.
                GetPackagesByUserId(UserId);
            return QueryResult(result);
        }

    }
}
