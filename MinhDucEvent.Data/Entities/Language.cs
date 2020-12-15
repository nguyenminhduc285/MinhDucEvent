using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class Language
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public List<ProductTranslation> ProductTranslations { get; set; }

        public List<CategoryTranslation> CategoryTranslations { get; set; }
        public List<EquipmentCategoryTranslation> EquipmentCategoryTranslations { get; set; }
        public List<ContentTagTranslation> ContentTagTranslations { get; set; }
        public List<ContentTranslation> ContentTranslations { get; set; }
        public List<EquipmentTranslation> EquipmentTranslations { get; set; }
        public List<AboutTranslation> AboutTranslations { get; set; }
    }
}