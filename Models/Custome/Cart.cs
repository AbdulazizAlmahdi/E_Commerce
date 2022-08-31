using System.Collections.Generic;

namespace E_commerce.Models.Custome
{
    public class Cart
    {
        private static List<Cart> cartList;
        public static List<Cart> getInstance()
        {
            if (cartList == null)
                cartList = new List<Cart>();
            return cartList;
        }

        public int Id { get; set; }
        public string ImageURL { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
