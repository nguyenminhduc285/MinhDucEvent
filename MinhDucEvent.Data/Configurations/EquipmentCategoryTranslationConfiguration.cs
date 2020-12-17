using MinhDucEvent.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MinhDucEvent.Data.Configurations
{
    public class EquipmentCategoryTranslationConfiguration : IEntityTypeConfiguration<EquipmentCategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<EquipmentCategoryTranslation> builder)
        {
            builder.ToTable("EquipmentCategoryTranslations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).HasMaxLength(200);

            builder.Property(x => x.SeoAlias).HasMaxLength(200);

            builder.Property(x => x.SeoDescription).HasMaxLength(500);

            builder.Property(x => x.SeoTitle).HasMaxLength(200);

            builder.Property(x => x.LanguageId).IsUnicode(false).IsRequired().HasMaxLength(5);

            builder.HasOne(x => x.Language).WithMany(x => x.EquipmentCategoryTranslations).HasForeignKey(x => x.LanguageId);

            builder.HasOne(x => x.EquipmentCategory).WithMany(x => x.EquipmentCategoryTranslations).HasForeignKey(x => x.EquipmentCategoryId);
        }
    }
}