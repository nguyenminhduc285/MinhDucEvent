using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class EquipmentCategoryTranslation
    {
        public int Id { get; set; }
        public int EquipmentCategoryId { get; set; }
        public string Name { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string SeoAlias { get; set; }
        public string LanguageId { get; set; }
        public EquipmentCategory EquipmentCategory { get; set; }

        public Language Language { get; set; }
    }
}