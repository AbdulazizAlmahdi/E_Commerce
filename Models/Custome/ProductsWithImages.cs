using System.Collections.Generic;

namespace E_commerce.Models.Custome
{
    public class ProductsWithImages
    {
        public Product product { get; set; }
        public List<ImagesProduct> image { get; set; }

    }
}
