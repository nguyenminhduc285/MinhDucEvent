using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class EquipmentCategory
    {
        public int Id { get; set; }
        public int SortOrder { get; set; }
        public int IsShowOnHome { get; set; }
        public int ParentId { get; set; }
        public int Status { get; set; }
        public List<EquipmentInCategory> EquipmentInCategories { get; set; }
        public List<EquipmentCategoryTranslation> EquipmentCategoryTranslations { get; set; }
    }
}