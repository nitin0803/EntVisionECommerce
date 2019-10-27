using Ent_Vision_Procurement.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent_Vision_Procurement.DAL
{
    public class StoreSeedData : DropCreateDatabaseAlways<StoreDBContext>
    {
        const string baseAddress = "http://devtest.ent-vision.com/";

        protected override void Seed(StoreDBContext context)
        {
            var seedProductsData = this.GetAllProducts();
            var inventories = new List<Inventory>();
            var products = new List<Product>();
            foreach (var item in seedProductsData)
            {
                var product = new Product
                {
                    PartNumber = item.PartNumber,
                    CategoryName = item.CategoryName,
                    ProductName = item.ProductName,
                    QuantityPerUnit = item.QuantityPerUnit,
                    UnitPrice = item.UnitPrice,
                    Discontinued = item.Discontinued
                };
                products.Add(product);
                context.Products.AddRange(products);

                var inventory = new Inventory
                {
                    PartNumber = item.PartNumber,
                    ReorderLevel = item.ReorderLevel,
                    UnitsInStock = item.UnitsInStock,
                    UnitsOnOrder = 0
                };
                inventories.Add(inventory);
            }
            context.Inventories.AddRange(inventories);

            context.SaveChanges();
        }

        private List<SeedProductData> GetAllProducts()
        {
            var client = new RestClient(baseAddress);
            var request = new RestRequest("wms/GetAllProducts", Method.GET);
            var result = client.Execute<List<SeedProductData>>(request).Data;
            return result.OrderBy(x => x.PartNumber).ToList();
        }
    }
}
