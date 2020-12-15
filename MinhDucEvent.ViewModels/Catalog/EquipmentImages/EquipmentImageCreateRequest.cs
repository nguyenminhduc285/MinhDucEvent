﻿using Microsoft.AspNetCore.Http;

namespace MinhDucEvent.ViewModels.Catalog.EquipmentImages
{
    public class EquipmentImageCreateRequest
    {
        public string Caption { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}