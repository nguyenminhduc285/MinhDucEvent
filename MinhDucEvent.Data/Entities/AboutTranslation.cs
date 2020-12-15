using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class AboutTranslation
    {
        public int Id { get; set; }
        public int AboutId { get; set; }
        public string Name { get; set; }
        public string Metatitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Details { get; set; }
        public string MetaKeywords { get; set; }
        public string LanguageId { set; get; }

        public Language Language { get; set; }
        public About About { get; set; }
    }
}