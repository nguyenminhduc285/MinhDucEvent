using Microsoft.EntityFrameworkCore;
using MinhDucEvent.Data.EF;
using MinhDucEvent.ViewModels.Catalog.EquipmentCategories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhDucEvent.Application.Catalog.Categories
{
    public class EquipmentCategoryService : IEquipmentCategoryService
    {
        private readonly MinhDucEventDbContext _context;

        public EquipmentCategoryService(MinhDucEventDbContext context)
        {
            _context = context;
        }

        public async Task<List<EquipmentCategoryVm>> GetAll(string languageId)
        {
            var query = from ec in _context.EquipmentCategories
                        join ect in _context.EquipmentCategoryTranslations on ec.Id equals ect.EquipmentCategoryId
                        where ect.LanguageId == languageId
                        select new { ec, ect };
            int totalRow = await query.CountAsync();

            return await query.Select(x => new EquipmentCategoryVm()
            {
                Id = x.ec.Id,
                Name = x.ect.Name,
                ParentId = x.ec.ParentId
            }).ToListAsync();
        }

        public async Task<EquipmentCategoryVm> GetById(string languageId, int id)
        {
            var query = from c in _context.EquipmentCategories
                        join ct in _context.EquipmentCategoryTranslations on c.Id equals ct.EquipmentCategoryId
                        where ct.LanguageId == languageId && c.Id == id
                        select new { c, ct };
            return await query.Select(x => new EquipmentCategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                ParentId = x.c.ParentId
            }).FirstOrDefaultAsync();
        }
    }
}