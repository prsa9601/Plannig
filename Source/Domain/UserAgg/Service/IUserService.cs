
namespace Domain.UserAgg.Service
{
    public interface IUserService
    {
        bool PhoneNumberIsExist(string phoneNumber);
        bool UserNameIsExist(string userName);
        bool EmailIsExist(string email);
    }
}
