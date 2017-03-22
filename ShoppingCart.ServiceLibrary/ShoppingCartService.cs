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
            IList<Item> listItems;

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
            Item item;

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
                        
                    basket = ShoppingCartDomain.AddItem(basket, item, 1);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("AddItemToBasket : " + ex.InnerException);
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
                        Item item = basket.Items != null
                            ? basket.Items.FirstOrDefault(i => i.Id == itemToAdd.Key)
                            : null;

                        if (item == null) item = ShoppingCartDomain.GetItem(itemToAdd.Key);
                        
                        if (item != null)
                            basket = ShoppingCartDomain.AddItem(basket, item, itemToAdd.Value);
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



        public IList<Item> CheckOutBasket(Basket basket)
        {
            IList<Item> items;

            try
            {
                items = ShoppingCartDomain.CheckOutBasket(basket);
            }
            catch (Exception ex)
            {
                Logger.Error("CheckOutBasket : " + ex.InnerException);
                throw ex;
            }

            return items;
        }
    }
}
