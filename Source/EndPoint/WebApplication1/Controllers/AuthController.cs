using Application.User.Login;
using Application.User.Register;
using Common.Application;
using Domain.UserAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Presentation.Facade.User;
using Query.User.DTOs;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.User.Logout;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Planning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserFacade _facade;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;


        public AuthController(IUserFacade facade, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _facade = facade;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        // POST api/<AuthController>
        [HttpPost("register")]
        public async Task<OperationResult> Register([FromBody] RegisterUserCommand command)
        {
            return await _facade.RegisterUser(command);
        }
        [HttpDelete("Logout")]
        public async Task<OperationResult> Logout()
        {
            return await _facade.LogoutUser(new LogoutUserCommand());
        }

        // PUT api/<AuthController>/5
        [HttpPost]
        public async Task<OperationResult> Login([FromBody] UserLoginCommand command)
        {
            //var user = await _facade.GetUserByUserNameNumber(instagramCommand.UserName);
            //BuildToken(user, _configuration);
            //_memoryCache.Set("UsernameCacheKey", instagramCommand.UserName, TimeSpan.FromDays(3));
            return await _facade.LoginUser(command);
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
    }
}
