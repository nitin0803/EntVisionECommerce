using Ent_Vision_Procurement.Model;
using Ent_Vision_Procurement.Repository;
using Ent_Vision_Procurement.WebAPI.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Ent_Vision_Procurement.WebAPI.Controllers
{
    [RoutePrefix("Store")]
    [EnableCors("*", "*", "*")]
    public class StoreController : ApiController
    {
        private readonly IStoreRepository repository;
        private List<string> categories = new List<string>();
        public StoreController()
        {
            if (this.repository == null)
            {
                this.repository = new StoreRepository();
            }
            this.repository.SeedProductData();
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public IEnumerable<OrderedProduct> GetAllProducts([FromUri] int? orderId = null)
        {
            var result = new List<OrderedProduct>();
            var allProducts = this.repository.GetAll();
            var orderDetails = this.repository.GetOrderDetails(orderId);
            foreach (var item in allProducts)
            {
                categories.Add(item.CategoryName);
                var orderedQty = orderId == null ? 0 : orderDetails.Where(x => x.OrderId == orderId && x.PartNumber == item.PartNumber)
                                                            .FirstOrDefault().Quantity;
                var orderedProduct = new OrderedProduct
                {
                    PartNumber = item.PartNumber,
                    CategoryName = item.CategoryName,
                    ProductName = item.ProductName,
                    QuantityPerUnit = item.QuantityPerUnit,
                    UnitPrice = item.UnitPrice,
                    OrderedQuantity = orderedQty,
                    TotalPrice = item.UnitPrice * orderedQty,
                    Available = item.Discontinued ? "No" : "Yes"
                };
                result.Add(orderedProduct);
            }

            return result;

        }

        [HttpGet]
        [Route(" ")]
        public IEnumerable<CategoryAndProducts> GetCategoriesAndProducts()
        {
            var allProducts = this.repository.GetAll();
            var categories = allProducts.Select(x => x.CategoryName).Distinct().ToList();
            var CategoriesAndProducts = new List<CategoryAndProducts>();
            foreach (var categoryName in categories)
            {
                CategoriesAndProducts.Add(new CategoryAndProducts
                {
                    CategoryName = categoryName,
                    Products = this.repository.GetProductsByCategory(categoryName)
                });
            }

            return CategoriesAndProducts;
        }

        [HttpPost]
        [Route("ProductsByCategory")]
        public IEnumerable<OrderedProduct> ProductsByCategory([FromBody] StoreServiceInput storeServiceInput)
        {
            var allProducts = this.GetAllProducts();
            if (string.Equals(storeServiceInput.CategoryName, "All", StringComparison.InvariantCultureIgnoreCase))
                return allProducts;
            return allProducts.Where(x=>x.CategoryName == storeServiceInput.CategoryName).ToList();
        }

        [HttpPost]
        [Route("ProductByPartNumber")]
        public IEnumerable<OrderedProduct> ProductByPartNumber([FromBody] StoreServiceInput storeServiceInput)
        {
            var allProducts = this.GetAllProducts();
            if (string.Equals(storeServiceInput.PartNumber, "All", StringComparison.InvariantCultureIgnoreCase))
                return allProducts;

            return allProducts.Where(x => x.PartNumber == storeServiceInput.PartNumber).ToList();
        }

        [HttpPost]
        [Route("PlaceTransportOrder")]
        public void PlaceTransportOrder([FromBody] Order order)
        {
            this.repository.InsertSalesOrder(order);
            this.repository.UpdateInventory(order.OrderId);
        }

        [HttpGet]
        [Route("CategoryAndQuantity")]
        public Dictionary<string, int> ProductsOrderQuantityByCategories([FromUri] int? orderId = null)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            var allProducts = this.GetAllProducts(orderId);
            var categories = allProducts.Select(x => x.CategoryName).Distinct().ToList();

            foreach (var category in categories)
            {
                var orderedQuantity = 0;
                var categoryProducts = allProducts.Where(x => x.CategoryName == category).ToList();
                foreach (var product in categoryProducts)
                {
                    orderedQuantity += product.OrderedQuantity;
                }
                result.Add(category, orderedQuantity);
            }

            return result;
        }
    }
}
