using MinhDucEvent.ViewModels.Catalog.Categories;
using MinhDucEvent.ViewModels.Catalog.ProductImages;
using MinhDucEvent.ViewModels.Catalog.Products;
using System.Collections.Generic;

namespace MinhDucEvent.WebApp.Models
{
    public class ProductDetailViewModel
    {
        public CategoryVm Category { get; set; }

        public ProductVm Product { get; set; }

        public List<ProductVm> RelatedProducts { get; set; }

        public List<ProductImageViewModel> ProductImages { get; set; }
    }
}