﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MinhDucEvent.Application.Common;
using MinhDucEvent.Data.EF;
using MinhDucEvent.Data.Entities;
using MinhDucEvent.Utilities.Constants;
using MinhDucEvent.Utilities.Exceptions;
using MinhDucEvent.ViewModels.Catalog.EquipmentImages;
using MinhDucEvent.ViewModels.Catalog.Equipments;
using MinhDucEvent.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MinhDucEvent.Application.Catalog.Equipments
{
    public class EquipmentService : IEquipmentService
    {
        private readonly MinhDucEventDbContext _context;
        private readonly IStorageService _storageService;

        public EquipmentService(MinhDucEventDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> AddImage(int equipmentId, EquipmentImageCreateRequest request)
        {
            var equipmentImage = new EquipmentImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                EquipmentId = equipmentId,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)
            {
                equipmentImage.ImagePath = await this.SaveFile(request.ImageFile);
                equipmentImage.FileSize = request.ImageFile.Length;
            }
            _context.EquipmentImages.Add(equipmentImage);
            await _context.SaveChangesAsync();
            return equipmentImage.Id;
        }

        public async Task<int> Create(EquipmentCreateRequest request)
        {
            var languages = _context.Languages;
            var translations = new List<EquipmentTranslation>();
            foreach (var language in languages)
            {
                if (language.Id == request.LanguageId)
                {
                    translations.Add(new EquipmentTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    });
                }
                else
                {
                    translations.Add(new EquipmentTranslation()
                    {
                        Name = SystemConstants.NA,
                        Description = SystemConstants.NA,
                        SeoAlias = SystemConstants.NA,
                        LanguageId = language.Id
                    });
                }
            }
            var equipment = new Equipment()
            {
                Stock = request.Stock,
                DateCreated = DateTime.Now,
                EquipmentTranslations = translations
            };
            //Save image
            if (request.ThumbnailImage != null)
            {
                equipment.EquipmentImages = new List<EquipmentImage>()
                {
                    new EquipmentImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Equipments.Add(equipment);
            await _context.SaveChangesAsync();
            return equipment.Id;
        }

        public async Task<int> Delete(int equipmentId)
        {
            var Equipment = await _context.Equipments.FindAsync(equipmentId);
            if (Equipment == null) throw new MinhDucEventException($"Cannot find a Equipment: {equipmentId}");

            var images = _context.EquipmentImages.Where(i => i.EquipmentId == equipmentId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Equipments.Remove(Equipment);

            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<EquipmentVm>> GetAllPaging(GetManageEquipmentPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Equipments
                        join pt in _context.EquipmentTranslations on p.Id equals pt.EquipmentId
                        join pic in _context.EquipmentInCategories on p.Id equals pic.EquipmentId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _context.Categories on pic.EquipmentCategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        join pi in _context.EquipmentImages on p.Id equals pi.EquipmentId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        where pt.LanguageId == request.LanguageId && pi.IsDefault == true
                        select new { p, pt, pic, pi };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));

            if (request.CategoryId != null && request.CategoryId != 0)
            {
                query = query.Where(p => p.pic.EquipmentCategoryId == request.CategoryId);
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new EquipmentVm()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<EquipmentVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<EquipmentVm> GetById(int equipmentId, string languageId)
        {
            var Equipment = await _context.Equipments.FindAsync(equipmentId);
            var EquipmentTranslation = await _context.EquipmentTranslations.FirstOrDefaultAsync(x => x.EquipmentId == equipmentId
            && x.LanguageId == languageId);

            var categories = await (from c in _context.Categories
                                    join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                                    join pic in _context.EquipmentInCategories on c.Id equals pic.EquipmentCategoryId
                                    where pic.EquipmentId == equipmentId && ct.LanguageId == languageId
                                    select ct.Name).ToListAsync();

            var image = await _context.EquipmentImages.Where(x => x.EquipmentId == equipmentId && x.IsDefault == true).FirstOrDefaultAsync();

            var EquipmentViewModel = new EquipmentVm()
            {
                Id = Equipment.Id,
                DateCreated = Equipment.DateCreated,
                Description = EquipmentTranslation != null ? EquipmentTranslation.Description : null,
                LanguageId = EquipmentTranslation.LanguageId,
                Details = EquipmentTranslation != null ? EquipmentTranslation.Details : null,
                Name = EquipmentTranslation != null ? EquipmentTranslation.Name : null,
                SeoAlias = EquipmentTranslation != null ? EquipmentTranslation.SeoAlias : null,
                SeoDescription = EquipmentTranslation != null ? EquipmentTranslation.SeoDescription : null,
                SeoTitle = EquipmentTranslation != null ? EquipmentTranslation.SeoTitle : null,
                Stock = Equipment.Stock,
                Categories = categories,
                ThumbnailImage = image != null ? image.ImagePath : "no-image.jpg"
            };
            return EquipmentViewModel;
        }

        public async Task<EquipmentImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.EquipmentImages.FindAsync(imageId);
            if (image == null)
                throw new MinhDucEventException($"Cannot find an image with id {imageId}");

            var viewModel = new EquipmentImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
                EquipmentId = image.EquipmentId,
                SortOrder = image.SortOrder
            };
            return viewModel;
        }

        public async Task<List<EquipmentImageViewModel>> GetListImages(int equipmentId)
        {
            return await _context.EquipmentImages.Where(x => x.EquipmentId == equipmentId)
                .Select(i => new EquipmentImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    IsDefault = i.IsDefault,
                    EquipmentId = i.EquipmentId,
                    SortOrder = i.SortOrder
                }).ToListAsync();
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var equipmentImage = await _context.EquipmentImages.FindAsync(imageId);
            if (equipmentImage == null)
                throw new MinhDucEventException($"Cannot find an image with id {imageId}");
            _context.EquipmentImages.Remove(equipmentImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(EquipmentUpdateRequest request)
        {
            var Equipment = await _context.Equipments.FindAsync(request.Id);
            var EquipmentTranslations = await _context.EquipmentTranslations.FirstOrDefaultAsync(x => x.EquipmentId == request.Id
            && x.LanguageId == request.LanguageId);

            if (Equipment == null || EquipmentTranslations == null) throw new MinhDucEventException($"Cannot find a Equipment with id: {request.Id}");

            EquipmentTranslations.Name = request.Name;
            EquipmentTranslations.SeoAlias = request.SeoAlias;
            EquipmentTranslations.SeoDescription = request.SeoDescription;
            EquipmentTranslations.SeoTitle = request.SeoTitle;
            EquipmentTranslations.Description = request.Description;
            EquipmentTranslations.Details = request.Details;

            //Save image
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.EquipmentImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.EquipmentId == request.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.EquipmentImages.Update(thumbnailImage);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, EquipmentImageUpdateRequest request)
        {
            var EquipmentImage = await _context.EquipmentImages.FindAsync(imageId);
            if (EquipmentImage == null)
                throw new MinhDucEventException($"Cannot find an image with id {imageId}");

            if (request.ImageFile != null)
            {
                EquipmentImage.ImagePath = await this.SaveFile(request.ImageFile);
                EquipmentImage.FileSize = request.ImageFile.Length;
            }
            _context.EquipmentImages.Update(EquipmentImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStock(int equipmentId, int addedQuantity)
        {
            var Equipment = await _context.Equipments.FindAsync(equipmentId);
            if (Equipment == null) throw new MinhDucEventException($"Cannot find a Equipment with id: {equipmentId}");
            Equipment.Stock += addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + SystemConstants.USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }
}