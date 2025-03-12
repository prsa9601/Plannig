using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EventAgg.Service
{
    public interface IEventService
    {
        //Task<string> Schedule(string id, string contentMessage, DateTime startTime, CancellationToken cancel);
        Task<string> SendEmail(string id, string contentMessage, DateTime startTime, CancellationToken cancel);

    }
}
