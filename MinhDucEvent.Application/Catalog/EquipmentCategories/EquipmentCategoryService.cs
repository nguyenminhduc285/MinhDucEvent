using Microsoft.EntityFrameworkCore;
using MinhDucEvent.Application.Common;
using MinhDucEvent.Data.EF;
using MinhDucEvent.Data.Entities;
using MinhDucEvent.Utilities.Constants;
using MinhDucEvent.Utilities.Exceptions;
using MinhDucEvent.ViewModels.Catalog.EquipmentCategories;
using MinhDucEvent.ViewModels.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhDucEvent.Application.Catalog.Categories
{
    public class EquipmentCategoryService : IEquipmentCategoryService
    {
        private readonly MinhDucEventDbContext _context;
        private readonly IStorageService _storageService;

        public EquipmentCategoryService(MinhDucEventDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<PagedResult<EquipmentCategoryVm>> GetAllPaging(GetManageEqCaPagingRequest request)
        {
            //1. select join
            var query = from eqca in _context.EquipmentCategories
                        join eqcatran in _context.EquipmentCategoryTranslations on eqca.Id equals eqcatran.EquipmentCategoryId
                        where eqcatran.LanguageId == request.LanguageId
                        select new { eqca, eqcatran };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.eqcatran.Name.Contains(request.Keyword));
            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new EquipmentCategoryVm()
                {
                    Id = x.eqca.Id,
                    Name = x.eqcatran.Name,
                    SeoAlias = x.eqcatran.SeoAlias,
                    SeoDescription = x.eqcatran.SeoDescription,
                    SeoTitle = x.eqcatran.SeoTitle,
                    ParentId = x.eqca.ParentId,
                    SortOrder = x.eqca.SortOrder,
                    Status = x.eqca.Status
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<EquipmentCategoryVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<int> Create(EquipmentCategoryCreateRequest request)
        {
            var languages = _context.Languages;
            var translations = new List<EquipmentCategoryTranslation>();
            foreach (var language in languages)
            {
                if (language.Id == request.LanguageId)
                {
                    translations.Add(new EquipmentCategoryTranslation()
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
                    translations.Add(new EquipmentCategoryTranslation()
                    {
                        Name = SystemConstants.NA,
                        SeoDescription = SystemConstants.NA,
                        SeoAlias = SystemConstants.NA,
                        SeoTitle = SystemConstants.NA,
                        LanguageId = language.Id
                    });
                }
            }
            var epcategory = new EquipmentCategory()
            {
                EquipmentCategoryTranslations = translations,
                SortOrder = request.SortOrder,
                ParentId = request.ParentId,
                Status = request.Status,
            };
            _context.EquipmentCategories.Add(epcategory);
            await _context.SaveChangesAsync();
            return epcategory.Id;
        }

        public async Task<int> Delete(int equipmentcaId)
        {
            var Equipmentca = await _context.EquipmentCategories.FindAsync(equipmentcaId);
            if (Equipmentca == null) throw new MinhDucEventException($"Cannot find a Equipment: {equipmentcaId}");

            _context.EquipmentCategories.Remove(Equipmentca);

            return await _context.SaveChangesAsync();
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
                SeoAlias = x.ct.SeoAlias,
                SeoDescription = x.ct.SeoDescription,
                SeoTitle = x.ct.SeoTitle
            }).FirstOrDefaultAsync();
        }

        public async Task<int> Update(EquipmentCategoryUpdateRequest request)
        {
            var eqca = await _context.EquipmentCategories.FindAsync(request.Id);
            var eqcaTrans = await _context.EquipmentCategoryTranslations.FirstOrDefaultAsync(x => x.EquipmentCategoryId == request.Id
            && x.LanguageId == request.LanguageId);

            if (eqca == null || eqcaTrans == null)
                throw new MinhDucEventException($"Cannot find a EquipmentCategory with id: {request.Id}");
            eqcaTrans.Name = request.Name;
            eqcaTrans.SeoAlias = request.SeoAlias;
            eqcaTrans.SeoDescription = request.SeoDescription;
            eqcaTrans.SeoTitle = request.SeoTitle;

            return await _context.SaveChangesAsync();
        }
    }
}