using Domain.EventAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.EventAgg
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events","event");

            builder.Property(b=>b.Title).IsRequired().HasMaxLength(50);
            builder.HasKey(b=>b.Id);

            builder.Property(b=>b.Description).IsRequired();

            builder.OwnsMany(b => b.EventUser, option =>
            {
                option.ToTable("eventUser", "event");
                option.HasIndex(b => b.EventId);
            });
        }
    }
}
