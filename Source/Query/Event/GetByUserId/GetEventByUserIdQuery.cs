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
            var user = _context.Users.Include("userEvents").Where(i => i.Id.ToString() == request.userId).FirstOrDefault();
            var events = _context.Events.Select(i => i.EventUser).ToList();
            List<long> EventIds = new List<long>();
            foreach (var item in events)
            {
                foreach (var item1 in item)
                {
                    if (item1.UserName.Equals(user.UserName))
                    {
                        EventIds.Add(item1.EventId);
                    }
                }
            }

            
            var model = new List<Domain.EventAgg.Event>();
            foreach (var ids in EventIds)
            {
                model.Add(_context.Events.Where(i=>i.Id.Equals(ids)).FirstOrDefault());
            }
            return model.MapList();
        }
    }
}