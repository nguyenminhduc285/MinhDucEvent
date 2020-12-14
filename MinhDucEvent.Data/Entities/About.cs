using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class About
    {
        public int Id { get; set; }
        public int CreatedDate { get; set; }
        public int Status { get; set; }

        public List<AboutTranslation> AboutTranslations { get; set; }
    }
}