using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class ContentTranslation
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string LanguageId { get; set; }
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescriptions { get; set; }

        public Content Content { get; set; }

        public Language Language { get; set; }
    }
}