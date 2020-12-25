using MinhDucEvent.ViewModels.Catalog.Equipments;
using MinhDucEvent.ViewModels.Catalog.Products;
using MinhDucEvent.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MinhDucEvent.ApiIntegration
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductVm>> GetPagings(GetManageProductPagingRequest request);

        Task<bool> CreateProduct(ProductCreateRequest request);

        Task<bool> UpdateProduct(ProductUpdateRequest request);

        Task<ProductVm> GetById(int id, string languageId);

        Task<bool> DeleteProduct(int id);
    }
}