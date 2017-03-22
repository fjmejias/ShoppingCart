using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ShoppingCart.Library.Model;
using ShoppingCart.Library.RepositoryContract;

namespace ShoppingCart.ServiceLibrary.UnitTest.Mocks
{
    public class RepositoryMock : IRepository
    {
        private static Basket _basket;

        public static void SetMockBasket(Basket basket)
        {
            _basket = basket;
        }

        public IList<Item> GetAllItems()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..\ShoppingCart.ServiceLibrary.UnitTest\Mocks\example_data.csv");
            string[] allLines = File.ReadAllLines(path);

            var query = from line in allLines
                        where !line.Contains("Name,")
                        let data = line.Split(',')
                        select new Item
                        {
                            Id = Convert.ToInt16(data[0]),
                            Name = data[1],
                            Description = data[2],
                            Stock = Convert.ToInt16(data[3]),
                            Price = Convert.ToDecimal(data[4])
                        };

            return query.ToList();
        }

        public IList<Item> GetBasketItems(int basketId)
        {
            throw new NotImplementedException();
        }

        public Basket GetBasket(int basketId)
        {
            throw new NotImplementedException();
        }

        public int CheckOutBasket(int basketId)
        {
            throw new NotImplementedException();
        }

        public Shopper GetShopper(string name)
        {
            return new Shopper() {Id = 1, Name = "Mika"};
        }

        public Basket GetLatestShopperBasket(int shopperId)
        {
            if (_basket == null)
                return new Basket()
                {
                    CreationDate = DateTime.Today,
                    Id = 1,
                    ShopperId = shopperId,
                    Items = new List<Item>()
                };
            
            return _basket;
        }


        public Item GetItem(int id)
        {
            return GetAllItems().SingleOrDefault(i => i.Id == id);
        }

        public Basket UpdateBasket(Basket basket)
        {
            return basket;
        }

        public Basket GetNewBasket(int shopperId)
        {
            return new Basket()
            {
                CreationDate = DateTime.Today,
                Id = 2,
                ShopperId = shopperId
            };
        }

        public Item UpdateItem(Item item)
        {
            return item;
        }
        
    }
}
