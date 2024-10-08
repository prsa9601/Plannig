using Common.Query;
using Domain.UserAgg;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Event.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Event.GetByUserId
{
    public record class GetEventByUserIdQuery(string userId) : IQuery<List<EventDto?>>;
 

    public class GetEventByUserIdQueryHandler : IQueryHandler<GetEventByUserIdQuery, List<EventDto?>>
    {
        private readonly PlanningContext _context;

        public GetEventByUserIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<List<EventDto?>> Handle(GetEventByUserIdQuery request, CancellationToken cancellationToken)
        {
            var model = _context.Users.Include("userEvents").Where(i => i.Id.ToString() == request.userId).FirstOrDefault();
            //var eventId = model.userEvents.Where(i => i.UserId.ToString() == request.userId).Select(i => i.EventId).ToList();
            var eventId = model.userEvents.Select(i => i.EventId).ToList();
            // List<Domain.EventAgg.Event> model1 = new List<Domain.EventAgg.Event>;
            var model1 = new List<EventDto>();
            foreach (var item in eventId)
            {
                var mo =  _context.Events.Where(i => i.Id == item).ToList().MapList();
                foreach(var item1 in mo)
                {

                    model1.Add(item1);

                }
            }
            return model1;
        }
    }
}
