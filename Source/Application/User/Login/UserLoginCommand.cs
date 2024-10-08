using System.Security.Claims;
using Common.Application;
using Microsoft.AspNetCore.Identity;
using Common.Application.SecurityUtil;
using Domain.UserAgg.Repository;
using Domain.UserAgg.Service;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Login
{
    public class UserLoginCommand : IBaseCommand
    {
       // public string Name { get; set; }
        public string UserName { get; set; }
       // public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool rememberMe { get; set; }
        //public string Email { get; set; }
    }
    public class UserLoginCommandHandler : IBaseCommandHandler<UserLoginCommand>
    {
        private readonly IUserService _userService;
        private readonly IUserRepository<Domain.UserAgg.User> _repository;
        private readonly UserManager<Domain.UserAgg.User> _userManager;
        private readonly SignInManager<Domain.UserAgg.User> _signInManager;

        public UserLoginCommandHandler(UserManager<Domain.UserAgg.User> userManager, SignInManager<Domain.UserAgg.User> signInManager, IUserService userService, IUserRepository<Domain.UserAgg.User> repository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _repository = repository;
        }

        public async Task<OperationResult> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
                //var claimsIdentity = new ClaimsIdentity(new List<Claim>
                //{
                //    // اضافه کردن claim برای شناسه کاربر
                //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //    // اضافه کردن سایر claims مورد نیاز
                //}, "YourAuthenticationType");

                //// ایجاد یک ClaimsPrincipal با استفاده از ClaimsIdentity
                //var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                //// استفاده از ClaimsPrincipal برای لاگین کردن کاربر
                //await HttpContext.SignInAsync(claimsPrincipal);

                // || s.Email==request.Email
            var user = await _userManager.Users.FirstOrDefaultAsync(s => s.UserName == request.UserName);
            if (user == null)
                return OperationResult.NotFound();
            //if (user == null)
            //{
            //    _logger.LogError("User not found: {UserName}", request.UserName);
            //    return OperationResult.NotFound();
            //}
            //var user = await _repository.GetTrackingByUserName(request.UserName);
            if (!Sha256Hasher.IsCompare(user.Password, request.Password))
            {
                return OperationResult.Error("پسورد شما اشتباه است!");
            }

                //var claim = new Claim(ClaimTypes.NameIdentifier, user.Id);
                //var claim = new Claim(ClaimTypes.NameIdentifier, user.Id);
                //var r =  await  _userManager.AddClaimAsync(user,claim);
                new Claim(ClaimTypes.NameIdentifier , user.Id);

             await _signInManager.SignInAsync(user, request.rememberMe);
            

            
             return OperationResult.Success();
            
        }
    }
}
