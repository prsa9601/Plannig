using Infrastructure.Persistent.Ef;
using Microsoft.EntityFrameworkCore;
using Query.User.DTOs;
using System.Security.Cryptography;

namespace Query.User
{
    public static class UserMapper
    {
        public static UserDto? Map(this Domain.UserAgg.User? user, PlanningContext context)
        {
            var model = new UserDto()
            {
                Password = user.Password,
                UserName = user.UserName,
                Email = user.Email,
                CreationDate = user.CreationDate,
                Family = user.Family,
                friends = user.friends.FriendsMap(context)!,
                Id = user.Id,
                Roles=user.Roles.Select(i=>new UserRoleDto
                {
                    RoleId = i.RoleId,
                    RoleName = "",
                }).ToList(),
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                avatar = user.Id.MapAvatar(context)!,
                userPackageDto = user.UserPackages.PackagesMap()!,
            };
            return model;
        }
        public static UserFilterData? Map(this Domain.UserAgg.User?
            user, string CurrentUserId, PlanningContext context)
        {
            var model = new UserFilterData()
            {
                Password = user.Password,
                Email = user.Email,
                CreationDate = user.CreationDate,
                Family = user.Family,
                CurrentUserId = CurrentUserId,
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                avatar = user.Id.MapAvatar(context)!,
            };
            return model;
        }
        public static List<FriendsDto?> FriendsMap(this List<Domain.UserAgg.UserFriends>? user, PlanningContext context)
        {
            var friends = new List<FriendsDto>();
            foreach (var item in user)
            {
                var model = new FriendsDto()
                {
                    CreationDate = item.CreationDate,
                    CurrentUserId = item.CurrentUserId,
                    Id = item.Id,
                    UserFriend = item.UserFriendId,
                    avatar = item.UserFriendId.MapAvatar(context),
                };
                friends.Add(model);
            }
            return friends;
        }
        public static List<UserPackageDto?> PackagesMap(this List<Domain.UserAgg.UserPackage>?
            userPackage)
        {
            var package = new List<UserPackageDto>();
            foreach (var item in userPackage)
            {
                var model = new UserPackageDto()
                {
                    CreationDate = item.CreationDate,
                    Id = item.Id,
                    AllowedEmailCount = item.AllowedEmailCount,
                    AllowedSmsCount = item.AllowedSmsCount,
                    ExpiryDate = item.ExpiryDate,
                    IsActive = item.IsActive,
                    PackageId = item.PackageId,
                    UserId = item.UserId,
                    //ExpireDate = item.ExpiryDate
                };
                package.Add(model);
            }
            return package;
        }
        public static List<UserPackageDto?> PackagesMap(this List<Domain.UserAgg.UserPackage>?
            userPackage, bool ActivePackage)
        {
            var package = new List<UserPackageDto>();
            if (ActivePackage)
            {
                userPackage = userPackage.Where(i => i.ExpiryDate > DateTime.Now).ToList();
                foreach (var item in userPackage)
                {
                    var model = new UserPackageDto()
                    {
                        CreationDate = item.CreationDate,
                        Id = item.Id,
                        AllowedEmailCount = item.AllowedEmailCount,
                        AllowedSmsCount = item.AllowedSmsCount,
                        ExpiryDate = item.ExpiryDate,
                        IsActive = item.IsActive,
                        PackageId = item.PackageId,
                        UserId = item.UserId,
                        //ExpireDate = item.ExpiryDate
                    };
                    package.Add(model);
                }
                return package;
            }
            else
            {
                userPackage = userPackage.Where(i => i.ExpiryDate > DateTime.Now).ToList();
                foreach (var item in userPackage)
                {
                    var model = new UserPackageDto()
                    {
                        CreationDate = item.CreationDate,
                        Id = item.Id,
                        AllowedEmailCount = item.AllowedEmailCount,
                        AllowedSmsCount = item.AllowedSmsCount,
                        ExpiryDate = item.ExpiryDate,
                        IsActive = item.IsActive,
                        PackageId = item.PackageId,
                        UserId = item.UserId,
                        //ExpireDate = item.ExpiryDate
                    };
                    package.Add(model);
                }
                return package;
            }
            userPackage = userPackage.Where(i => i.ExpiryDate > DateTime.Now).ToList();
            foreach (var item in userPackage)
            {
                var model = new UserPackageDto()
                {
                    CreationDate = item.CreationDate,
                    Id = item.Id,
                    AllowedEmailCount = item.AllowedEmailCount,
                    AllowedSmsCount = item.AllowedSmsCount,
                    ExpiryDate = item.ExpiryDate,
                    IsActive = item.IsActive,
                    PackageId = item.PackageId,
                    UserId = item.UserId,
                    //ExpireDate = item.ExpiryDate
                };
                package.Add(model);
            }
            return package;
        }
        public static UserAvatarDto? MapAvatar(this string id, PlanningContext context)
        {
            var avatar = context.Users.Where(i => i.Id == id).Select(i => i.Avatar).FirstOrDefault();
            return new UserAvatarDto()
            {
                Id = avatar.Id,
                Avatar = avatar.avatar,
                CreationDate = avatar.CreationDate,
                UserId = avatar.UserId,
            };
        }
        public static async Task<UserDto?> SetUserRoleTitles(this UserDto? userDto, PlanningContext context)
        {
            var roleIds = userDto.Roles.Select(r => r.RoleId);
            var result = await context.Roles.Where(r => roleIds.Contains(r.Id)).ToListAsync();
            var roles = new List<UserRoleDto>();
            foreach (var role in result)
            {
                roles.Add(new UserRoleDto()
                {
                    RoleId = role.Id,
                    RoleName = role.Name!
                });
            }

            userDto.Roles = roles;
            return userDto;
        }
        public static UserFilterDataForAdmin MapForAdmin(this Domain.UserAgg.User? user, PlanningContext context, bool ActivePackage)
        {

            return new UserFilterDataForAdmin()
            {
                Id = user.Id,
                avatar = MapAvatar(user.Id, context)!,
                CreationDate = user.CreationDate,
                Email = user.Email,
                UserName = user.UserName,
                Family = user.Family,
                Name = user.Name,
                IsActive = user.IsActive,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                userPackages = PackagesMap(user.UserPackages!, ActivePackage)!,
            };
        }
    }
}
