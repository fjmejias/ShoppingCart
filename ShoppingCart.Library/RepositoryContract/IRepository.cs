using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Library.Model;

namespace ShoppingCart.Library.RepositoryContract
{
    public interface IRepository
    {
        IList<Item> GetAllItems();
        IList<Item> GetBasketItems(int basketId);
        Basket GetBasket(int basketId);
        Shopper GetShopper(string name);
        Basket GetLatestShopperBasket(int shopperId);
        Item GetItem(int id);
        Basket UpdateBasket(Basket basket);
        Basket GetNewBasket(int shopperId);
        Item UpdateItem(Item item);
    }
}
