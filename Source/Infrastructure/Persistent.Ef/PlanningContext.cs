using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Infrastructure._Utilities.MediatR;

namespace Infrastructure.Persistent.Ef
{
    public class PlanningContext : IdentityDbContext<Domain.UserAgg.User, Domain.RoleAgg.Role, string>
    {
        //private readonly ICustomPublisher _publisher;, ICustomPublisher publisher
        public PlanningContext(DbContextOptions<PlanningContext> options) : base(options)
        {
            //_publisher = publisher;
            ////options.UseSqlServer(("DefaultConnection"),
            ////    sqlServerOptionsAction: sqlOptions =>
            ////    {
            ////        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            ////    });
        }

        //public DbSet<Domain.UserAgg.User> Users { get; set; }
        public DbSet<Domain.PackageAgg.Package> Packages { get; set; }
     
        public DbSet<Domain.EventAgg.Event> Events { get; set; }
        public DbSet<Domain.SocialMediaAgg.InstagramAgg.Instagram> Instagram { get; set; }
        public DbSet<Domain.SocialMediaAgg.TelegramAgg.Telegram> Telegrams { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var modifiedEntities = GetModifiedEntities();
            //await PublishEvents(modifiedEntities);
            return await base.SaveChangesAsync(cancellationToken);
        }
        private List<AggregateRoot> GetModifiedEntities() =>
            ChangeTracker.Entries<AggregateRoot>()
                .Where(x => x.State != EntityState.Detached)
                .Select(c => c.Entity)
                .Where(c => c.DomainEvents.Any()).ToList();

        //private async Task PublishEvents(List<AggregateRoot> modifiedEntities)
        //{
        //    foreach (var entity in modifiedEntities)
        //    {
        //        var events = entity.DomainEvents;
        //        foreach (var domainEvent in events)
        //        {
        //            await _publisher.Publish(domainEvent, PublishStrategy.ParallelNoWait);
        //        }
        //    }
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlanningContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
