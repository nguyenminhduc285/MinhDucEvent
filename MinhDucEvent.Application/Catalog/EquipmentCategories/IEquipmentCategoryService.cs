using MinhDucEvent.ViewModels.Catalog.EquipmentCategories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhDucEvent.Application.Catalog.Categories
{
    public interface IEquipmentCategoryService
    {
        Task<List<EquipmentCategoryVm>> GetAll(string languageId);

        Task<EquipmentCategoryVm> GetById(string languageId, int id);
    }
}