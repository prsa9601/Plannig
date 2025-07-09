using Common.Query;
using Domain.UserAgg;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Event.DTOs;

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
                    if (item1.UserId.Equals(user.Id))
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
            string myUserName = _context.Users.Where(i => i.Id == request.userId).Select(i=>i.UserName).FirstOrDefault()!;
            return model.MapList(_context, myUserName);
        }
    }
}