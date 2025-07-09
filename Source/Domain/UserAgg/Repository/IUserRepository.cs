using Common.Domain;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Domain.UserAgg.Repository
{
    public interface IUserRepository<T> where T : IdentityUser
    {
        Task<T?> GetAsync(long id);
        Task<T?> GetWithStringAsync(string id);

        Task<T?> GetTracking(long id);
        Task<T?> GetTrackingByPhoneNumber(string PhoneNumber);
        Task<T?> GetTrackingByUserName(string UserName);
        Task<T?> GetTrackingWithString(string id);
        Task<List<T>> GetAllUser();
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> expression);
        Task<List<T>?> GetListByFilterAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        void Add(T entity);

        Task AddRange(ICollection<T> entities);
        Task<List<T>> GetListAsync(List<string> userNames);
        //Task<List<T>?> GetListAsync(Expression<Func<T, bool>> expression);

        void Update(T entity);

        Task<int> Save();

        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);

        bool Exists(Expression<Func<T, bool>> expression);
        Task<bool> Delete(long Id);
        Task<bool> Delete(string Id);


        T? Get(long id);
        
    }
}
