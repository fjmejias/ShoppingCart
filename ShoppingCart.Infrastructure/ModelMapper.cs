using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Infrastructure
{
    public class ModelMapper
    {
        internal static Library.Model.Item Map(Repository.Item dbItem)
        {
            return new Library.Model.Item()
            {
                Id = dbItem.Id,
                Description = dbItem.Description,
                Stock = dbItem.Stock,
                Name = dbItem.Name,
                Price = dbItem.Price
            };
        }

        internal static Library.Model.Basket Map(Repository.Basket dbBasket)
        {
            List<Library.Model.Item> items = null;

            if (dbBasket.BasketItems != null)
            {
                items = dbBasket.BasketItems.Select(bi => Map(bi.Item)).ToList();
            }

            return new Library.Model.Basket()
            {
                Id = dbBasket.Id,
                CreationDate = dbBasket.CreationDate,
                FinishDate = dbBasket.FinishDate,
                Cancelled = dbBasket.Cancelled,
                ShopperId = dbBasket.ShopperId,
                Shopper = Map(dbBasket.Shopper),
                Items = items
            };
        }

        internal static Library.Model.Shopper Map(Repository.Shopper dbShopper)
        {
            return new Library.Model.Shopper()
            {
                Id = dbShopper.Id,
                Name = dbShopper.Name
            };
        }

        //internal static Repository.Item MapToRepository(Library.Model.Item dbItem)
        //{
        //    return new Repository.Item()
        //    {
        //        Id = dbItem.Id,
        //        Description = dbItem.Description,
        //        Stock = dbItem.Stock,
        //        Name = dbItem.Name,
        //        Price = dbItem.Price
        //    };
        //}

        //internal static Repository.Basket MapToRepository(Library.Model.Basket dbBasket)
        //{
        //    List<Repository.Item> items = null;

        //    if (dbBasket.Items != null)
        //    {
        //        items = dbBasket.Items.Select(MapToRepository).ToList();
        //    }

        //    return new Repository.Basket()
        //    {
        //        Id = dbBasket.Id,
        //        CreationDate = dbBasket.CreationDate,
        //        FinishDate = dbBasket.FinishDate,
        //        Finished = dbBasket.Finished,
        //        ShopperId = dbBasket.ShopperId,
        //        Shopper = MapToRepository(dbBasket.Shopper),
        //        Items = items
        //    };
        //}

        //internal static Repository.Shopper MapToRepository(Library.Model.Shopper dbShopper)
        //{
        //    return new Repository.Shopper()
        //    {
        //        Id = dbShopper.Id,
        //        Name = dbShopper.Name
        //    };
        //}
    }
}
