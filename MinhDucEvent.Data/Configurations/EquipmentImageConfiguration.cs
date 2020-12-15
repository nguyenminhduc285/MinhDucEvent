using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhDucEvent.Data.Entities;

namespace eShopSolution.Data.Configurations
{
    public class EquipmentImageConfiguration : IEntityTypeConfiguration<EquipmentImage>
    {
        public void Configure(EntityTypeBuilder<EquipmentImage> builder)
        {
            builder.ToTable("EquipmentImages");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired(true);

            builder.Property(x => x.Caption).HasMaxLength(200).IsRequired(false);

            builder.HasOne(x => x.Equipment).WithMany(x => x.EquipmentImages).HasForeignKey(x => x.EquipmentId);
        }
    }
}