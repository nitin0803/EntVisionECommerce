using System.Data.Entity.ModelConfiguration;
using Ent_Vision_Procurement.Model;

namespace Ent_Vision_Procurement.DAL
{
    public class OrderDetailConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfiguration()
        {
            ToTable("OrderDetails");
        }
    }
}
