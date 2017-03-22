using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using ShoppingCart.Infrastructure.Repository;
using ShoppingCart.Library;
using ShoppingCart.Library.RepositoryContract;
using ShoppingCart.ServiceLibrary.UnitTest.Mocks;

namespace ShoppingCart.ServiceLibrary.UnitTest
{
    [TestClass]
    public class ShoppingCartServiceLibraryTests
    {
        protected StandardKernel kernel;

        [TestInitialize]
        public void TestInitialize()
        {
            kernel = new StandardKernel();

            kernel.Bind<IShoppingCartService>().To<ShoppingCartService>();
            kernel.Bind<IShoppingCartDomain>().To<ShoppingCartDomain>();
            kernel.Bind<IRepository>().To<RepositoryMock>();
        }

        [TestMethod]
        public void UserStory1()
        {
            var service = kernel.Get<IShoppingCartService>();

            var items = service.GetAllItems();

            Assert.AreEqual(items.Count, 5);

            foreach (var item in items)
            {
                Console.WriteLine("Name: {0}, Description: {1}, Id: {2}, Quantity: {3}", item.Name, item.Description, item.Id, item.Stock);
            }
            
        }

        [TestMethod]
        public void UserStory2()
        {
            var service = kernel.Get<IShoppingCartService>();

            string shopperName = "Mika";
            int itemToAdd = 2;

            var basket = service.GetLatestBasket(shopperName);
            var item = service.GetItem(itemToAdd);

            Assert.IsFalse(basket.Items.Any(i => i.Id == itemToAdd));

            Console.WriteLine("Before adding an item");
            Console.WriteLine("Basket of user: {0}", shopperName);
            Console.WriteLine("Items in the basket: {0}", basket.Items.Count);

            Console.WriteLine("Item to add:");
            Console.WriteLine("Name: {0}, Description: {1}, Id: {2}, Quantity: {3}, Stock: {4}", item.Name, item.Description, item.Id, item.Quantity, item.Stock);

            basket = service.AddItemToBasket(shopperName, itemToAdd);

            Assert.IsTrue(basket.Items.Any(i => i.Id == itemToAdd));

            Console.WriteLine("After adding an item");
            Console.WriteLine("Basket of user: {0}", shopperName);
            Console.WriteLine("Items in the basket: {0}", basket.Items.Count);
            foreach (var basketItem in basket.Items)
            {
                Console.WriteLine("Name: {0}, Description: {1}, Id: {2}, Quantity: {3}, Stock: {4}", basketItem.Name, basketItem.Description, basketItem.Id, basketItem.Quantity, basketItem.Stock);
            }
        }

        [TestMethod]
        public void UserStory3()
        {
            var service = kernel.Get<IShoppingCartService>();

            string shopperName = "Mika";
            var itemsToAdd = new Dictionary<int, int>();

            itemsToAdd.Add(2, 3);
            itemsToAdd.Add(3, 8);
            itemsToAdd.Add(4, 4);
            itemsToAdd.Add(5, 10);

            var basket = service.GetLatestBasket(shopperName);

            Assert.IsFalse(basket.Items.Any(i => itemsToAdd.Keys.Contains(i.Id)));

            Console.WriteLine("Before adding a list of items");
            Console.WriteLine("Basket of user: {0}", shopperName);
            Console.WriteLine("Items in the basket: {0}", basket.Items.Count);

            Console.WriteLine("Item to add:");
            foreach (var item in itemsToAdd)
            {
                var itemToAdd = service.GetItem(item.Key);
                Console.WriteLine("Name: {0}, Description: {1}, Id: {2}, Quantity: {3}, Stock: {4}", itemToAdd.Name, itemToAdd.Description, itemToAdd.Id, item.Value, itemToAdd.Stock);
            }

            basket = service.AddItemsToBasket(shopperName, itemsToAdd);

            Assert.IsTrue(basket.Items.Any(i => itemsToAdd.Keys.Contains(i.Id)));

            Console.WriteLine("After adding an item");
            Console.WriteLine("Basket of user: {0}", shopperName);
            Console.WriteLine("Items in the basket: {0}", basket.Items.Count);
            foreach (var basketItem in basket.Items)
            {
                Console.WriteLine("Name: {0}, Description: {1}, Id: {2}, Quantity: {3}, Stock: {4}", basketItem.Name, basketItem.Description, basketItem.Id, basketItem.Quantity, basketItem.Stock);
            }
        }
    }
}
