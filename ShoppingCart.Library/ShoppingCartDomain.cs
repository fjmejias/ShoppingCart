using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using ShoppingCart.Library.Model;
using ShoppingCart.Library.RepositoryContract;

namespace ShoppingCart.Library
{
    public class ShoppingCartDomain : IShoppingCartDomain
    {
        private ILog _logger = LogManager.GetLogger("ShoppingCart.Library");

        public IRepository Repository { set; get; }

        public ShoppingCartDomain(IRepository repository)
        {
            Repository = repository;
        }

        public IList<Item> GetAllItems()
        {
            return Repository.GetAllItems();
        }

        public IList<Item> GetBaksetItems(Basket basket)
        {
            throw new NotImplementedException();
        }


        public Shopper GetShopper(string name)
        {
            return Repository.GetShopper(name);
        }


        public Item GetItem(int id)
        {
            return Repository.GetItem(id);
        }

        public Basket GetLatestShopperBasketOrNew(Shopper shopper)
        {
            var basket = Repository.GetLatestShopperBasket(shopper.Id);

            if (basket == null)
                basket = Repository.GetNewBasket(shopper.Id);

            return basket;
        }

        public Basket AddItem(Basket basket, Item item)
        {
            throw new NotImplementedException();
        }

        public void UpdateBasket(Basket basket)
        {
            Repository.UpdateBasket(basket);
        }
    }
}
