using Domain.NotificationAgg.Repository;
using Infrastructure._Utilities;

namespace Infrastructure.Persistent.Ef.NotificationAgg
{
    public class NotificationRepository : BaseRepository<Domain.NotificationAgg.Notification>, INotificationRepository
    {
        public NotificationRepository(PlanningContext context) : base(context)
        {
            
        }
    }
}
