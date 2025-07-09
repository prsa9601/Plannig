using Domain.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.UserAgg
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users","user");

            builder.Property(b => b.PhoneNumber).IsRequired().HasMaxLength(11);
            builder.Property(b => b.Family).IsRequired(false);
            builder.Property(b => b.Name).IsRequired(false);

            builder.OwnsMany(b => b.friends, option => 
            {
                option.ToTable("friends", "user");
                option.HasIndex(b => b.Id);
                option.Property(b => b.UserFriendId);
                option.Property(b => b.CurrentUserId);
            });

            builder.OwnsMany(b => b.UserNotifications, option => 
            {
                option.ToTable("UserNotifications", "user");
                option.HasIndex(b => b.Id);
                //option.OwnsMany(b => b.UserIds);
            });

            builder.OwnsMany(b => b.RequestBox, option => 
            {
                option.ToTable("RequestBox", "user");
                option.HasIndex(b => b.Id);
                
            });

            builder.OwnsOne(b => b.Avatar, option =>
            {
                option.ToTable("Avatar", "user");
                option.HasIndex(b => b.Id);
            });

            builder.OwnsMany(b => b.userEvents, option => 
            {
                option.ToTable("userEvents", "user");
                option.HasIndex(b => b.UserId);
            });
            builder.OwnsMany(b => b.UserPackages, option => 
            {
                option.ToTable("userPackages", "user");
                option.HasIndex(b => b.UserId);
            });
            builder.OwnsMany(b => b.friends, option => 
            {
                option.ToTable("friends", "user");
                option.HasIndex(b => b.CurrentUserId);
                //option.OwnsOne(a => a.AvatarFriend, item =>
                //{
                //    item.ToTable("AvatarFriend", "user");
                //    item.HasIndex(b => b.UserId);
                //});


            });

            builder.OwnsMany(b => b.Tokens, option =>
            {
                option.ToTable("Tokens", "user");
                option.HasKey(b => b.Id);

                option.Property(b => b.HashJwtToken)
                    .IsRequired()
                    .HasMaxLength(250);

                option.Property(b => b.HashRefreshToken)
                    .IsRequired()
                    .HasMaxLength(250);

                option.Property(b => b.Device)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            builder.OwnsMany(b => b.Roles, option =>
            {
                option.ToTable("Roles", "user");
                option.HasIndex(b => b.UserId);
            });
        }
    }
}
