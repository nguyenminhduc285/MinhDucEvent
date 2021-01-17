using MinhDucEvent.Data.EF;
using MinhDucEvent.ViewModels.Catalog.Categories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinhDucEvent.Application.Catalog.Categories;
using System;
using MinhDucEvent.ViewModels.Common;
using MinhDucEvent.Application.Common;
using MinhDucEvent.Data.Entities;
using MinhDucEvent.Utilities.Constants;
using MinhDucEvent.Utilities.Exceptions;

namespace MinhDucEvent.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly MinhDucEventDbContext _context;

        public CategoryService(MinhDucEventDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(CategoryCreateRequest request)
        {
            var languages = _context.Languages;
            var translations = new List<CategoryTranslation>();
            foreach (var language in languages)
            {
                if (language.Id == request.LanguageId)
                {
                    translations.Add(new CategoryTranslation()
                    {
                        Name = request.Name,

                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    });
                }
                else
                {
                    translations.Add(new CategoryTranslation()
                    {
                        Name = SystemConstants.NA,
                        SeoDescription = SystemConstants.NA,
                        SeoAlias = SystemConstants.NA,
                        SeoTitle = SystemConstants.NA,
                        LanguageId = language.Id
                    });
                }
            }
            var category = new Category()
            {
                CategoryTranslations = translations,
                SortOrder = request.SortOrder,
                ParentId = request.ParentId,
                Status = request.Status,
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }

        public async Task<int> Delete(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) throw new MinhDucEventException($"Cannot find a Category: {categoryId}");

            _context.Categories.Remove(category);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<CategoryVm>> GetAll(string languageId)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new { c, ct };

            var lct = _context.CategoryTranslations.Where(x => x.Id > 0).ToList();
            var lc = _context.Categories.Where(x => x.Id > 0).ToList();
            Console.WriteLine(lct);
            Console.WriteLine(lc);
            return await query.Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                ParentId = x.c.ParentId
            }).ToListAsync();
        }

        public async Task<PagedResult<CategoryVm>> GetAllPaging(GetManageCategoryPagingRequest request)
        {
            //1. select join
            var query = from ca in _context.Categories
                        join catran in _context.CategoryTranslations on ca.Id equals catran.CategoryId
                        where catran.LanguageId == request.LanguageId
                        select new { ca, catran };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.catran.Name.Contains(request.Keyword));
            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CategoryVm()
                {
                    Id = x.ca.Id,
                    Name = x.catran.Name,
                    SeoAlias = x.catran.SeoAlias,
                    SeoDescription = x.catran.SeoDescription,
                    SeoTitle = x.catran.SeoTitle,
                    ParentId = x.ca.ParentId,
                    SortOrder = x.ca.SortOrder,
                    Status = x.ca.Status
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<CategoryVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<CategoryVm> GetById(string languageId, int id)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId && c.Id == id
                        select new { c, ct };
            return await query.Select(x => new CategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                SeoAlias = x.ct.SeoAlias,
                SeoDescription = x.ct.SeoDescription,
                SeoTitle = x.ct.SeoTitle
            }).FirstOrDefaultAsync();
        }

        public async Task<int> Update(CategoryUpdateRequest request)
        {
            var ca = await _context.Categories.FindAsync(request.Id);
            var caTrans = await _context.CategoryTranslations.FirstOrDefaultAsync(x => x.CategoryId == request.Id
            && x.LanguageId == request.LanguageId);

            if (ca == null || caTrans == null)
                throw new MinhDucEventException($"Cannot find a Category with id: {request.Id}");
            caTrans.Name = request.Name;
            caTrans.SeoAlias = request.SeoAlias;
            caTrans.SeoDescription = request.SeoDescription;
            caTrans.SeoTitle = request.SeoTitle;

            return await _context.SaveChangesAsync();
        }
    }
}