﻿using Microsoft.AspNetCore.Http;

namespace MinhDucEvent.ViewModels.Catalog.Equipments
{
    public class EquipmentUpdateRequest
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }

        public IFormFile ThumbnailImage { set; get; }

        public bool? IsFeature { get; set; }
    }
}