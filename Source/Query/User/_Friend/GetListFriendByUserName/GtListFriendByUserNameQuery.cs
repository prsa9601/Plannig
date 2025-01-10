using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User._Friend.DTOs;
using Query.User._RequestBox;

namespace Query.User._Friend.GetListFriendByUserName
{
    public class GtListFriendByUserNameQuery : IQuery<List<FriendDto?>>
    {
        public GtListFriendByUserNameQuery(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; set; }
    }
    internal class GtListFriendByUserIdQueryHandler : IQueryHandler<GtListFriendByUserNameQuery, List<FriendDto?>>
    {
        private readonly PlanningContext _context;

        public GtListFriendByUserIdQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<List<FriendDto?>> Handle(GtListFriendByUserNameQuery request, CancellationToken cancellationToken)
        {
            var requests = await _context.Users.Where(i => i.UserName.Equals(request.UserName)).Select(i => i.friends).ToListAsync();
            
            var user = await _context.Users.Where(i => i.UserName == request.UserName).FirstOrDefaultAsync();
            
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
