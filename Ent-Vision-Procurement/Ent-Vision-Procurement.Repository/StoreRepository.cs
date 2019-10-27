using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ent_Vision_Procurement.Model;
using Ent_Vision_Procurement.DAL;
using System.Data.Entity;

namespace Ent_Vision_Procurement.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreDBContext storeDBContext;

        public StoreRepository()
        {
            if(this.storeDBContext == null)
            {
                storeDBContext = new StoreDBContext();
            }
        }
        public List<Product> GetAll()
        {
            return this.storeDBContext.Products.ToList(); ;
        }

        public List<Product> GetProductsByCategory(string categoryName)
        {
            return this.GetAll().Where(x => x.CategoryName == categoryName).ToList();
        }

        public List<Product> GetProductByPartNumber(string partNumber)
        {
            return this.GetAll().Where(x => x.PartNumber == partNumber).ToList();
        }

        public void SeedProductData()
        {
            Database.SetInitializer(new StoreSeedData());
        }

        public void InsertSalesOrder(Order order)
        {
            this.storeDBContext.Orders.Add(order);
            this.storeDBContext.SaveChanges();
        }
    }
}
