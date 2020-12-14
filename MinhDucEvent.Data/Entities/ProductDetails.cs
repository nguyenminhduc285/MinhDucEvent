using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.Data.Entities
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; }
        public Equipment Equipment { get; set; }
    }
}