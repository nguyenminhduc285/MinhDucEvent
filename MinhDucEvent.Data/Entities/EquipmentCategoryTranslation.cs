using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class EquipmentCategoryTranslation
    {
        public int Id { get; set; }
        public int CategoryEquipmentId { get; set; }
        public int Name { get; set; }
        public int SeoDescription { get; set; }
        public int SeoTitle { get; set; }
        public int SaoAlias { get; set; }
        public int LanguageId { get; set; }
        public EquipmentCategory EquipmentCategory { get; set; }

        public Language Language { get; set; }
    }
}