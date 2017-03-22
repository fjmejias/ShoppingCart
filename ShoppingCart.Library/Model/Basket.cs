using System;
using System.Collections.Generic;

namespace ShoppingCart.Library.Model
{
    public class Basket
    {
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool? Finished { get; set; }
        public int? ShopperId { get; set; }
        public List<Item> Items { get; set; }
        public Shopper Shopper { get; set; }
    }
}
