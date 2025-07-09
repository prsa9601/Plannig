using Domain.UserAgg;
using Infrastructure.Persistent.Ef;
using Query.Event.DTOs;

namespace Query.Event
{
    public static class EventMapper
    {
        public static List<EventDto>? MapList(this List<Domain.EventAgg.Event>? Event,
            PlanningContext context, string myUserName)
        {
            var model = new List<EventDto>();
            foreach (var item in Event)
            {
                var dto = new EventDto()
                {
                    AccessNotification = item.AccessNotification,
                    CreationDate = item.CreationDate,
                    Description = item.Description,
                    EventAddress = item.EventAddress,
                    EndTime = item.EndTime,
                    Id = item.Id,
                    Link = item.Link,
                    notification = item.notification,
                    StartTime = item.StartTime,
                    tag = item.Tag,
                    Title = item.Title,
                    UserNames = item.EventUser.Select(i => i.UserId).ToList().GetUserNames(context, myUserName)
                };
                model.Add(dto);
            }
            return model;
        }
        public static List<string?> GetUserNames(this List<string?> UserIds, PlanningContext context,
            string MyUserName)
        {
            var UserNames = new List<Domain.UserAgg.User?>();
            var myUser = context.Users.FirstOrDefault(i => i.Id == MyUserName);
            foreach (var item in UserIds)
            {
                UserNames.Add(context.Users.Where(i => i.Id == item).FirstOrDefault());

            }
            var result = UserNames.Remove(myUser);
            return UserNames.Select(i => i.UserName).ToList();
        }
        public static EventDto? Map(this Domain.EventAgg.Event? item)
        {

            var model = new EventDto()
            {
                AccessNotification = item.AccessNotification,
                CreationDate = item.CreationDate,
                Description = item.Description,
                EventAddress = item.EventAddress,
                EndTime = item.EndTime,
                Id = item.Id,
                Link = item.Link,
                notification = item.notification,
                StartTime = item.StartTime,
                tag = item.Tag,
                Title = item.Title,
            };


            return model;
        }
        public static List<EventForShopDto> MapForShop(this List<Domain.EventAgg.Event> Event)
        {
            var model = new List<EventForShopDto>();
            foreach (var item in Event)
            {
                var dto = new EventForShopDto()
                {
                    CreationDate = item.CreationDate,
                    EndTime = item.EndTime,
                    Id = item.Id,
                    StartTime = item.StartTime,
                    tag = item.Tag,
                    Title = item.Title,
                };

                model.Add(dto);
            }
            return model;
        }
    }
}
