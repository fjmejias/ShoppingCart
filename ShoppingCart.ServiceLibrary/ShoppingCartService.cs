using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using ShoppingCart.Library.Model;

namespace ShoppingCart.ServiceLibrary
{
    public class ShoppingCartService : IShoppingCartService
    {
        static readonly ILog Logger = LogManager.GetLogger("ShoppingCart.ServiceLibrary");

        public Library.IShoppingCartDomain ShoppingCartDomain { get; set; }

        public ShoppingCartService(Library.IShoppingCartDomain shoppingCartDomain)
        {
            try
            {
                ShoppingCartDomain = shoppingCartDomain;
            }
            catch (Exception ex)
            {
                Logger.Error("ShoppingCartService : " + ex.InnerException);
                throw ex;
            }
        }


        public IList<Library.Model.Item> GetAllItems()
        {
            IList<Item> listItems = null;

            try
            {
                listItems = ShoppingCartDomain.GetAllItems();
            }
            catch (Exception ex)
            {
                Logger.Error("GetAllItems : " + ex.InnerException);
                throw ex;
            }

            return listItems;
        }

        public Basket AddItemToBasket(string shopperName, int itemToAdd)
        {
            Basket basket = null;

            try
            {
                var shopper = ShoppingCartDomain.GetShopper(shopperName);

                if (shopper != null)
                {
                    basket = ShoppingCartDomain.GetLatestShopperBasketOrNew(shopper);
                    var item = ShoppingCartDomain.GetItem(itemToAdd);

                    if (item.Stock > 0)
                    {
                        item.Stock = item.Stock - 1;
                        basket = ShoppingCartDomain.AddItem(basket, item);
                    }
                    else
                    {
                        throw new Exception(string.Format("The item {0} has no stock.", itemToAdd));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("AddItemToBasket : " + ex.InnerException);
                throw ex;
            }

            return basket;
        }
    }
}
