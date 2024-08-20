using E_commerce.Models;
using System.Collections.Generic;

namespace E_commerce.ViewModel
{
    public class PurchaseViewModel
    {
        public Purchase purchase { get; set; }
        public List<Product> products { get; set; }
        public List<PaymentItem> paymentItems { get; set; }

    }
}
