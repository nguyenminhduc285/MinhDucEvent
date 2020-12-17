using MinhDucEvent.Data.Entities;
using MinhDucEvent.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
               new AppConfig() { Key = "HomeTitle", Value = "This is home page of MinhDucEvent" },
               new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of MinhDucEvent" },
               new AppConfig() { Key = "HomeDescription", Value = "This is description of MinhDucEvent" }
               );
            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "vi", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "en", Name = "English", IsDefault = false });

            modelBuilder.Entity<EquipmentCategory>().HasData(
                new EquipmentCategory()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active,
                },
                 new EquipmentCategory()
                 {
                     Id = 2,
                     IsShowOnHome = true,
                     ParentId = null,
                     SortOrder = 2,
                     Status = Status.Active
                 });

            modelBuilder.Entity<EquipmentCategoryTranslation>().HasData(
                  new EquipmentCategoryTranslation() { Id = 1, EquipmentCategoryId = 1, Name = "Loa", LanguageId = "vi", SeoAlias = "ao-nam", SeoDescription = "Sản phẩm áo thời trang nam", SeoTitle = "Sản phẩm áo thời trang nam" },
                  new EquipmentCategoryTranslation() { Id = 2, EquipmentCategoryId = 1, Name = "Speaker", LanguageId = "en", SeoAlias = "men-shirt", SeoDescription = "The shirt products for men", SeoTitle = "The shirt products for men" },
                  new EquipmentCategoryTranslation() { Id = 3, EquipmentCategoryId = 2, Name = "Den", LanguageId = "vi", SeoAlias = "ao-nu", SeoDescription = "Sản phẩm áo thời trang nữ", SeoTitle = "Sản phẩm áo thời trang women" },
                  new EquipmentCategoryTranslation() { Id = 4, EquipmentCategoryId = 2, Name = "Light", LanguageId = "en", SeoAlias = "women-shirt", SeoDescription = "The shirt products for women", SeoTitle = "The shirt products for women" }
                    );

            modelBuilder.Entity<Equipment>().HasData(
           new Equipment()
           {
               Id = 1,
               DateCreated = DateTime.Now,
               Stock = 12,
           },
           new Equipment()
           {
               Id = 2,
               DateCreated = DateTime.Now,
               Stock = 12,
           });

            modelBuilder.Entity<EquipmentTranslation>().HasData(
                 new EquipmentTranslation()
                 {
                     Id = 1,
                     EquipmentId = 1,
                     Name = "Loa",
                     LanguageId = "vi",
                     SeoAlias = "Loa",
                     SeoDescription = "Loa",
                     SeoTitle = "Loa",
                     Details = "Loa",
                     Description = "Loa"
                 },
                    new EquipmentTranslation()
                    {
                        Id = 2,
                        EquipmentId = 1,
                        Name = "Speaker",
                        LanguageId = "en",
                        SeoAlias = "Speaker",
                        SeoDescription = "Speaker",
                        SeoTitle = "Speaker",
                        Details = "Speaker",
                        Description = "Speaker"
                    }, new EquipmentTranslation()
                    {
                        Id = 3,
                        EquipmentId = 2,
                        Name = "Den",
                        LanguageId = "vi",
                        SeoAlias = "Den",
                        SeoDescription = "Den",
                        SeoTitle = "Den",
                        Details = "Den",
                        Description = "Den"
                    }, new EquipmentTranslation()
                    {
                        Id = 4,
                        EquipmentId = 2,
                        Name = "Lighting",
                        LanguageId = "en",
                        SeoAlias = "Lighting",
                        SeoDescription = "Lighting",
                        SeoTitle = "Lighting",
                        Details = "Lighting",
                        Description = "Lighting"
                    });

            modelBuilder.Entity<EquipmentInCategory>().HasData(
                new EquipmentInCategory() { EquipmentId = 1, EquipmentCategoryId = 1 },
                new EquipmentInCategory() { EquipmentId = 2, EquipmentCategoryId = 2 }
                );

            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "tedu.international@gmail.com",
                NormalizedEmail = "tedu.international@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FirstName = "Duc",
                LastName = "Nguyen Minh",
                Dob = new DateTime(1998, 05, 28)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });

            modelBuilder.Entity<Slide>().HasData(
              new Slide() { Id = 1, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 1, Url = "#", Image = "/themes/images/carousel/1.png", Status = Status.Active },
              new Slide() { Id = 2, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 2, Url = "#", Image = "/themes/images/carousel/2.png", Status = Status.Active },
              new Slide() { Id = 3, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 3, Url = "#", Image = "/themes/images/carousel/3.png", Status = Status.Active },
              new Slide() { Id = 4, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 4, Url = "#", Image = "/themes/images/carousel/4.png", Status = Status.Active },
              new Slide() { Id = 5, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 5, Url = "#", Image = "/themes/images/carousel/5.png", Status = Status.Active },
              new Slide() { Id = 6, Name = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 6, Url = "#", Image = "/themes/images/carousel/6.png", Status = Status.Active }
              );
        }
    }
}