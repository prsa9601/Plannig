using Domain.EventAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.EventAgg
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events","dbo");

            builder.Property(b=>b.Title).IsRequired().HasMaxLength(50);

            builder.Property(b=>b.Description).IsRequired();

            builder.OwnsMany(b => b.eventUser, option =>
            {
                option.ToTable("eventUser", "dbo");
                option.HasIndex(b => b.EventId);
            });
        }
    }
}
