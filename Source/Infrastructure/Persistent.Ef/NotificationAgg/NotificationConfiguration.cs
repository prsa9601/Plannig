using Domain.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.NotificationAgg
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notification", "dbo");
            //builder.Property(b => b.UserNames).IsRequired();
            //builder.OwnsMany(b => b.UserNames);

        }
    }
}
