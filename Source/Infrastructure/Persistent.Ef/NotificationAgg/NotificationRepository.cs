using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Notification;
using Domain.Notification.Repository;
using Infrastructure._Utilities;

namespace Infrastructure.Persistent.Ef.NotificationAgg
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(PlanningContext context) : base(context)
        {
            
        }
    }
}
