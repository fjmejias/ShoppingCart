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
                        select new Library.Model.Item()
                {
                    Id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock
                };

            return items.ToList();
        }

        public IList<Library.Model.Item> GetBasketItems(int basketId)
        {
            var items =
                _dbContext.Baskets.Where(b => b.Id == basketId)
                    .SelectMany(b => b.Items)
                    .Select(p => new Library.Model.Item()
                    {
                        Id = p.Id,
                        Description = p.Description,
                        Name = p.Name,
                        Price = p.Price,
                        Stock = p.Stock
                    });

            return items.ToList();
        }

        public void AddItem(int basketId, int itemId)
        {
            var basket = _dbContext.Baskets.SingleOrDefault(b => b.Id == basketId);
            var item = _dbContext.Items.SingleOrDefault(p => p.Id == itemId);

            if (basket != null && item != null) 
                basket.Items.Add(item);

            _dbContext.SaveChanges();
        }

        public Library.Model.Basket GetBasket(int basketId)
        {
            var basket = from b in _dbContext.Baskets
                where b.Id == basketId
                         select new Library.Model.Basket()
                {
                    Id = b.Id,
                    CreationDate = b.CreationDate,
                    FinishDate = b.FinishDate,
                    Finished = b.Finished,
                    ShopperId = b.ShopperId
                };

            return basket.SingleOrDefault();
        }

        public int CheckOutBasket(int basketId)
        {
            throw new System.NotImplementedException();
        }


        public Library.Model.Shopper GetShopper(string name)
        {
            var shopper = from s in _dbContext.Shoppers
                         where s.Name == name
                         select new Library.Model.Shopper()
                         {
                             Id = s.Id,
                             Name = s.Name
                         };

            return shopper.SingleOrDefault();
        }


        public Library.Model.Basket GetLatestShopperBasket(int shopperId)
        {
            var basket = from b in _dbContext.Baskets
                where b.ShopperId == shopperId
                select new Library.Model.Basket()
                {
                    CreationDate = b.CreationDate,
                    FinishDate = b.FinishDate,
                    Id = b.Id,
                    Finished = b.Finished,
                    ShopperId = shopperId
                };

            return basket.OrderByDescending(b => b.CreationDate).FirstOrDefault();
        }


        public Library.Model.Item GetItem(int id)
        {
            return _dbContext.Items.Where(i => i.Id == id).Select(i => new Library.Model.Item()
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                Description = i.Description,
                Stock = i.Stock
            }).FirstOrDefault();
        }

        public void UpdateBasket(Library.Model.Basket basket)
        {
            var dbBasket = _dbContext.Baskets.FirstOrDefault(b => b.Id == basket.Id);

            if (dbBasket != null)
            {
                dbBasket.FinishDate = basket.FinishDate;
                dbBasket.Finished = basket.Finished;
                dbBasket.ShopperId = basket.ShopperId;

                if (dbBasket.Items == null) dbBasket.Items = new List<Item>();
                foreach (var item in basket.Items)
                {
                    dbBasket.Items.Add(new Item()
                    {
                        Id = item.Id,
                        Description = item.Description,
                        Name = item.Name,
                        Price = item.Price,
                        Stock = item.Stock
                    });
                }

                _dbContext.SaveChanges();
            }
        }


        public Library.Model.Basket GetNewBasket(int shopperId)
        {
            var dbBasket = new Basket() {CreationDate = DateTime.Today, Finished = false, ShopperId = shopperId};

            _dbContext.Baskets.Add(dbBasket);

            _dbContext.SaveChanges();

            return 
        }
    }
}
