using Common.Query;
using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User._Friend.DTOs;
using Query.User.DTOs;

namespace Query.User._Friend.GetFiendFilterForProfile
{
    public class GetFriendFilterForProfileQuery : QueryFilter<FriendDtoForProfileResult, FriendDtoForProfileParam>
    {
        public GetFriendFilterForProfileQuery(FriendDtoForProfileParam filterParams) : base(filterParams)
        {
        }
    }
    internal class GetFriendFilterForProfileQueryHandler : IQueryHandler<GetFriendFilterForProfileQuery, FriendDtoForProfileResult>
    {
        private readonly PlanningContext _context;

        public GetFriendFilterForProfileQueryHandler(PlanningContext context)
        {
            _context = context;
        }

        public async Task<FriendDtoForProfileResult> Handle(GetFriendFilterForProfileQuery request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var user = await _context.Users.Select(i => i).ToListAsync();
            var userFriend = await _context.Users.Where(i=>i.Id.Equals(@param.CurrentUserId)).FirstOrDefaultAsync();
            var result = new List<FriendDtoForProfile>();
            if (!string.IsNullOrWhiteSpace(param.UserName) && !string.IsNullOrWhiteSpace(param.CurrentUserId))
            {
                foreach (var item in user)
                {
                    //if (item.friends.Any(i => i.UserFriendId.Equals(Friends.Select(i => i.Id))))
                    if (item.UserName.Contains(@param.UserName))
                    {
                        if (item.friends.Any(i=>i.CurrentUserId.Equals(@param.CurrentUserId) || i.UserFriendId.Equals(@param.CurrentUserId)))
                        {
                            if (item.RequestBox.Any(i => i.ReceiverId.Equals(request.FilterParams.CurrentUserId) || i.SenderId.Equals(request.FilterParams.CurrentUserId)))
                            {
                                result.Add(new FriendDtoForProfile()
                                {
                                    UserId = item.Id,
                                    FriendUserName = item.UserName,
                                    CreationDate = item.CreationDate,
                                    FriendId = param.CurrentUserId,
                                    IsFriend = true,
                                    IsSendRequest = true,
                                    avatar = new UserFriendAvatarDto()
                                    {
                                        Avatar = item.Avatar.avatar,
                                        CreationDate = item.Avatar.CreationDate,
                                        Id = item.Avatar.Id,
                                        UserId = item.Avatar.UserId
                                    }
                                });
                            }
                            else
                            {
                                result.Add(new FriendDtoForProfile()
                                {
                                    UserId = item.Id,
                                    FriendUserName = item.UserName,
                                    CreationDate = item.CreationDate,
                                    FriendId = param.CurrentUserId,
                                    IsFriend = true,
                                    IsSendRequest = false,
                                    avatar = new UserFriendAvatarDto()
                                    {
                                        Avatar = item.Avatar.avatar,
                                        CreationDate = item.Avatar.CreationDate,
                                        Id = item.Avatar.Id,
                                        UserId = item.Avatar.UserId
                                    }
                                });
                            }


                        }
                        else
                        {
                            if (item.RequestBox.Any(i => i.ReceiverId.Equals(request.FilterParams.CurrentUserId) || i.SenderId.Equals(request.FilterParams.CurrentUserId)))
                            {
                                result.Add(new FriendDtoForProfile()
                                {
                                    UserId = item.Id,
                                    FriendUserName = item.UserName,
                                    CreationDate = item.CreationDate,
                                    FriendId = param.CurrentUserId,
                                    IsFriend = false,
                                    IsSendRequest = true,
                                    avatar = new UserFriendAvatarDto()
                                    {
                                        Avatar = item.Avatar.avatar,
                                        CreationDate = item.Avatar.CreationDate,
                                        Id = item.Avatar.Id,
                                        UserId = item.Avatar.UserId
                                    }
                                });
                            }
                            else
                            {
                                result.Add(new FriendDtoForProfile()
                                {
                                    UserId = item.Id,
                                    FriendUserName = item.UserName,
                                    CreationDate = item.CreationDate,
                                    FriendId = param.CurrentUserId,
                                    IsFriend = false,
                                    IsSendRequest = false,
                                    avatar = new UserFriendAvatarDto()
                                    {
                                        Avatar = item.Avatar.avatar,
                                        CreationDate = item.Avatar.CreationDate,
                                        Id = item.Avatar.Id,
                                        UserId = item.Avatar.UserId
                                    }
                                });
                            }


                        }

                    }
                    //else
                    //{
                    //    result.Add(new FriendDtoForProfile()
                    //    {
                    //        UserId = item.Id,
                    //        FriendUserName = item.UserName,
                    //        CreationDate = item.CreationDate,
                    //        FriendId = param.CurrentUserId,
                    //        IsFriend = false,
                    //        avatar = new UserFriendAvatarDto()
                    //        {
                    //            Avatar = item.Avatar.avatar,
                    //            CreationDate = item.Avatar.CreationDate,
                    //            Id = item.Avatar.Id,
                    //            UserId = item.Avatar.UserId
                    //        }
                    //    });
                    //}

                }

            }

            else if (string.IsNullOrWhiteSpace(param.UserName) && !string.IsNullOrWhiteSpace(param.CurrentUserId))
            {
                List<Domain.UserAgg.User> Friends = new List<Domain.UserAgg.User>();

                var friendIds = userFriend.friends.Select(i => i.UserFriendId).ToList();
                foreach (var item in friendIds)
                {
                     Friends.AddRange(await _context.Users.Where(i => i.Id.Equals(item)).ToListAsync());

                }

                foreach (var item in Friends)
                {
                    result.Add(new FriendDtoForProfile()
                    {
                        UserId = item.Id,
                        FriendUserName = item.UserName,
                        CreationDate = item.CreationDate,
                        FriendId = param.CurrentUserId,
                        IsFriend = item.friends.Any(i => i.CurrentUserId.Equals(@param.CurrentUserId) || i.UserFriendId.Equals(@param.CurrentUserId)),
                        IsSendRequest = item.RequestBox.Any(i => i.ReceiverId.Equals(request.FilterParams.CurrentUserId) || i.SenderId.Equals(request.FilterParams.CurrentUserId)),

                        avatar = new UserFriendAvatarDto()
                        {
                            Avatar = item.Avatar.avatar,
                            CreationDate = item.Avatar.CreationDate,
                            Id = item.Avatar.Id,
                            UserId = item.Avatar.UserId
                        }
                    });

                }

            }

            var currentUser = result.Where(i =>
                i.UserId.Equals(request.FilterParams.CurrentUserId) &&
                i.FriendId.Equals(request.FilterParams.CurrentUserId)).FirstOrDefault();

            var q = result.Remove(currentUser);
            var skip = (@param.PageId - 1) * @param.Take;
            var model = new FriendDtoForProfileResult()
            {
                Data = result.Skip(skip).Take(@param.Take).Select(s => s)
                    .ToList(),
                FilterParams = @param
            };
            model.GeneratePaging(result.AsQueryable(), @param.Take, @param.PageId);
            return model;

        }
    }
}
