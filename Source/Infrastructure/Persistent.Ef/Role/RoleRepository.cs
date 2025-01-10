using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using Domain.RoleAgg.Repository;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistent.Ef.Role
{

    public class RoleRepository<TEntity> : IRoleRepository<TEntity> where TEntity : IdentityRole
    {
        protected readonly PlanningContext Context;
        public RoleRepository(PlanningContext context)
        {
            Context = context;
        }
        public async Task<bool> DeleteRole(string roleId)
        {
            var role = await Context.Set<TEntity>().FirstOrDefaultAsync(i => i.Id == roleId);
            if (role == null)
                return false;
            Context.Set<TEntity>().Remove(role);
            await Context.SaveChangesAsync();
            return true;
        }
        public virtual async Task<TEntity?> GetAsync(long id)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(t => t.Id.Equals(id)); ;
        }
        public async Task<TEntity?> GetTracking(long id)
        {
            return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        //public async Task<TEntity?> GetTrackingByUserName(string UserName)
        //{
        //    return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.UserName.Equals(UserName));
        //}

        public async Task<TEntity?> GetTrackingWithString(string id)
        {
            return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
        }
        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public async Task AddRange(ICollection<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
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

        //public async Task<TEntity?> GetTrackingByPhoneNumber(string PhoneNumber)
        //{
        //    return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.PhoneNumber.Equals(PhoneNumber));
        //}
    }
}
