using Query.User._Package.UsersPackagesDTOs;
using Query.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.User._Package
{
    public static class UserPackageMapper
    {
        public static UsersPackagesDto? UsersPackagesMap(this Domain.UserAgg.User? user)
        {
            var model = new UsersPackagesDto
            {
                CreationDate = user!.CreationDate,
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName!,
                PhoneNumber = user.PhoneNumber,
                userPackages = user.UserPackages!.UserPackageMap()!
            };
            return model;
        }
        public static List<UserPackageDto>? UserPackageMap(this List<Domain.UserAgg.UserPackage>? package)
        {
            var userPackages = new List<UserPackageDto>();
            foreach (var item in package)
            {
                var model = new UserPackageDto
                {
                    AllowedEmailCount = item.AllowedEmailCount,
                    AllowedSmsCount = item.AllowedSmsCount,
                    IsActive = item.IsActive,
                    CreationDate = item.CreationDate,
                    ExpiryDate = item.ExpiryDate,
                    Id = item.Id,
                    PackageId = item.PackageId,
                    UserId = item.UserId,
                    //ExpireDate = item.ExpiryDate
                };
                userPackages.Add(model);
            }
            return userPackages;
        }
        public static UserPackageDto? UserPackageMap(this Domain.UserAgg.UserPackage? package, Domain.PackageAgg.Package packageDomain)
        {
            return new UserPackageDto
            {
                AllowedEmailCount = package!.AllowedEmailCount,
                AllowedSmsCount = package.AllowedSmsCount,
                IsActive = package.IsActive,
                CreationDate = package.CreationDate,
                ExpiryDate = package.ExpiryDate,
                Id = package.Id,
                PackageId = package.PackageId,
                UserId = package.UserId,
                Title = package.PackageTitle,
                Price = packageDomain.Price 
                //ExpireDate = item.ExpiryDate
            };
        }
        public static UsersSinglePackagesDto? UsersSinglePackagesMap(this Domain.UserAgg.User? user,
            string userId, long packageId)
        {
            var model = new UsersSinglePackagesDto
            {
                CreationDate = user!.CreationDate,
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName!,
                PhoneNumber = user.PhoneNumber,
                userPackages = user.UserPackages.
                FirstOrDefault(i => i.PackageId.
                Equals(packageId) && i.UserId.Equals(userId)).UserPackageSingleMap()!
            };
            return model;
        }
        public static UserSinglePackageDto? UserPackageSingleMap(this Domain.UserAgg.UserPackage? package)
        {
            var model = new UserSinglePackageDto
            {
                AllowedEmailCount = package.AllowedEmailCount,
                AllowedSmsCount = package.AllowedSmsCount,
                IsActive = package.IsActive,
                CreationDate = package.CreationDate,
                ExpiryDate = package.ExpiryDate,
                Id = package.Id,
                PackageId = package.PackageId,
                UserId = package.UserId,

            };
            return model;
        }
    }
}
