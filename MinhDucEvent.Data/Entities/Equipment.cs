using System;
using System.Collections.Generic;

namespace MinhDucEvent.Data.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public DateTime DateCreated { get; set; }
        public string Image { get; set; }
        public List<EquipmentTranslation> EquipmentTranslations { get; set; }

        public List<EquipmentInCategory> EquipmentInCategories { get; set; }

        public List<ProductDetails> ProductDetails { get; set; }

        public List<EquipmentImage> EquipmentImages { get; set; }
    }
}