using Domain.EventAgg.Repository;
using Domain.PackageAgg.Repository;
using Domain.UserAgg;
using Domain.UserAgg.Repository;
using Infrastructure.Persistent.Ef;
using Infrastructure.Persistent.Ef.EventAgg;
using Infrastructure.Persistent.Ef.UserAgg;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Domain.SocialMediaAgg.InstagramAgg.Repository;
using Infrastructure.Persistent.Ef.InstagramAgg;
using Infrastructure.Persistent.Ef.TelegramAgg;
using Domain.SocialMediaAgg.InstagramAgg;
using Domain.SocialMediaAgg.TelegramAgg.Repository;
using Infrastructure.Persistent.Ef.PackageAgg;


namespace Infrastructure
{
    public class InfrastructureBootstrapper
    {
        public static void Init(IServiceCollection services, string connectionString)
        {
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IUserRepository<Domain.UserAgg.User>, UserRepository<Domain.UserAgg.User>>();
            services.AddScoped<IInstagramRepository, InstagramRepository>();
            services.AddScoped<ITelegramRepository, TelegramRepository>();
            //services.AddSingleton<IPostRepository, PostRepository>();

            //services.AddSingleton<ICustomPublisher, CustomPublisher>();

            services.AddDefaultIdentity<Domain.UserAgg.User>(options =>
            {

                // User Options
                // options.User.RequireUniqueEmail = true;
                // options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+";
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+";
                // Signin Options
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = true;
                //// Password Options
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                //// LockOut
                //options.Lockout.AllowedForNewUsers = true;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                //options.Lockout.MaxFailedAccessAttempts = 3;
                // Stores Options
                //options.Stores.MaxLengthForKeys = 10;
                //options.Stores.ProtectPersonalData = false;
                options.Lockout.AllowedForNewUsers = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Stores.ProtectPersonalData = false;
                //options.Tokens.AuthenticatorTokenProvider = "";

                //options.ClaimsIdentity.UserNameClaimType = "ClaimTypes.Name";
                //options.ClaimsIdentity.UserIdClaimType = "ClaimTypes.NameIdentifier";
                //options.ClaimsIdentity.EmailClaimType = "ClaimTypes.Email";
                //options.ClaimsIdentity.UserNameClaimType = "ClaimTypes.MobilePhone";
            })
             .AddEntityFrameworkStores<PlanningContext>();
            //.AddDefaultTokenProviders();
            //.AddErrorDescriber<PersianIdentityErrors>();

           // services.AddTransient(_ => new DapperContext(connectionString));
            services.AddDbContext<PlanningContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
        }
    }
}