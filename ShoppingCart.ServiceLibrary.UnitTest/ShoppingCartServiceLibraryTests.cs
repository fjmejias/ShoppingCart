using System;
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
                Console.WriteLine(string.Format("Name: {0}, Description: {1}, Id: {2}, Quantity: {3}", item.Name,
                    item.Description, item.Id, item.Stock));
            }
            
        }

        [TestMethod]
        public void UserStory2()
        {
            var service = kernel.Get<IShoppingCartService>();

            string shopperName = "Mika";
            int itemToAdd = 2;

            var basket = service.GetLatestBasket(shopperName);

            Assert.IsFalse(basket.Items.Any(i => i.Id == itemToAdd));

            Console.WriteLine("Before adding an item");
            Console.WriteLine($"Basket of user: {shopperName}");
            Console.WriteLine($"Items in the basket: {basket.Items.Count}");

            basket = service.AddItemToBasket(shopperName, itemToAdd);

            Assert.IsTrue(basket.Items.Any(i => i.Id == itemToAdd));

            Console.WriteLine("After adding an item");
            Console.WriteLine($"Basket of user: {shopperName}");
            Console.WriteLine($"Items in the basket: {basket.Items.Count}");
            foreach (var item in basket.Items)
            {
                Console.WriteLine(string.Format("Name: {0}, Description: {1}, Id: {2}, Quantity: {3}", item.Name,
                    item.Description, item.Id, item.Stock));
            }
        }
    }
}
