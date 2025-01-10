using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.Role
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Domain.RoleAgg.Role>
    {
        public void Configure(EntityTypeBuilder<Domain.RoleAgg.Role> builder)
        {

            builder.ToTable("Roles", "role");
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(60);

            builder.OwnsMany(b => b.Permissions, option =>
            {
                option.ToTable("Permissions", "role");
            });
        }
    }
}
