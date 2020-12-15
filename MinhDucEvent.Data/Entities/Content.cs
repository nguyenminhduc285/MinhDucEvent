using MinhDucEvent.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class Content
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int OrderId { get; set; }
        public string Image { get; set; }
        public Status Status { get; set; }

        public List<ContentTranslation> ContentTranslations { get; set; }
        public List<ContentInTag> ContentInTags { get; set; }
    }
}