using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhDucEvent.Data.Entities;

namespace eShopSolution.Data.Configurations
{
    public class ProductDetailsConfiguration : IEntityTypeConfiguration<ProductDetails>
    {
        public void Configure(EntityTypeBuilder<ProductDetails> builder)
        {
            builder.ToTable("ProductDetails");

            builder.HasKey(x => new { x.EquipmentId, x.ProductId });

            builder.HasOne(x => x.Equipment).WithMany(x => x.ProductDetails).HasForeignKey(x => x.EquipmentId);

            builder.HasOne(x => x.Product).WithMany(x => x.ProductDetails).HasForeignKey(x => x.ProductId);
        }
    }
}