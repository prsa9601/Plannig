using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Query.User._RequestBox.DTOs;

namespace Query.User._RequestBox.GetId
{
    public record class GetRequestByIdQuery(long id, string userName) : IQuery<RequestDto?>;

    public class GetRequestByIdQueryHandler : IQueryHandler<GetRequestByIdQuery, RequestDto?>
    {
        private readonly PlanningContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<Domain.UserAgg.User> _userManager;

        public GetRequestByIdQueryHandler(PlanningContext context, Microsoft.AspNetCore.Identity.UserManager<Domain.UserAgg.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<RequestDto?> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.FirstOrDefault(s => s.UserName == request.userName);

            var requests = user.RequestBox.Where(i => i.Id.Equals(request.id));
            foreach (var item in requests)
            {
                //foreach (var item1 in item)
                //{
                    //var item2 = item.Where(i => i.Id.Equals(request.id)).FirstOrDefault();

                    var model = new RequestDto
                    {
                        Description = item.Description,
                        SenderId = item.SenderId,
                        CreationDate = item.CreationDate,
                        Id = item.Id,
                        RecipientId = item.ReceiverId,
                        Title = item.Title,
                    };
                    return model;
                //}
            }
            return null;
        }
    }
}
