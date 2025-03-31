using AngleSharp;
using Application._Utilities;
using Application.Event;
using Application.Event.Add;
using Application.Notification;
using Application.Notification.Remove;
using Application.NotificationSchedule;
using Application.Package._Service;
using Application.Role.Create;
using Application.User;
using Application.User._RequestBox;
using Application.User.SendVerificationEmailToken;
using Common.Application.Schedule;
using Domain.EventAgg.Service;
using Domain.Notification.NotificationSchedule;
using Domain.Notification.Service;
using Domain.PackageAgg.Service;
using Domain.UserAgg;
using Domain.UserAgg.Service;
using FluentValidation;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Presentation.Facade;
using Query.Event.GetById;
using StackExchange.Redis;

namespace Config
{
    public static class Bootstrapper
    {
        public static void RegisterDependency(this IServiceCollection services, string connectionString)
        {
            InfrastructureBootstrapper.Init(services, connectionString);

            services.AddMediatR(typeof(AddEventCommandHandler).Assembly);
            services.AddMediatR(typeof(CreateRoleCommandValidator).Assembly);
            services.AddMediatR(typeof(Directories).Assembly);

            services.AddMediatR(typeof(GetEventByIdQuery).Assembly);

            //services.AddTransient<IPostDomainService, PostDomainService>();
            // services.AddTransient<IUserService, UserService>();
            //services.AddTransient<ICategoryDomainService, CategoryDomainService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationScheduleService, NotificationScheduleService>();
            services.AddTransient<IPackageService, PackageService>();
            services.AddSingleton<EventScheduler>();
            services.AddSingleton<EventNotificationScheduler>();

            services.AddMemoryCache();

             services.AddLogging(); // اضافه کردن سرویس‌های لاگینگ
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<RemoveNotificationCommandHandler>>();
            services.AddSingleton(typeof(ILogger), logger);

            services.AddValidatorsFromAssembly(typeof(AddEventCommandValidator).Assembly);

            services.InitFacadeDependency();
        }
    }
}