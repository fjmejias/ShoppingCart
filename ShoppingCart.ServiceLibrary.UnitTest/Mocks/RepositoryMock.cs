using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Library.Model;
using ShoppingCart.Library.RepositoryContract;

namespace ShoppingCart.ServiceLibrary.UnitTest.Mocks
{
    public class RepositoryMock : IRepository
    {
        public IList<Library.Model.Item> GetAllItems()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Mocks\example_data.csv");
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

        public IList<Library.Model.Item> GetBasketItems(int basketId)
        {
            throw new NotImplementedException();
        }

        public void AddItem(int basketId, int itemId)
        {
            throw new NotImplementedException();
        }

        public Library.Model.Basket GetBasket(int basketId)
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
            return new Basket()
            {
                CreationDate = DateTime.Today,
                Id = 1,
                ShopperId = 1,
                Finished = false
            };
        }


        public Item GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBasket(Basket basket)
        {
            throw new NotImplementedException();
        }
    }
}
