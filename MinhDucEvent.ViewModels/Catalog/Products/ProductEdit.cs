using System.Collections.Generic;
using MinhDucEvent.ViewModels.Catalog.Equipments;

namespace MinhDucEvent.ViewModels.Catalog.Products
{
    public class ProductEdit
    {
        public int Id { set; get; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }

        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        public bool? IsFeatured { get; set; }
        public string ThumbnailImage { get; set; }
        
        public List<EquipmentVm> eqm { get; set; } = new List<EquipmentVm>();
    }
}