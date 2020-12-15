using eShopSolution.Data.Configurations;
using eShopSolution.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinhDucEvent.Data.Entities;
using System;

namespace MinhDucEvent.Data.EF
{
    public class MinhDucEventDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public MinhDucEventDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using fluent API
            modelBuilder.ApplyConfiguration(new AboutConfiguration());
            modelBuilder.ApplyConfiguration(new AboutTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());

            modelBuilder.ApplyConfiguration(new CartConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryTranslationConfiguration());

            modelBuilder.ApplyConfiguration(new ContactConfiguration());

            modelBuilder.ApplyConfiguration(new ContentConfiguration());

            modelBuilder.ApplyConfiguration(new ContentInTagConfiguration());

            modelBuilder.ApplyConfiguration(new ContentTagConfiguration());

            modelBuilder.ApplyConfiguration(new ContentTagTranslationConfiguration());

            modelBuilder.ApplyConfiguration(new ContentTranslationConfiguration());

            modelBuilder.ApplyConfiguration(new EquipmentCategoryConfiguration());

            modelBuilder.ApplyConfiguration(new EquipmentCategoryTranslationConfiguration());

            modelBuilder.ApplyConfiguration(new EquipmentConfiguration());

            modelBuilder.ApplyConfiguration(new EquipmentInCategoryConfiguration());

            modelBuilder.ApplyConfiguration(new EquipmentTranslationConfiguration());

            modelBuilder.ApplyConfiguration(new LanguageConfiguration());

            modelBuilder.ApplyConfiguration(new OrderConfiguration());

            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());

            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.ApplyConfiguration(new ProductDetailsConfiguration());

            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());

            modelBuilder.ApplyConfiguration(new ProductInCategoryConfiguration());

            modelBuilder.ApplyConfiguration(new ProductTranslationConfiguration());

            modelBuilder.ApplyConfiguration(new PromotionConfiguration());

            modelBuilder.ApplyConfiguration(new SlideConfiguration());

            modelBuilder.ApplyConfiguration(new TransactionConfiguration());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRole").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogin").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            //Data seeding
            modelBuilder.Seed();
        }

        // base.OnModelCreating(modelBuilder);

        public DbSet<Product> Products { get; set; }
        public DbSet<Product> ProductDetails { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<AboutTranslation> AboutTranslations { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentInTag> ContentInTags { get; set; }
        public DbSet<ContentTag> ContentTags { get; set; }
        public DbSet<ContentTagTranslation> ContentTagTranslations { get; set; }
        public DbSet<ContentTranslation> ContentTranslations { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public DbSet<EquipmentCategoryTranslation> EquipmentCategoryTranslations { get; set; }
        public DbSet<EquipmentInCategory> EquipmentInCategories { get; set; }
        public DbSet<EquipmentTranslation> EquipmentTranslations { get; set; }
        public DbSet<EquipmentCategory> Categories { get; set; }
        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<ProductInCategory> ProductInCategories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Slide> Slides { get; set; }
    }
}