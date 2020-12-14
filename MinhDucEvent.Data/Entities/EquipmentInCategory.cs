using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class EquipmentInCategory
    {
        public int EquipmentId { get; set; }

        public Equipment Equipment { get; set; }

        public int EquipmentCategoryId { get; set; }

        public EquipmentCategory EquipmentCategory { get; set; }
    }
}