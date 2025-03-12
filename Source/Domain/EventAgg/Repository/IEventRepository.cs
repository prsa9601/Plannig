using System.Linq.Expressions;
using Common.Domain.Repository;
using Domain.EventAgg.Enum;

namespace Domain.EventAgg.Repository
{
    public interface IEventRepository : IBaseRepository<Event>
    {
         Task<Event?> FindEvent(string title, DateTime startTime, DateTime endTime, string description, string link,
            string eventAddress, Tagged tag, NotificationEnum notification, bool accessNotification);

    }
}
