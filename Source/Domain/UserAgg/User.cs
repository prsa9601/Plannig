using Common.Domain;
using Common.Domain.Exceptions;
using Domain.UserAgg;
using Domain.UserAgg.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Domain.UserAgg
{
    public class User : IdentityUser
    {
        private User()
        {

        }
        //  public long Id { get; set; }
        public DateTime CreationDate { get; set; }

        public string Name { get; set; }
        public string? Family { get; private set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public UserAvatar Avatar { get; set; }

        public List<UserRole?> Roles { get; } = new List<UserRole?>();
        public List<UserToken?> Tokens { get; } = new List<UserToken?>();
        public List<Wallet?> Wallets { get; } = new List<Wallet?>();
        public List<UserFriends?> friends { get; } = new List<UserFriends?>();
        public List<UserEvent?> userEvents { get; } = new List<UserEvent?>();
        public List<RequestBox?> RequestBox { get; } = new List<RequestBox?>();
        public List<UserPackage?> UserPackages { get; } = new List<UserPackage?>();


        public User(string email, string userName, string phoneNumber, string password, IUserService userService)
        {
            Guard(phoneNumber, userService);
            GuardUserName(userName, userService);
            Password = password;
            Email = email;
            UserName = userName;
            //Family = family;
            PhoneNumber = phoneNumber;
            //Password = password;
            CreationDate = DateTime.Now;
            Avatar = new UserAvatar(0);

        }

        //public User(string email, string userName, string phoneNumber, string password)
        //{
        //    Avatar = new UserAvatar(0);
        //    Avatar.UserId = Id;
        //    Password = password;
        //    Email = email;
        //    UserName = userName;
        //    //Family = family;
        //    PhoneNumber = phoneNumber;
        //    //Password = password;
        //    Avatar = new UserAvatar(0);

        //}

        //public User( string phoneNumber, IUserService userService)
        //{
        //    Guard(phoneNumber,userService);
        //    PhoneNumber = phoneNumber;
        //   // this.UserName = UserName;, string UserName
        //   // Password = password;
        //    CreationDate = DateTime.Now;

        //}
        public void ChangeActivityStatus()
        {
            if(IsActive)
                IsActive = false;
            else
                IsActive = true;
        }  
        public void ChangeEmailConfirmedStatus()
        {
            if (EmailConfirmed)
                EmailConfirmed = false;
            else
                EmailConfirmed = true;
        } 
        public void ChangePhoneNumberConfirmedStatus()
        {
            if (PhoneNumberConfirmed)
                PhoneNumberConfirmed = false;
            else
                PhoneNumberConfirmed = true;
        }
        public void SetRoles(List<UserRole> roles)
        {
            roles.ForEach(f => f.UserId = Id);
            Roles.Clear();
            Roles.AddRange(roles);
        }

        public void SetUserRoles(List<string> roles)
        {
            List<UserRole> userRoles = new List<UserRole>();
            foreach (var item in roles)
            {
                userRoles.Add(new UserRole(item));
            }
            userRoles.ForEach(f => f.UserId = Id);
            Roles.Clear();
            Roles.AddRange(userRoles);
        }
        public void AddToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
        {
            var activeTokenCount = Tokens.Count(c => c.RefreshTokenExpireDate > DateTime.Now);
            if (activeTokenCount == 3)
                throw new InvalidDomainDataException("امکان استفاده از 4 دستگاه همزمان وجود ندارد");

            var token = new UserToken(hashJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate, device);
            token.UserId = Id;
            Tokens.Add(token);
        }
        public string RemoveToken(long tokenId)
        {
            var token = Tokens.FirstOrDefault(f => f.Id == tokenId);
            if (token == null)
                throw new InvalidDomainDataException("invalid TokenId");

            Tokens.Remove(token);
            return token.HashJwtToken;
        }
        public void Edit(string name, string family, string phoneNumber, string email, string userName, IUserService userService)
        {
            //Guard(phoneNumber, userService);
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Email = email;
            UserName = userName;
        }

        public void ChargeWallet(Wallet wallet)
        {
            wallet.UserId = Id;
            Wallets.Add(wallet);
        }

        public void AddEvent(List<long> eventId)
        {
            List<UserEvent> participants = new List<UserEvent>();

            foreach (var item in eventId)
            {
                participants.Add(new UserEvent(item));
            }
            participants.ForEach(f => f.UserId = Id);

            userEvents.Clear();
            userEvents.AddRange(participants);
        }
        public void AddEvent(long eventId)
        {
            var userEvent = new UserEvent(eventId);
            userEvent.UserId = Id;
            userEvents.Add(userEvent);
        }
        public void AddFriend(List<string> friendsId)
        {
            List<UserFriends> friendsList = new List<UserFriends>();
            foreach (var item in friendsId)
            {
                friendsList.Add(new UserFriends(item));
            }
            friends.ForEach(f => f.CurrentUserId = Id);

            friends.Clear();
            friends.AddRange(friendsList);
        }
        public void AddRequest(string userName, string senderId)
        {
            foreach (var item in RequestBox)
            {
                if (item.SenderId == senderId && item.ReceiverId == Id)
                {
                    throw new Exception("شما به این کاربر یک بار درخواست دادید!");
                }
            }
            if (Id == senderId)
            {
                throw new Exception("درخواست شما نامعتبر است!");
            }
            var request = new RequestBox(Id, userName);

            request.SenderId = senderId;

            RequestBox.Add(request);
        }
        public void AddRequest(string receiverId)
        {
            foreach (var item in RequestBox)
            {
                if (item.ReceiverId == receiverId && item.SenderId == Id)
                {
                    throw new Exception("شما به این کاربر یک بار درخواست دادید!");
                }
            }
            if (Id == receiverId)
            {
                throw new Exception("درخواست شما نامعتبر است!");
            }
            var request = new RequestBox(receiverId, UserName);

            request.SenderId = Id;

            RequestBox.Add(request);
        }
        public bool AddFriend(string friendId)
        {
            if (RequestBox.Any(i => (i.ReceiverId.Equals(friendId) && i.SenderId.Equals(Id))
                                    || i.ReceiverId.Equals(Id) && i.SenderId.Equals(friendId)))
            {
                foreach (var item in friends)
                {
                    if (item.CurrentUserId == Id && item.UserFriendId == friendId ||
                        item.CurrentUserId == friendId && item.UserFriendId == Id)
                    {
                        throw new Exception("درخواست غیر مجاز است!");
                    }
                }
                var friend = new UserFriends(friendId);

                friend.CurrentUserId = Id;


                friends.Add(friend);
                return true;
            }
            else
            {
                throw new Exception("درخواست غیر مجاز است!");
                return false;
            }

        }
        public void AddFriends(string friendId)
        {

            foreach (var item in friends)
            {
                if (item.CurrentUserId == Id && item.UserFriendId == friendId ||
                    item.CurrentUserId == friendId && item.UserFriendId == Id)
                {
                    throw new Exception("درخواست غیر مجاز است!");
                }
            }
            var friend = new UserFriends(friendId);

            friend.CurrentUserId = Id;


            friends.Add(friend);

        }
        public void RemoveRequest(string receiverId, string senderId)
        {
            var request = RequestBox.Where(f => f.SenderId == senderId && f.ReceiverId == receiverId || f.SenderId == receiverId && f.ReceiverId == senderId).FirstOrDefault();
            RequestBox.Remove(request);
        }
        public void RemoveFriend(string friendId)
        {
            var friend = friends.Where(f => f.CurrentUserId == Id && f.UserFriendId == friendId).FirstOrDefault();
            friends.Remove(friend);
        }

        public void AddAvatar(UserAvatar avatar)
        {
            avatar.UserId = Id;
            this.Avatar = avatar;
        }
        public void SetAvatar(UserAvatar avatar)
        {
            avatar.UserId = Id;
            this.Avatar = null;
            this.Avatar = avatar;
        }
        public void Guard(
             string phoneNumber, IUserService userService)
        {
            if (phoneNumber.Length != 11)
                throw new InvalidDomainDataException("شماره موبایل نامعتبر است");

            //if (string.IsNullOrWhiteSpace(name))
            //    throw new InvalidDomainDataException(" نام نامعتبر است");

            //if (string.IsNullOrWhiteSpace(family))
            //    throw new InvalidDomainDataException(" نام خانوادگی نامعتبر است");

            if (phoneNumber != PhoneNumber)
                if (userService.PhoneNumberIsExist(phoneNumber))
                    throw new Exception("شما با این شماره تماس ثبت نام کرده اید!");

            //if (email != Email)
            //    if (userService.EmailIsExist(email))
            //        throw new Exception("شما از این ایمیل در یک اکانت استفاده کرده اید!");

        }
        public void GuardUserName(
             string userName, IUserService userService)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new InvalidDomainDataException("نام کاربری را وارد کنید!");

            if (userName != UserName)
                if (userService.UserNameIsExist(userName))
                    throw new Exception("شما با این نام کاربری ثبت نام کرده اید!");

        }
        public void SetUserPackage(DateTime time, long packageId, int AllowedSmsCount,
            int AllowedEmailCount, string packageTitle)
        {
            var package = new UserPackage(packageId, AllowedSmsCount, AllowedEmailCount, time, packageTitle);
            package.UserId = Id;
            package.IsActive = true;
            UserPackages.Add(package);
        }
        public void EditUserPackage(long packageId, DateTime time, int AllowedSmsCount,
            int AllowedEmailCount)
        {
            var package = UserPackages.Where(i =>
                i.UserId == Id && i.PackageId == packageId && i.IsActive == true).FirstOrDefault();
            package.Edit(time, AllowedSmsCount, AllowedEmailCount);
            if (time.Equals(0))
            {
                package.IsActive = false;
            }
            UserPackages.Add(package);
        }
        public void RemoveUserPackage(string userId)
        {
            UserPackages.RemoveAll(i => i.UserId == Id && i.UserId == userId);
        }
        public void DeActivePackageForUser(string userId)
        {
            try
            {
                var package = UserPackages.Where(i =>
                    i.UserId == Id && i.UserId == userId && i.IsActive == true).FirstOrDefault();
                package.IsActive = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

    }

}

