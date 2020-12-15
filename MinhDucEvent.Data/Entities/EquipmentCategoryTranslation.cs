using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class EquipmentCategoryTranslation
    {
        public int Id { get; set; }
        public int EquipmentCategoryId { get; set; }
        public int Name { get; set; }
        public int SeoDescription { get; set; }
        public int SeoTitle { get; set; }
        public int SeoAlias { get; set; }
        public string LanguageId { get; set; }
        public EquipmentCategory EquipmentCategory { get; set; }

        public Language Language { get; set; }
    }
}