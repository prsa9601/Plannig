using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.Event.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Event.GetById
{
    public record class GetEventByIdQuery(long Id) : IQuery<EventDto>;
    

    public class GetEventByIdQueryHandler : IQueryHandler<GetEventByIdQuery, EventDto>
    {
        private readonly PlanningContext _context;

        public GetEventByIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<EventDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _context.Events.FirstOrDefaultAsync(i => i.Id == request.Id);
            return model.Map();
        } 
    }
}
