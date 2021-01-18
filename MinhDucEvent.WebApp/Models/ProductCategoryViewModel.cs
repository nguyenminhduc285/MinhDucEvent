using MinhDucEvent.ViewModels.Catalog.Categories;
using MinhDucEvent.ViewModels.Catalog.Products;
using MinhDucEvent.ViewModels.Common;

namespace MinhDucEvent.WebApp.Models
{
    public class ProductCategoryViewModel
    {
        public CategoryVm Category { get; set; }

        public PagedResult<ProductVm> Products { get; set; }
    }
}