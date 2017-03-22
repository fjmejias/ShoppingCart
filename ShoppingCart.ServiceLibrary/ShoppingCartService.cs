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


        public IList<Item> GetAllItems()
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

        public Item GetItem(int itemToAdd)
        {
            Item item = null;

            try
            {
                item = ShoppingCartDomain.GetItem(itemToAdd);
            }
            catch (Exception ex)
            {
                Logger.Error("GetItem : " + ex.InnerException);
                throw ex;
            }

            return item;
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
                        item.Quantity = item.Quantity == null ? 1 : item.Quantity++;
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

        public Basket GetLatestBasket(string shopperName)
        {
            Basket basket = null;

            try
            {
                var shopper = ShoppingCartDomain.GetShopper(shopperName);

                if (shopper != null)
                {
                    basket = ShoppingCartDomain.GetLatestShopperBasketOrNew(shopper);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("GetLatestBasket : " + ex.InnerException);
                throw ex;
            }

            return basket;
        }


        public Basket AddItemsToBasket(string shopperName, Dictionary<int, int> itemsToAdd)
        {
            Basket basket = null;

            try
            {
                var shopper = ShoppingCartDomain.GetShopper(shopperName);

                if (shopper != null)
                {
                    basket = ShoppingCartDomain.GetLatestShopperBasketOrNew(shopper);

                    foreach (var itemToAdd in itemsToAdd)
                    {
                        var item = ShoppingCartDomain.GetItem(itemToAdd.Key);

                        if (item.Stock > 0)
                        {
                            item.Quantity = item.Stock > itemToAdd.Value ? itemToAdd.Value : item.Stock;
                            item.Stock = item.Stock > itemToAdd.Value ? item.Stock - itemToAdd.Value : 0;
                            basket = ShoppingCartDomain.AddItem(basket, item);
                        }
                        else
                        {
                            throw new Exception(string.Format("The item {0} has no stock.", itemToAdd));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("AddItemsToBasket : " + ex.InnerException);
                throw ex;
            }

            return basket;
        }

    }
}
