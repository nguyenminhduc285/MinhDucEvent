using MinhDucEvent.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class ContentTagTranslationConfiguration : IEntityTypeConfiguration<ContentTagTranslation>
    {
        public void Configure(EntityTypeBuilder<ContentTagTranslation> builder)
        {
            builder.ToTable("ContentTagTranslations");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);

            builder.Property(x => x.TagName).IsRequired().HasMaxLength(200);

            builder.HasOne(x => x.Language).WithMany(x => x.ContentTagTranslations).HasForeignKey(x => x.LanguageId);

            builder.HasOne(x => x.ContentTag).WithMany(x => x.ContentTagTranslations).HasForeignKey(x => x.ContentTagId);
        }
    }
}