using System.Collections.Generic;

namespace ShoppingCart.Library.Model
{
    public class Shopper
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Basket> Baskets { get; set; }
    }
}
