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

        public Basket AddItem(Basket basket, Item item, int quantity)
        {
            //if (item != null && item.Stock > 0)
            //{
            //    var newQuantity = (item.Quantity ?? 0) + quantity;
            //    item.Quantity = item.Stock > newQuantity ? newQuantity : item.Stock;
            //    //item.Stock = item.Stock > itemToAdd.Value ? item.Stock - itemToAdd.Value : 0;
            //}

            if (item.Stock > 0)
            {
                var newQuantity = (item.Quantity ?? 0) + quantity;
                item.Quantity = item.Stock > newQuantity ? newQuantity : item.Stock;
            }
            else
            {
                _logger.Warn(string.Format("The item {0} has no stock.", item.Id));
            }

            if (basket.Items == null)
            {
                basket.Items = new List<Item> {item};
            }
            else if (basket.Items.All(i => i.Id != item.Id))
            {
                basket.Items.Add(item);
            }

            return Repository.UpdateBasket(basket);
        }

        public void UpdateBasket(Basket basket)
        {
            Repository.UpdateBasket(basket);
        }


        public IList<Item> CheckOutBasket(Basket basket)
        {
            basket.FinishDate = DateTime.Now;

            foreach (var item in basket.Items)
            {
                item.Stock = item.Stock - item.Quantity;
                Repository.UpdateItem(item);
            }

            Repository.UpdateBasket(basket);

            return basket.Items;
        }
    }
}
