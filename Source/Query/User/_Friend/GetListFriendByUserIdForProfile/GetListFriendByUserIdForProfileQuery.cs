using Common.Query;
using Domain.UserAgg;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User._Friend.DTOs;
using Query.User._RequestBox;

namespace Query.User._Friend.GetListFriendByUserIdForProfile
{
    public class GetListFriendByUserIdForProfileQuery : QueryFilter<UserFriendFilterResult, UserFriendFilterParam>
    {
        public GetListFriendByUserIdForProfileQuery(UserFriendFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal class GetListFriendByUserIdForProfileQueryHandler : IQueryHandler<GetListFriendByUserIdForProfileQuery, UserFriendFilterResult>
    {
        private readonly PlanningContext _context;

        public GetListFriendByUserIdForProfileQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<UserFriendFilterResult> Handle(GetListFriendByUserIdForProfileQuery request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var requests = _context.Users.Select(i => i).ToList();
            var userFriend = new List<List<UserFriends>>();
            if (!string.IsNullOrWhiteSpace(@param.CurrentUserId)&&string.IsNullOrWhiteSpace(@param.UserName))
            {
                userFriend = requests.Where(i => i.Id.Equals(@param.CurrentUserId)).Select(i => i.friends).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(@param.UserName))
            {
                userFriend = requests.Where(i => i.Id.Contains(@param.UserName)).Select(i => i.friends).ToList();
            }

           //var user = await _context.Users.Where(i => i.Id == request.Id).FirstOrDefaultAsync();

            var friends = new List<FriendDto>();
            foreach (var item in userFriend)
            {
                foreach (var item1 in item)
                {
                   // var item2 = item.Where(i => i.CurrentUserId.Equals(user.Id)).FirstOrDefault();

                    var model1 = new FriendDto
                    {
                        CreationDate = item1.CreationDate,
                        FriendId = item1.UserFriendId,
                        Id = item1.Id,
                        UserId = item1.CurrentUserId,
                        FriendUserName = item1.UserFriendId.GetUserNameByIdUser(_context),
                        avatar = item1.UserFriendId.MapFriendAvatar(_context)
                    };
                    friends.Add(model1);
                }
            }
            var skip = (@param.PageId - 1) * @param.Take;
            var model = new UserFriendFilterResult()
            {
                Data = await Task.Run(()=> friends.Skip(skip).Take(@param.Take).Select(s => s).AsQueryable()
                    .ToList()),
                FilterParams = @param
            };
            model.GeneratePaging(friends.AsQueryable(), @param.Take, @param.PageId);
            return model;
        }
    }
}
