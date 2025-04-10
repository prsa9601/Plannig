using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.Ef.CommentAgg
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Domain.CommentAgg.Comment>
    {
        public void Configure(EntityTypeBuilder<Domain.CommentAgg.Comment> builder)
        {
            builder.ToTable("Comments", "dbo");
            builder.HasIndex(b => b.Id);
            builder.HasIndex(b => b.PostId);
            builder.HasIndex(b => b.UserId);


        }
    }
}