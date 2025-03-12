
namespace Domain.UserAgg.Service
{
    public interface IUserService
    {
        Task<bool> DeActiveUserPackage(string userId);
        bool PhoneNumberIsExist(string phoneNumber);
        bool UserNameIsExist(string userName);
        bool EmailIsExist(string email);
    }
}
