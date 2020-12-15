using MinhDucEvent.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    internal class EquipmentInCategoryConfiguration : IEntityTypeConfiguration<EquipmentInCategory>
    {
        public void Configure(EntityTypeBuilder<EquipmentInCategory> builder)
        {
            builder.HasKey(t => new { t.EquipmentCategoryId, t.EquipmentId });

            builder.ToTable("EquipmentInCategories");

            builder.HasOne(t => t.Equipment).WithMany(pc => pc.EquipmentInCategories)
                .HasForeignKey(pc => pc.EquipmentId);

            builder.HasOne(t => t.EquipmentCategory).WithMany(pc => pc.EquipmentInCategories)
              .HasForeignKey(pc => pc.EquipmentCategoryId);
        }
    }
}