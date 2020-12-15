using MinhDucEvent.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class ContentInTagConfiguration : IEntityTypeConfiguration<ContentInTag>
    {
        public void Configure(EntityTypeBuilder<ContentInTag> builder)
        {
            builder.HasKey(t => new { t.ContentId, t.ContentTagId });

            builder.ToTable("ContentInTags");

            builder.HasOne(t => t.Content).WithMany(pc => pc.ContentInTags)
                .HasForeignKey(pc => pc.ContentId);

            builder.HasOne(t => t.ContentTag).WithMany(pc => pc.ContentInTags)
              .HasForeignKey(pc => pc.ContentTagId);
        }
    }
}