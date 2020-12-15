using MinhDucEvent.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class ContentTagConfiguration : IEntityTypeConfiguration<ContentTag>
    {
        public void Configure(EntityTypeBuilder<ContentTag> builder)
        {
            builder.ToTable("ContentTags");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}