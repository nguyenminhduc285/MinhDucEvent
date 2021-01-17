using MinhDucEvent.ViewModels.Catalog.EquipmentCategories;
using MinhDucEvent.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhDucEvent.Application.Catalog.Categories
{
    public interface IEquipmentCategoryService
    {
        Task<List<EquipmentCategoryVm>> GetAll(string languageId);

        Task<EquipmentCategoryVm> GetById(string languageId, int id);

        Task<int> Create(EquipmentCategoryCreateRequest request);

        Task<int> Update(EquipmentCategoryUpdateRequest request);

        Task<int> Delete(int equipmentcaId);

        Task<PagedResult<EquipmentCategoryVm>> GetAllPaging(GetManageEqCaPagingRequest request);
    }
}