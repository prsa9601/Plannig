using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Notification.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Notification
{
    internal static class NotificationMapper
    {
        internal static async Task<NotificationDto?> Map(this Domain.Notification.Notification? notification, PlanningContext context)
        {
            return new NotificationDto
            {
                Id = notification!.Id,
                CreationDate = notification.CreationDate,
                EventEndTime = notification.EventEndTime,
                //EventId=notification.EventId,
                EventStartTime = notification.EventStartTime,
                IsActive = notification.IsActive,
                IsSend = notification.IsSend,
                NotificationType = notification.NotificationType,
                SendTime = notification.SendTime,
                UserNames = notification.UserIds,
                eventDto = await EventMap(notification.EventId, context)!
            };
        }
        internal static NotificationFilterData? MapFilter(this
            Domain.Notification.Notification? notification, PlanningContext context)
        {
            return new NotificationFilterData
            {
                Id = notification!.Id,
                CreationDate = notification.CreationDate,
                EventEndTime = notification.EventEndTime,
                //EventId=notification.EventId,
                EventStartTime = notification.EventStartTime,
                IsActive = notification.IsActive,
                IsSend = notification.IsSend,
                NotificationType = notification.NotificationType,
                SendTime = notification.SendTime,
                UserNames = context.
                Users.Where(i => i.Id.Equals(notification.UserIds)).
                Select(i => i.UserName).ToList()!,
                IsSeen = notification.IsSeen,
                eventDto = EventMapForFilter(notification.EventId, context)!

            };
        }
        internal static EventNotificationDto? EventMapForFilter(long? EventId, PlanningContext context)
        {
            if (EventId == null)
                return null;
            var Event = context.Events.
                Where(i => i.Id.Equals(EventId)).FirstOrDefault();
            if (Event == null)
                return null;
            else
            {

                return new EventNotificationDto
                {
                    Id = Event!.Id,
                    AccessNotification = Event.AccessNotification,
                    CreationDate = Event.CreationDate,
                    notification = Event.notification,
                    Title = Event.Title,
                    Description = Event.Description,
                };
            }
        }
        internal static async Task<EventNotificationDto?> EventMap(long? EventId, PlanningContext context)
        {
            if (EventId == null)
                return null;
            var Event = await context.Events.
                Where(i => i.Id.Equals(EventId)).FirstOrDefaultAsync();
            if (Event == null)
                return null;
            else
            {

                return new EventNotificationDto
                {
                    Id = Event!.Id,
                    AccessNotification = Event.AccessNotification,
                    CreationDate = Event.CreationDate,
                    notification = Event.notification,
                    Title = Event.Title,
                    Description = Event.Description,
                };
            }
        }
    }
}
