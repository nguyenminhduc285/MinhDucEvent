using MinhDucEvent.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class EquipmentTranslationConfiguration : IEntityTypeConfiguration<EquipmentTranslation>
    {
        public void Configure(EntityTypeBuilder<EquipmentTranslation> builder)
        {
            builder.ToTable("EquipmentTranslations");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

            builder.Property(x => x.SeoAlias).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Details).HasMaxLength(500);

            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);

            builder.HasOne(x => x.Language).WithMany(x => x.EquipmentTranslations).HasForeignKey(x => x.LanguageId);

            builder.HasOne(x => x.Equipment).WithMany(x => x.EquipmentTranslations).HasForeignKey(x => x.EquipmentId);
        }
    }
}