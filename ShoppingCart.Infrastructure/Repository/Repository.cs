using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Infrastructure.Repository
{
    public class Repository : Library.RepositoryContract.IRepository
    {
        private readonly ShoppingCartEntities _dbContext = null;

        public Repository()
        {
            _dbContext = new ShoppingCartEntities();
        }

        public IList<Library.Model.Item> GetAllItems()
        {
            var items = from p in _dbContext.Items
                select ModelMapper.Map(p);

            return items.ToList();
        }

        public IList<Library.Model.Item> GetBasketItems(int basketId)
        {
            var items =
                _dbContext.Baskets.Where(b => b.Id == basketId)
                    .SelectMany(b => b.BasketItems)
                    .Select(p => ModelMapper.Map(p.Item));

            return items.ToList();
        }

        public Library.Model.Basket GetBasket(int basketId)
        {
            var basket = from b in _dbContext.Baskets
                where b.Id == basketId
                select ModelMapper.Map(b);

            return basket.SingleOrDefault();
        }


        public Library.Model.Shopper GetShopper(string name)
        {
            var shopper = from s in _dbContext.Shoppers
                         where s.Name == name
                         select ModelMapper.Map(s);

            return shopper.SingleOrDefault();
        }


        public Library.Model.Basket GetLatestShopperBasket(int shopperId)
        {
            var basket = from b in _dbContext.Baskets
                where b.ShopperId == shopperId && b.FinishDate == null && b.Cancelled == false
                select ModelMapper.Map(b);

            return basket.OrderByDescending(b => b.CreationDate).FirstOrDefault();
        }


        public Library.Model.Item GetItem(int id)
        {
            return _dbContext.Items.Where(i => i.Id == id).Select(i => ModelMapper.Map(i)).FirstOrDefault();
        }

        public Library.Model.Basket UpdateBasket(Library.Model.Basket basket)
        {
            var dbBasket = _dbContext.Baskets.FirstOrDefault(b => b.Id == basket.Id);

            if (dbBasket != null)
            {
                dbBasket.FinishDate = basket.FinishDate;
                dbBasket.ShopperId = basket.ShopperId;

                if (dbBasket.BasketItems == null) dbBasket.BasketItems = new List<BasketItem>();
                foreach (var item in basket.Items)
                {
                    var dbBasketItem = dbBasket.BasketItems.FirstOrDefault(bi => bi.Item.Id == item.Id);
                    if (dbBasketItem != null)
                    {
                        dbBasketItem.Quantity = item.Quantity ?? 1;
                    }
                    else
                    {
                        dbBasket.BasketItems.Add(new BasketItem()
                        {
                            Basket = dbBasket,
                            Item = new Item()
                            {
                                Id = item.Id,
                                Description = item.Description,
                                Name = item.Name,
                                Price = item.Price,
                                Stock = item.Stock
                            },
                            Quantity = item.Quantity ?? 1
                        });
                    }
                }

                _dbContext.SaveChanges();
            }

            return ModelMapper.Map(dbBasket);
        }


        public Library.Model.Basket GetNewBasket(int shopperId)
        {
            var dbBasket = new Basket() {CreationDate = DateTime.Now, ShopperId = shopperId};

            _dbContext.Baskets.Add(dbBasket);

            _dbContext.SaveChanges();

            return ModelMapper.Map(dbBasket);
        }


        public Library.Model.Item UpdateItem(Library.Model.Item item)
        {
            //var dbBasket = _dbContext.Baskets.FirstOrDefault(b => b.Id == basket.Id);

            //if (dbBasket != null)
            //{
            //    dbBasket.FinishDate = basket.FinishDate;
            //    dbBasket.ShopperId = basket.ShopperId;

            //    if (dbBasket.BasketItems == null) dbBasket.BasketItems = new List<BasketItem>();
            //    foreach (var item in basket.Items)
            //    {
            //        var dbBasketItem = dbBasket.BasketItems.FirstOrDefault(bi => bi.Item.Id == item.Id);
            //        if (dbBasketItem != null)
            //        {
            //            dbBasketItem.Quantity = item.Quantity ?? 1;
            //        }
            //        else
            //        {
            //            dbBasket.BasketItems.Add(new BasketItem()
            //            {
            //                Basket = dbBasket,
            //                Item = new Item()
            //                {
            //                    Id = item.Id,
            //                    Description = item.Description,
            //                    Name = item.Name,
            //                    Price = item.Price,
            //                    Stock = item.Stock
            //                },
            //                Quantity = item.Quantity ?? 1
            //            });
            //        }
            //    }

            //    _dbContext.SaveChanges();
            //}

            //return ModelMapper.Map(dbBasket);
            return null;
        }
    }
}
