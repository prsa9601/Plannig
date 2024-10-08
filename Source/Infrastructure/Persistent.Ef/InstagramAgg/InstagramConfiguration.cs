using Domain.SocialMediaAgg;
using Domain.SocialMediaAgg.InstagramAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.InstagramAgg
{
    internal class InstagramConfiguration : IEntityTypeConfiguration<Instagram>
    {
        public void Configure(EntityTypeBuilder<Instagram> builder)
        {
            builder.ToTable("Instagram", "instagram");
            builder.OwnsMany(b => b.Stories, option =>
            {
                option.ToTable("Stories", "instagram");
                option.HasIndex(b => b.storyId);
            });
            builder.OwnsMany(b => b.Posts, option =>
            {
                option.ToTable("Posts", "instagram");
                option.HasIndex(b => b.Id);
                option.OwnsMany(b => b.Images, options =>
                {
                    options.ToTable("Images", "instagram");
                    options.HasIndex(b => b.Id);
                });
                option.OwnsMany(b => b.Videos, options =>
                {
                    options.ToTable("Videos", "instagram");
                    options.HasIndex(b => b.Id);
                });
            });
        }
    }
}
