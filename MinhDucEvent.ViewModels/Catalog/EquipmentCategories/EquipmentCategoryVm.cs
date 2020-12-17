using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.ViewModels.Catalog.EquipmentCategories
{
    public class EquipmentCategoryVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }
    }
}