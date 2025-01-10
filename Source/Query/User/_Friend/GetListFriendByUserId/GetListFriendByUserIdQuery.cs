using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User._Friend.DTOs;
using Query.User._RequestBox;

namespace Query.User._Friend.GetListFriendByUserId
{
    public class GetListFriendByUserIdQuery : IQuery<List<FriendDto?>>
    {
        public GetListFriendByUserIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
    internal class GetListFriendByUserIdQueryHandler : IQueryHandler<GetListFriendByUserIdQuery, List<FriendDto?>>
    {
        private readonly PlanningContext _context;

        public GetListFriendByUserIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<List<FriendDto?>> Handle(GetListFriendByUserIdQuery request, CancellationToken cancellationToken)
        {
            var requests = await _context.Users.Where(i => i.Id.Equals(request.Id)).Select(i => i.friends).ToListAsync();
            
            var user = await _context.Users.Where(i => i.Id == request.Id).FirstOrDefaultAsync();

            var friends = new List<FriendDto>();
            foreach (var item in requests)
            {
                foreach (var item1 in item)
                {
                    var item2 = item.Where(i => i.CurrentUserId.Equals(user.Id)).FirstOrDefault();

                    var model = new FriendDto
                    {
                        CreationDate = item2.CreationDate,
                        FriendId = item2.UserFriendId,
                        Id = item2.Id,
                        UserId = item2.CurrentUserId,
                        FriendUserName = item2.UserFriendId.GetUserNameByIdUser(_context),
                        avatar = item2.UserFriendId.MapFriendAvatar(_context)
                    };
                    friends.Add(model);
                }
            }
            return friends;
        }
    }
}
