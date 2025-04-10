using Domain.BlogAgg;
using Domain.EventAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.Ef.BlogAgg
{
    internal class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blog", "Blog");
            builder.Property(b=>b.Slug).IsUnicode(true);
            builder.Property(b=>b.Description).IsUnicode(true);
            builder.Property(b=>b.Title).IsUnicode(true);
            builder.HasIndex(b => b.Slug).IsUnique();
            builder.OwnsOne(b => b.SeoData, config =>
            {
                config.Property(b => b.MetaDescription)
                    .HasMaxLength(500)
                    .HasColumnName("MetaDescription");

                config.Property(b => b.MetaTitle)
                    .HasMaxLength(500)
                    .HasColumnName("MetaTitle");

                config.Property(b => b.MetaKeyWords)
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
