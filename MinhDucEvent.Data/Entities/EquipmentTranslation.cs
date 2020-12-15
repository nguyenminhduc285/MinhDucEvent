using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class EquipmentTranslation
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int Name { get; set; }
        public int Description { get; set; }
        public int Details { get; set; }
        public int SeoDescription { get; set; }
        public int SeoTitle { get; set; }
        public int SeoAlias { get; set; }
        public string LanguageId { get; set; }
        public Equipment Equipment { get; set; }

        public Language Language { get; set; }
    }
}