using System;
using System.Collections.Generic;
using System.Text;

namespace MinhDucEvent.ViewModels.Sales
{
    public class CheckoutRequest
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderDetailVm> OrderDetails { set; get; } = new List<OrderDetailVm>();
    }
}