using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.Ef.CategoryAgg
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Domain.CategoryAgg.Category>
    {
        public void Configure(EntityTypeBuilder<Domain.CategoryAgg.Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(b => b.Slug).IsUnique();

            builder.Property(b => b.Slug)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(b => b.Title).IsUnicode()
                .IsRequired();
            builder.OwnsOne(b => b.SeoData, config =>
            {
                config.Property(b => b.MetaDescription).IsUnicode()
                    .HasMaxLength(500)
                    .HasColumnName("MetaDescription");

                config.Property(b => b.MetaTitle).IsUnicode()
                    .HasMaxLength(500)
                    .HasColumnName("MetaTitle");

                config.Property(b => b.MetaKeyWords).IsUnicode()
                    .HasMaxLength(500)
                    .HasColumnName("MetaKeyWords");

                config.Property(b => b.IndexPage)
                    .HasColumnName("IndexPage");

                config.Property(b => b.Canonical)
                    .HasMaxLength(500)
                    .HasColumnName("Canonical");

                config.Property(b => b.Schema)
                    .HasColumnName("Schema");
            });
        }
    }
}
