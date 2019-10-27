using Ent_Vision_Procurement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ent_Vision_Procurement.WebAPI.Models
{
    public class CategoryAndProducts
    {
        public CategoryAndProducts()
        {
            this.Products = new List<Product>();
        }
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}