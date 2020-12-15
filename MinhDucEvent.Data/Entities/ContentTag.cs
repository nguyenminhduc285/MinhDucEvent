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

        public List<ContentTagTranslation> ContentTagTranslations { get; set; }
        public List<ContentInTag> ContentInTags { get; set; }
    }
}