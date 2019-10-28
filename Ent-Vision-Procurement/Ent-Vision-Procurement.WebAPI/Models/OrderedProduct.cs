using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ent_Vision_Procurement.WebAPI.Models
{
    public class OrderedProduct
    {
        public string PartNumber { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public int OrderedQuantity { get; set; }
        public double TotalPrice { get; set; }
        public string Available { get; set; }
    }
}