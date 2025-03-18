using Domain.UserAgg.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistent.Ef.UserAgg
{
    public class UserRepository<TEntity> : IUserRepository<TEntity> where TEntity : IdentityUser
    {
        protected readonly PlanningContext Context;
        public UserRepository(PlanningContext context)
        {
            Context = context;
        }
        
        public virtual async Task<TEntity?> GetAsync(long id)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(t => t.Id.Equals(id)); ;
        }
        public virtual async Task<TEntity?> GetWithStringAsync(string id)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(t => t.Id.Equals(id)); ;
        }
        public async Task<TEntity?> GetTracking(long id)
        {
            return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));

        }
        public async Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(expression);
        }
        public async Task<TEntity?> GetTrackingByUserName(string UserName)
        {
            return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.UserName.Equals(UserName));
        }

        public async Task<TEntity?> GetTrackingWithString(string id)
        {
            return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
        }
        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        void IUserRepository<TEntity>.Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public async Task AddRange(ICollection<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task<List<TEntity>> GetListAsync(List<string> userNames)
        {
            List<TEntity> users = new List<TEntity>();
            foreach (var item in userNames)
            { 
                users.Add( await Context.Set<TEntity>()
                    .Where(i => i.UserName.Equals(item)).FirstOrDefaultAsync());
            }
            return users;
        }

        public void Update(TEntity entity)
        {
            Context.Update(entity);
        }
        public async Task<int> Save()
        {
            return await Context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Context.Set<TEntity>().AnyAsync(expression);
        } 
        //public async Task<List<TEntity>?> GetListAsync(Expression<Func<TEntity, bool>> expression)
        //{
        //    return await Context.Set<TEntity>().Where(expression).ToListAsync();
        //}
        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().Any(expression);
        }

        public TEntity? Get(long id)
        {
            return Context.Set<TEntity>().FirstOrDefault(t => t.Id.Equals(id)); ;
        }
        public async Task<bool> Delete(long Id)
        {
            try
            {
                var entity = await Context.Set<TEntity>().FirstOrDefaultAsync(t => t.Id.Equals(Id));
                Context.Set<TEntity>().Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(string Id)
        {
            try
            {
                var entity = await Context.Set<TEntity>().FirstOrDefaultAsync(t => t.Id.Equals(Id));
                Context.Set<TEntity>().Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<TEntity?> GetTrackingByPhoneNumber(string PhoneNumber)
        {
            return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.PhoneNumber.Equals(PhoneNumber));
        }
    }
}
