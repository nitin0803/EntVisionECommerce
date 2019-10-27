using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent_Vision_Procurement.Model
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public string PartNumber { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
    }
}
