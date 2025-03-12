using Query.Event.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Event
{
    public static class EventMapper 
    {
        public static List<EventDto>? MapList(this List<Domain.EventAgg.Event>? Event)
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
                    NotificationEnum = item.notification,
                    StartTime = item.StartTime,
                    tag = item.tag,
                    Title = item.Title,
                };
                model.Add(dto);
            }
            return model;
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
                    NotificationEnum = item.notification,
                    StartTime = item.StartTime,
                    tag = item.tag,
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
                    tag = item.tag,
                    Title = item.Title,
                };

                model.Add(dto);
            }
            return model;
        }
    }
}
