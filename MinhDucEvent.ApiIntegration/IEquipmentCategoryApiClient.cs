using MinhDucEvent.ViewModels.Catalog.EquipmentCategories;
using MinhDucEvent.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhDucEvent.ApiIntegration
{
    public interface IEquipmentCategoryApiClient
    {
        Task<List<EquipmentCategoryVm>> GetAll(string languageId);

        Task<EquipmentCategoryVm> GetById(string languageId, int id);

        Task<bool> CreateEquipmentCategory(EquipmentCategoryCreateRequest request);

        Task<bool> UpdateEquipmentCategory(EquipmentCategoryUpdateRequest request);

        Task<bool> DeleteEquipmentCategory(int id);

        Task<PagedResult<EquipmentCategoryVm>> GetPagings(GetManageEqCaPagingRequest request);
    }
}