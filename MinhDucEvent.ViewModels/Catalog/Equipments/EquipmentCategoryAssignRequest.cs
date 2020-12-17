using MinhDucEvent.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.ViewModels.Catalog.Equipments
{
    public class EquipmentCategoryAssignRequest
    {
        public int Id { get; set; }
        public List<SelectItem> Categories { get; set; } = new List<SelectItem>();
    }
}