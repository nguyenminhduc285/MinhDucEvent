using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MinhDucEvent.ViewModels.Catalog.Equipments;

namespace MinhDucEvent.ViewModels.Catalog.Products
{
    public class ProductCreateRequest
    {
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập tên sản phẩm")]
        public string Name { set; get; }

        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        public bool? IsFeature { get; set; }
        public List<ProductDetailVM> productDetailVMs { get; set; } = new List<ProductDetailVM>();
        public List<EquipmentVm> eqm { get; set; } = new List<EquipmentVm>();
        public IFormFile ThumbnailImage { get; set; }
    }
}