
using Ent_Vision_Procurement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent_Vision_Procurement.Repository
{
    public interface IStoreRepository
    {
        void SeedProductData();
        List<Product> GetAll();
        List<Product> GetProductsByCategory(string categoryName);
        List<Product> GetProductByPartNumber(string partNumber);
        List<OrderDetail> GetOrderDetails(int? orderId);
        void InsertSalesOrder(Order order);
        void UpdateInventory(int orderId);
    }
}
