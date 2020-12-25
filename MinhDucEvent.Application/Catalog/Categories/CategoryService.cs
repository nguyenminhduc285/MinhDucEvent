using MinhDucEvent.Data.EF;
using MinhDucEvent.ViewModels.Catalog.Categories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinhDucEvent.Application.Catalog.Categories;
using System;

namespace MinhDucEvent.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly MinhDucEventDbContext _context;

        public CategoryService(MinhDucEventDbContext context)
        {
            _context = context;
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
                ParentId = x.c.ParentId
            }).FirstOrDefaultAsync();
        }
    }
}