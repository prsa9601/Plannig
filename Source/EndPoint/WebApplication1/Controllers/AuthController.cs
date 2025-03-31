using Application.User.Login;
using Application.User.Register;
using Common.Application;
using Domain.UserAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Presentation.Facade.User;
using Query.User.DTOs;
using Application.User.AddToken;
using Application.User.Logout;
using Common.AspNetCore;
using Common.Application.SecurityUtil;
using Microsoft.AspNetCore.Authorization;
using Planning.Api.Infrastructure.JwtUtil;
using Planning.Api.Model.Auth;
using UAParser;
using Telegram.Bot.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using User = Domain.UserAgg.User;
using Application.User.RemoveToken;
using Microsoft.AspNetCore.Authentication;
using Application.User.SendVerificationEmailToken;
using Application.User.VerificationEmail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiController
    {
        private readonly IUserFacade _facade;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<Domain.UserAgg.User> _userManager;
        private readonly SignInManager<Domain.UserAgg.User> _signInManager;


        public AuthController(IUserFacade facade, IConfiguration configuration, IMemoryCache memoryCache, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _facade = facade;
            _configuration = configuration;
            _memoryCache = memoryCache;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // POST api/<AuthController>
        [HttpPost("register")]
        public async Task<ApiResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _facade.RegisterUser(command);
            return CommandResult(result);
        }
        [Authorize]
        [HttpDelete("logout")]
        public async Task<ApiResult> Logout()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            //var token = await HttpContext.GetTokenAsync("Authorization");
            var result = await _facade.GetUserTokenByJwtToken(token!);
            if (result == null)
                return CommandResult(OperationResult.NotFound());

            await _facade.RemoveToken(new RemoveUserTokenCommand(result.UserId, result.Id));
            return CommandResult(OperationResult.Success());
        }

        // PUT api/<AuthController>/5
        //[HttpPost("Login")]
        //public async Task<ApiResult<LoginResultDto?>> Login([FromBody] UserLoginCommand command)
        //{
        //    var user = await _userManager.Users.FirstOrDefaultAsync(s => s.UserName == command.UserName);
        //    if (user == null)
        //    {

        //        var result = OperationResult<LoginResultDto>.NotFound();
        //        return CommandResult(result);
        //    }

        //    //if (user == null)
        //    //{
        //    //    _logger.LogError("User not found: {UserName}", request.UserName);
        //    //    return OperationResult.NotFound();
        //    //}
        //    //var user = await _repository.GetTrackingByUserName(request.UserName);
        //    if (!Sha256Hasher.IsCompare(user.Password, command.Password))
        //    {
        //        var result = OperationResult<LoginResultDto>.Error("پسورد شما اشتباه است!");
        //        return CommandResult(result);
        //    }

        //    //var claim = new Claim(ClaimTypes.NameIdentifier, user.Id);
        //    //var claim = new Claim(ClaimTypes.NameIdentifier, user.Id);
        //    //var r =  await  _userManager.AddClaimAsync(user,claim);
        //    var claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.Id),
        //        new Claim(ClaimTypes.Name, user.UserName!)
        //    };

        //    await _signInManager.SignInAsync(user, command.rememberMe);

        //    //var user = await _facade.GetUserByUserNameNumber(instagramCommand.UserName);
        //    //BuildToken(user, _configuration);
        //    //_memoryCache.Set("UsernameCacheKey", instagramCommand.UserName, TimeSpan.FromDays(3));
        //    //var result = await _facade.LoginUser(command);
        //    var loginResult = await AddTokenAndGenerateJwt(user);

        //    return CommandResult(loginResult);
        //}
        [HttpPost("Login")]
        public async Task<ApiResult<LoginResultDto?>> Login([FromBody] UserLoginCommand command)
        {
            var user = await _facade.GetUserByUserName(command.UserName);
            if (user == null)
            {
                var result = OperationResult<LoginResultDto>.Error("کاربری با مشخصات وارد شده یافت نشد");
                return CommandResult(result);
            }

            if (Sha256Hasher.IsCompare(user.Password, command.Password) == false)
            {
                var result = OperationResult<LoginResultDto>.Error("کاربری با مشخصات وارد شده یافت نشد");
                return CommandResult(result);
            }



            var loginResult = await AddTokenAndGenerateJwt(user);
            return CommandResult(loginResult);
        }

        // DELETE api/<AuthController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        //public static void BuildToken(UserDto user, IConfiguration configuration)
        //{
        //    var claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
        //        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
        //        //new Claim(ClaimTypes.Role,string.Join("-",roles))
        //    };
        //    //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"]));
        //    //var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        //    //var token = new JwtSecurityToken(
        //    //    issuer: configuration["JwtConfig:Issuer"],
        //    //    audience: configuration["JwtConfig:Audience"],
        //    //    claims: claims,
        //    //    expires: DateTime.Now.AddDays(7),
        //    //    signingCredentials: credential);

        //    //return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        //private async Task<OperationResult<LoginResultDto?>> AddTokenAndGenerateJwt(UserDto user)
        private async Task<OperationResult<LoginResultDto?>> AddTokenAndGenerateJwt(UserDto user)
        {
            var uaParser = Parser.GetDefault();
            var header = HttpContext.Request.Headers["user-agent"].ToString();
            var device = "windows";
            if (header != null)
            {
                var info = uaParser.Parse(header);
                device = $"{info.Device.Family}/{info.OS.Family} {info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";
            }

            var token = JwtTokenBuilder.BuildToken(user, _configuration);
            var refreshToken = Guid.NewGuid().ToString();

            var hashJwt = Sha256Hasher.Hash(token);
            var hashRefreshToken = Sha256Hasher.Hash(refreshToken);

            var tokenResult = await _facade.AddToken(new AddUserTokenCommand(user.Id, hashJwt, hashRefreshToken, DateTime.Now.AddDays(7), DateTime.Now.AddDays(8), device));
            if (tokenResult.Status != OperationResultStatus.Success)
                return OperationResult<LoginResultDto?>.Error();

            return OperationResult<LoginResultDto?>.Success(new LoginResultDto()
            {
                Token = token,
                //RefreshToken = refreshToken
            });
        }
        #region VerificationEmail

        [Authorize]
        [HttpGet("SendVerificationEmailToken")]
        public async Task<ApiResult> SendVerificationEmailToken()
        {
            var result = await _facade.SendVerificationEmailToken(new SendVerificationEmailCodeCommand
            {
                UserId = User.GetUserIdToString()
            });
            return CommandResult(result)!;
        }

        [Authorize]
        [HttpPost("VerificationEmail")]
        public async Task<ApiResult> VerificationEmail(VerificationEmailViewModel command)
        {
            var result = await _facade.VerificationEmail(new VerificationEmailCommand
            {
                UserId = User.GetUserIdToString(),
                VerificationEmailToken = command.token
            });
            return CommandResult(result);
        }
        #endregion
    }
}
