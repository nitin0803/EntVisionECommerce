using Ent_Vision_Procurement.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ent_Vision_Procurement.DAL
{
    public class InventoryConfiguration : EntityTypeConfiguration<Inventory>
    {
        public InventoryConfiguration()
        {
            ToTable("Inventory");
            HasKey(x => x.PartNumber);
        }
    }
}
