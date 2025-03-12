using Domain.EventAgg;
using Domain.EventAgg.Enum;
using Domain.EventAgg.Repository;
using Infrastructure._Utilities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistent.Ef.EventAgg
{
    public class EventRepository :  BaseRepository<Event>, IEventRepository
    {
        public EventRepository(PlanningContext context) : base(context)
        {
        }

        public async Task<Event?> FindEvent(string title, DateTime startTime, DateTime endTime, string description, string link, string eventAddress, Tagged tag, NotificationEnum notificationEnum, bool accessNotification)
        {
            return await Context.Set<Event>().FirstOrDefaultAsync(i => i.Title == title && i.EndTime.Equals(endTime)
            && i.StartTime.Equals(startTime)
            && i.Description.Equals(description)
            && i.Link.Equals(link) && i.EventAddress.Equals(eventAddress)
            && i.Tag.Equals(tag)
            && i.notification.Equals(notificationEnum) && i.AccessNotification == accessNotification);
        }
    }
}
