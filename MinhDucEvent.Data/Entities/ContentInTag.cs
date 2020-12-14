using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class ContentInTag
    {
        public int ContentId { get; set; }

        public Content Content { get; set; }

        public int ContentTagId { get; set; }

        public ContentTag ContentTag { get; set; }
    }
}