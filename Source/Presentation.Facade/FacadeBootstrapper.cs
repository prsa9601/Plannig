using Domain.PackageAgg.Repository;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Facade.Blog;
using Presentation.Facade.Category;
using Presentation.Facade.Comment;
using Presentation.Facade.Event;
using Presentation.Facade.Instagram;
using Presentation.Facade.Notification;
using Presentation.Facade.Package;
using Presentation.Facade.Role;
using Presentation.Facade.Telegram;
using Presentation.Facade.Telegram.Account;
using Presentation.Facade.User;
using Presentation.Facade.User.Friend;
using Presentation.Facade.User.Package;
using Presentation.Facade.User.Request;

namespace Presentation.Facade
{
    public static class FacadeBootstrapper
    {
        public static void InitFacadeDependency(this IServiceCollection services)
        {

            services.AddScoped<IEventFacade, EventFacade>();

            services.AddScoped<IAccountTelegramFacade, AccountTelegramFacade>();
            services.AddScoped<IUserFacade, UserFacade>();
            services.AddScoped<ITelegramFacade, TelegramFacade>();
            services.AddScoped<IPackageFacade, PackageFacade>();
            services.AddScoped<IRoleFacade, RoleFacade>();
            services.AddScoped<IBlogFacade, BlogFacade>();
            services.AddScoped<ICommentFacade, CommentFacade>();
            services.AddScoped<ICategoryFacade, CategoryFacade>();
            services.AddScoped<IInstagramFacade, InstagramFacade>();
            services.AddScoped<IRequestFacade, RequestFacade>();
            services.AddScoped<IFriendFacade, FriendFacade>();
            services.AddScoped<INotificationFacade, NotificationFacade>();
            services.AddScoped<IUserPackageFacade, UserPackageFacade>();


        }
    }
}