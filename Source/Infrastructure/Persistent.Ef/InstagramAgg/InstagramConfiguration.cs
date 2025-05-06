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
            builder.ToTable("Instagram", "Instagram");
            builder.OwnsMany(b => b.Stories, option =>
            {
                option.ToTable("Stories", "Instagram");
                option.HasIndex(b => b.storyId);
                option.OwnsOne(b => b.Image, options =>
                {
                    options.ToTable("Images", "dbo");
                    options.HasIndex(b => b.Id);
                });
                option.OwnsOne(b => b.Video, options =>
                {
                    options.ToTable("Videos", "dbo");
                    options.HasIndex(b => b.Id);
                });
            });
            builder.OwnsMany(b => b.Posts, option =>
            {
                option.ToTable("Posts", "Instagram");
                option.HasIndex(b => b.Id);
                option.OwnsMany(b => b.Images, options =>
                {
                    options.ToTable("Images", "Instagram");
                    options.HasIndex(b => b.Id);
                });
                option.OwnsMany(b => b.Videos, options =>
                {
                    options.ToTable("Videos", "Instagram");
                    options.HasIndex(b => b.Id);
                });
            });
        }
    }
}
