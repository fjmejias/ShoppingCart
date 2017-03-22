//using System.Data.Entity;

//namespace ShoppingCart.Infrastructure.Repository
//{
//    public class ShoppingCartDbContext : IShoppingCartDbContext
//    {
//        public ShoppingCartEntities Context { get; set; }

//        public ShoppingCartDbContext()
//        {
//            Context = GetContext();
//        }

//        public ShoppingCartEntities GetContext()
//        {
//            return new ShoppingCartEntities();
//        }

//        public void Persist()
//        {
//            //throw new NotImplementedException();
//        }

//        public void Dispose()
//        {
//            //throw new NotImplementedException();
//        }

//        protected virtual void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}
