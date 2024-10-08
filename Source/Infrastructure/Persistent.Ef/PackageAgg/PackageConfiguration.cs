using Domain.PackageAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.PackageAgg
{
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.ToTable("Package", "dbo");
            builder.Property(i=>i.Title)
                .IsRequired().IsUnicode();
            builder.Property(i=>i.Link)
                .IsRequired();
            builder.Property(i=>i.ImageName)
                .IsRequired();

            builder.OwnsMany(b=>b.Specification , option =>
            {
                option.ToTable("specification", "dbo");
                option.HasIndex(b => b.Id);
                option.HasKey(b=>b.PackageId);
            });

        }
    }
}
