using Ent_Vision_Procurement.Model;
using System.Data.Entity.ModelConfiguration;

namespace Ent_Vision_Procurement.DAL
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Products");
            HasKey(x => x.PartNumber);
            Property(x => x.PartNumber);
            Property(x => x.ProductName);
            Property(x => x.QuantityPerUnit);
            Property(x => x.ProductName);
            Property(x => x.ProductName);
        }
    }
}
