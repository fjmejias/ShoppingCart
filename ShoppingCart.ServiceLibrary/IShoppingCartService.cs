using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Library.Model;

namespace ShoppingCart.ServiceLibrary
{
    public interface IShoppingCartService
    {
        IList<Item> GetAllItems();
        Item GetItem(int itemToAdd);
        Basket AddItemToBasket(string shopperName, int itemToAdd);
        Basket GetLatestBasket(string shopperName);
        Basket AddItemsToBasket(string shopperName, Dictionary<int, int> itemsToAdd);
        IList<Item> CheckOutBasket(Basket basket);
    }
}
