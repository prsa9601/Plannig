using AngleSharp.Io;
using Common.Application;
using Domain.UserAgg;
using Domain.UserAgg.Repository;
using Domain.UserAgg.Service;
using Hangfire;

namespace Application.User
{
    public class UserService : IUserService
    {

        //private readonly IDistributedLockFactory _lockFactory;

        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public UserService(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public bool EmailIsExist(string email)
        {
            return _repository.Exists(b => b.Email == email);
        }

        public async Task<bool> DeActiveUserPackage(string userId)
        {
            //await using (var redLock = await _lockFactory.CreateLockAsync(resource, expiry))
            //{
            try
            {
                var user = await _repository.GetTrackingWithString(userId);
                if (user == null)
                    return false;
                user.DeActivePackageForUser(userId);
                await _repository.Save();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            //}
        }

        public bool PhoneNumberIsExist(string phoneNumber)
        {
            return _repository.Exists(p => p.PhoneNumber == phoneNumber);
        }
        public bool UserNameIsExist(string userName)
        {
            return _repository.Exists(p => p.UserName == userName);
        }
    }
}
