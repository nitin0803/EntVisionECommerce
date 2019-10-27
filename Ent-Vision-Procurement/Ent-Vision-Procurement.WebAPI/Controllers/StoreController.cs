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
        public StoreController()
        {
            if (this.repository == null)
            {
                this.repository = new StoreRepository();
            }
            this.repository.SeedProductData();
        }

        [HttpGet]
        [Route("Products")]
        public IEnumerable<Product> GetAllProducts()
        {
            return this.repository.GetAll();
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
        public IEnumerable<Product> ProductsByCategory([FromBody] StoreServiceInput storeServiceInput)
        {
            return this.repository.GetProductsByCategory(storeServiceInput.CategoryName);
        }

        [HttpPost]
        [Route("ProductByPartNumber")]
        public IEnumerable<Product> ProductByPartNumber([FromBody] StoreServiceInput storeServiceInput)
        {
            return this.repository.GetProductByPartNumber(storeServiceInput.PartNumber);
        }

        [HttpPost]
        [Route("PlaceTransportOrder")]
        public void PlaceTransportOrder([FromBody] Order order)
        {
            this.repository.InsertSalesOrder(order);
        }

        [HttpGet]
        [Route("CategoryAndQuantity")]
        public Dictionary<string, int> ProductsOrderQuantityByCategories()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            var products = this.GetAllProducts();
            var categories = products.Select(x => x.CategoryName).Distinct().ToList();

            foreach (var category in categories)
            {
                var orderedQuantity = 0;
                var categoryProducts = products.Where(x => x.CategoryName == category).ToList();
                foreach (var product in categoryProducts)
                {
                    ///// TBD
                    orderedQuantity += 0;
                }
                result.Add(category, orderedQuantity);
            }

            return result;
        }
        /*
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
