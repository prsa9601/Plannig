using Common.Query;
using Domain.UserAgg;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User._RequestBox.DTOs;

namespace Query.User._RequestBox.GetList
{
    public class GetRequestListQuery : IQuery<List<RequestDto>?>
    {
        public GetRequestListQuery(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; set; }
    }
    internal class GetRequestListQueryHandler : IQueryHandler<GetRequestListQuery, List<RequestDto>?>
    {
        private readonly PlanningContext _context;

        public GetRequestListQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<List<RequestDto>?> Handle(GetRequestListQuery request, CancellationToken cancellationToken)
        {
            var requests = await _context.Users.Where(i=>i.UserName == request.UserName).Include("RequestBox").Select(i => i.RequestBox).ToListAsync();
            List<RequestDto> requestBox = new List<RequestDto>();
            foreach (var item in requests)
            {
                foreach (var item1 in item)
                {
                    var model = new RequestDto
                    {
                        Description = item1.Description,
                        SenderId = item1.SenderId,
                        CreationDate = item1.CreationDate,
                        Id = item1.Id,
                        RecipientId = item1.ReceiverId,
                        Title = item1.Title,
                    };
                    requestBox.Add(model);
                }
            }
            return requestBox;
        }
    }
}
