using Application._Utilities;
using Application.Event.Add;
using Application.Package._Service;
using Application.User;
using Application.User._RequestBox;
using Domain.PackageAgg.Service;
using Domain.UserAgg;
using Domain.UserAgg.Service;
using FluentValidation;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Facade;
using Query.Event.GetById;

namespace Config
{
    public static class Bootstrapper
    {
        public static void RegisterDependency(this IServiceCollection services, string connectionString)
        {
            InfrastructureBootstrapper.Init(services, connectionString);

            services.AddMediatR(typeof(AddEventCommandHandler).Assembly);
            services.AddMediatR(typeof(Directories).Assembly);

            services.AddMediatR(typeof(GetEventByIdQuery).Assembly);

            //services.AddTransient<IPostDomainService, PostDomainService>();
            // services.AddTransient<IUserService, UserService>();
            //services.AddTransient<ICategoryDomainService, CategoryDomainService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPackageService, PackageService>();

            services.AddMemoryCache();

            services.AddValidatorsFromAssembly(typeof(AddEventCommandValidator).Assembly);

            services.InitFacadeDependency();
        }
    }
}