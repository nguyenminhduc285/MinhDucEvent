using MinhDucEvent.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class About
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Status Status { get; set; }

        public List<AboutTranslation> AboutTranslations { get; set; }
    }
}