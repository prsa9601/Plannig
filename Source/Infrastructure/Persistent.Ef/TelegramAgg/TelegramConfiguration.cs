using Domain.SocialMediaAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.TelegramAgg
{
    internal class TelegramConfiguration : IEntityTypeConfiguration<Domain.SocialMediaAgg.TelegramAgg.Telegram>
    {
        public void Configure(EntityTypeBuilder<Domain.SocialMediaAgg.TelegramAgg.Telegram> builder)
        {
            builder.ToTable("Telegrams", "telegram");
            builder.OwnsMany(b => b.Posts, option =>
            {
                option.ToTable("Posts", "telegram");
                option.HasIndex(b => b.postId);
                option.OwnsMany(b => b.Images, options =>
                {
                    options.ToTable("Images", "telegram");
                    options.HasIndex(b => b.Id);
                });
                option.OwnsMany(b => b.Videos, options =>
                {
                    options.ToTable("Videos", "telegram");
                    options.HasIndex(b => b.Id);
                });
            });
        }
    }
}
