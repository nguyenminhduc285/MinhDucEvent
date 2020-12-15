using System;

namespace MinhDucEvent.ViewModels.Catalog.EquipmentImages
{
    public class EquipmentImageViewModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public bool IsDefault { get; set; }
        public DateTime DateCreated { get; set; }
        public int SortOrder { get; set; }
        public long FileSize { get; set; }
    }
}