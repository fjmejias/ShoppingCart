using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Library.Model;

namespace ShoppingCart.Library
{
    public interface IShoppingCartDomain
    {
        IList<Item> GetAllItems();
        IList<Item> GetBaksetItems(Basket basket);
        Shopper GetShopper(string name);
        Item GetItem(int itemToAdd);
        Basket GetLatestShopperBasketOrNew(Shopper shopper);
        Basket AddItem(Basket basket, Item item);
        void UpdateBasket(Basket basket);
    }
}
