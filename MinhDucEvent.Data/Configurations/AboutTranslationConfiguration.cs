using MinhDucEvent.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Configurations
{
    public class AboutTranslationConfiguration : IEntityTypeConfiguration<AboutTranslation>
    {
        public void Configure(EntityTypeBuilder<AboutTranslation> builder)
        {
            builder.ToTable("AboutTranslations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).HasMaxLength(200);

            builder.Property(x => x.Metatitle).HasMaxLength(200);
            builder.Property(x => x.Image).HasMaxLength(200);
            builder.Property(x => x.Image).HasMaxLength(200);
            builder.Property(x => x.MetaKeywords).HasMaxLength(200);
            builder.Property(x => x.Details).HasMaxLength(500);
            builder.Property(x => x.Description).HasMaxLength(200);

            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);
            builder.HasOne(x => x.Language).WithMany(x => x.AboutTranslations).HasForeignKey(x => x.LanguageId);

            builder.HasOne(x => x.About).WithMany(x => x.AboutTranslations).HasForeignKey(x => x.AboutId);
        }
    }
}