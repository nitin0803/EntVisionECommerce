using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent_Vision_Procurement.Model
{
    public class Product
    {
        public string PartNumber { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public bool Discontinued { get; set; }
    }
}
