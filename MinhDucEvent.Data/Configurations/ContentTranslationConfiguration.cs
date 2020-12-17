using MinhDucEvent.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Configurations
{
    public class ContentTranslationConfiguration : IEntityTypeConfiguration<ContentTranslation>
    {
        public void Configure(EntityTypeBuilder<ContentTranslation> builder)
        {
            builder.ToTable("ContentTranslations");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.MetaTitle).HasMaxLength(200);
            builder.Property(x => x.MetaDescriptions).HasMaxLength(200);
            builder.Property(x => x.MetaKeywords).HasMaxLength(200);
            builder.Property(x => x.Details).HasColumnType("ntext");

            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);

            builder.HasOne(x => x.Language).WithMany(x => x.ContentTranslations).HasForeignKey(x => x.LanguageId);

            builder.HasOne(x => x.Content).WithMany(x => x.ContentTranslations).HasForeignKey(x => x.ContentId);
        }
    }
}