using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class ContentTagTranslation
    {
        public int Id { get; set; }
        public int ContentTagId { get; set; }
        public string LanguageId { get; set; }
        public string TagName { get; set; }
        public ContentTag ContentTag { get; set; }

        public Language Language { get; set; }
    }
}