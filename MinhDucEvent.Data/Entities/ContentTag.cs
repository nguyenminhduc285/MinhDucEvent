using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class ContentTag
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public DateTime CreatedDate { get; set; }

        public Content Content { get; set; }

        public List<ContentTagTranslation> ContentTagTranslations { get; set; }
    }
}