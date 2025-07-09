using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.NotificationAgg
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Domain.NotificationAgg.Notification>
    {
        public void Configure(EntityTypeBuilder<Domain.NotificationAgg.Notification> builder)
        {
            builder.ToTable("Notification", "dbo");
            //builder.Property(b => b.UserNames).IsRequired();
            //builder.OwnsMany(b => b.UserNames); 

        }
    }
}
