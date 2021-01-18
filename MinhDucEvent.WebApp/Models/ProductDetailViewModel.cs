using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinhDucEvent.ViewModels.Catalog.Categories;
using MinhDucEvent.ViewModels.Catalog.ProductImages;
using MinhDucEvent.ViewModels.Catalog.Products;

namespace eShopSolution.WebApp.Models
{
    public class ProductDetailViewModel
    {
        public CategoryVm Category { get; set; }

        public ProductVm Product { get; set; }

        public List<ProductVm> RelatedProducts { get; set; }

        public List<ProductImageViewModel> ProductImages { get; set; }
    }
}