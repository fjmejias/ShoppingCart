using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using ShoppingCart.Library;
using ShoppingCart.Library.Model;
using ShoppingCart.Library.RepositoryContract;
using ShoppingCart.ServiceLibrary.UnitTest.Mocks;

namespace ShoppingCart.ServiceLibrary.UnitTest
{
    [TestClass]
    public class ShoppingCartServiceLibraryTests
    {
        protected StandardKernel Kernel;
        private static Basket _basket;

        [TestInitialize]
        public void TestInitialize()
        {
            Kernel = new StandardKernel();

            Kernel.Bind<IShoppingCartService>().To<ShoppingCartService>();
            Kernel.Bind<IShoppingCartDomain>().To<ShoppingCartDomain>();
            Kernel.Bind<IRepository>().To<RepositoryMock>();
        }

        [TestMethod]
        public void UserStory1()
        {
            var service = Kernel.Get<IShoppingCartService>();

            var items = service.GetAllItems();

            Assert.AreEqual(items.Count, 5);

            PrintItems(items);
        }

        

        [TestMethod]
        public void UserStory2()
        {
            var service = Kernel.Get<IShoppingCartService>();
            RepositoryMock.SetMockBasket(null);

            string shopperName = "Mika";
            int itemToAdd = 2;

            _basket = service.GetLatestBasket(shopperName);
            var item = service.GetItem(itemToAdd);

            Console.WriteLine("Before adding an item");
            PrintBasket(shopperName, _basket);

            Console.WriteLine("Item to add:");
            Console.WriteLine("Name: {0}, Description: {1}\t Id: {2}\t Stock: {3}", item.Name, item.Description, item.Id, item.Stock);

            _basket = service.AddItemToBasket(shopperName, itemToAdd);

            Assert.IsTrue(_basket.Items.Any(i => i.Id == itemToAdd));

            Console.WriteLine("After adding an item");
            PrintBasket(shopperName, _basket);
        }

        [TestMethod]
        public void UserStory3()
        {
            var service = Kernel.Get<IShoppingCartService>();

            string shopperName = "Mika";
            var itemsToAdd = new Dictionary<int, int>();

            itemsToAdd.Add(2, 3);
            itemsToAdd.Add(3, 15);
            itemsToAdd.Add(4, 4);
            itemsToAdd.Add(5, 10);

            RepositoryMock.SetMockBasket(_basket);

            _basket = service.GetLatestBasket(shopperName);
            
            Console.WriteLine("Before adding a list of items");
            PrintBasket(shopperName, _basket);

            Console.WriteLine("Item to add:");
            foreach (var item in itemsToAdd)
            {
                var itemToAdd = service.GetItem(item.Key);
                Console.WriteLine("Name: {0}\t Description: {1}\t Id: {2}\t Quantity: {3}\t Stock: {4}", itemToAdd.Name, itemToAdd.Description, itemToAdd.Id, item.Value, itemToAdd.Stock);
            }

            _basket = service.AddItemsToBasket(shopperName, itemsToAdd);

            Assert.IsTrue(_basket.Items.Any(i => itemsToAdd.Keys.Contains(i.Id)));

            Console.WriteLine("After adding an item");
            PrintBasket(shopperName, _basket);
        }

        [TestMethod]
        public void UserStory4()
        {
            var service = Kernel.Get<IShoppingCartService>();

            string shopperName = "Mika";
            RepositoryMock.SetMockBasket(_basket);

            _basket = service.GetLatestBasket(shopperName);

            Console.WriteLine("Before checking out");
            PrintBasket(shopperName, _basket);

            var itemsBought = service.CheckOutBasket(_basket);

            Console.WriteLine("After checking out");
            PrintItems(itemsBought);
        }

        #region privateMethods

        private void PrintItems(IList<Item> items)
        {
            foreach (var item in items)
            {
                Console.WriteLine("Name: {0}\t Description: {1}\t Id: {2}\t Stock: {3}", item.Name, item.Description, item.Id, item.Stock);
            }
        }

        private void PrintBasket(string shopperName, Basket basket)
        {
            Console.WriteLine("Basket of user: {0}", shopperName);
            Console.WriteLine("Different Items in the basket: {0}, Total Items in the basket: {1}", basket.Items.Count,
                basket.Items.Sum(i => i.Quantity));
            foreach (var basketItem in basket.Items)
            {
                Console.WriteLine("Name: {0}\t Description: {1}\t Id: {2}\t Quantity: {3}\t Stock: {4}", basketItem.Name, basketItem.Description, basketItem.Id, basketItem.Quantity, basketItem.Stock);
            }
        }

        #endregion
    }
}
