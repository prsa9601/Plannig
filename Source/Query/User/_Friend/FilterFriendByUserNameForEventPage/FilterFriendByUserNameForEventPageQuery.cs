using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Query;
using Domain.UserAgg;
using Infrastructure.Persistent.Ef;
using Query.User._Friend.DTOs;

namespace Query.User._Friend.FilterFriendByUserNameForEventPage
{
    public class FilterFriendByUserNameForEventPageQuery : QueryFilter<SearchFriendForEventFilterResult, SearchFriendForEventFilterParam>
    {
        public FilterFriendByUserNameForEventPageQuery(SearchFriendForEventFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal class FilterFriendByUserNameForEventPageQueryHandler : IQueryHandler<FilterFriendByUserNameForEventPageQuery, SearchFriendForEventFilterResult>
    {
        private readonly PlanningContext _context;

        public FilterFriendByUserNameForEventPageQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<SearchFriendForEventFilterResult> Handle(FilterFriendByUserNameForEventPageQuery request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var requests = _context.Users.Select(i => i).ToList();
            var currentUser = requests.Where(i => i.Id.Equals(@param.CurrentUserId)).FirstOrDefault();
            //  var Friends = currentUser.friends.Select(i => i.CurrentUserId || i.UserFriendId).ToList();
            var userFriend = new List<SearchFriendForEventData>();
            if (!string.IsNullOrWhiteSpace(@param.CurrentUserId) && !string.IsNullOrWhiteSpace(@param.UserName))
            {
                foreach (var item in currentUser.friends)
                {
                    requests = requests.Where(i => i.Id.Equals(item.CurrentUserId) || i.Id.Equals(item.UserFriendId)).ToList();
                    //Task.Delay();
                    if (requests.Count() != 0)
                    {
                        foreach (var item1 in requests)
                        {
                            var searchData = new SearchFriendForEventData()
                            {
                                Id = item1.Id,
                                CreationDate = item1.CreationDate,
                                PhoneNumber = item1.PhoneNumber,
                                UserName = item1.UserName,
                                avatar = item1.Id.MapFriendAvatar(_context)
                            };
                            userFriend.Add(searchData);
                        }
                    }
                }
            }
            //var user = await _context.Users.Where(i => i.Id == request.Id).FirstOrDefaultAsync();
            var userCurrent = userFriend.Where(i => i.Id.Equals(@param.CurrentUserId)).ToList();

            if (userCurrent != null)
            {
                foreach (var item in userCurrent)
                {
                    userFriend.Remove(item);
                }
            }

            var skip = (@param.PageId - 1) * @param.Take;
            var model = new SearchFriendForEventFilterResult()
            {
                Data = await Task.Run(() => userFriend.Skip(skip).Take(@param.Take).Select(s => s).AsQueryable()
                    .ToList()),
                FilterParams = @param
            };
            model.GeneratePaging(userFriend.AsQueryable(), @param.Take, @param.PageId);
            return model;
        }
    }
}
