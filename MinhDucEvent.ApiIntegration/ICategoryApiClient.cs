using MinhDucEvent.ViewModels.Catalog.Categories;
using MinhDucEvent.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhDucEvent.ApiIntegration
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryVm>> GetAll(string languageId);

        Task<CategoryVm> GetById(string languageId, int id);

        Task<bool> CreateCategory(CategoryCreateRequest request);

        Task<bool> UpdateCategory(CategoryUpdateRequest request);

        Task<bool> DeleteCategory(int id);

        Task<PagedResult<CategoryVm>> GetPagings(GetManageCategoryPagingRequest request);
    }
}