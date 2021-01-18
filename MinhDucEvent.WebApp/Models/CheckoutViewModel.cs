using MinhDucEvent.ViewModels.Sales;
using System.Collections.Generic;

namespace MinhDucEvent.WebApp.Models
{
    public class CheckoutViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }

        public CheckoutRequest CheckoutModel { get; set; }
    }
}