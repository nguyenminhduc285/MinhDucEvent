using MinhDucEvent.Data.Entities;
using MinhDucEvent.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEquipment>
    {
        public void Configure(EntityTypeBuilder<CategoryEquipment> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status).HasDefaultValue(Status.Active);
        }
    }
}