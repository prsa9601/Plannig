using Domain.UserAgg;
using Domain.UserAgg.Repository;
using Domain.UserAgg.Service;

namespace Application.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<Domain.UserAgg.User> _repository;

        public UserService(IUserRepository<Domain.UserAgg.User> repository)
        {
            _repository = repository;
        }

        public bool EmailIsExist(string email)
        {
            return _repository.Exists(b => b.Email == email);
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
