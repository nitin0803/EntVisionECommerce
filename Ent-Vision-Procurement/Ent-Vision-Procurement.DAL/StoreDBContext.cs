using Ent_Vision_Procurement.Model;
using System.Data.Entity;

namespace Ent_Vision_Procurement.DAL
{
    public class StoreDBContext : DbContext
    {
        public StoreDBContext(): base("name=StoreDBConnectionString")
        {
            //Database.SetInitializer<StoreDBContext>(new DropCreateDatabaseIfModelChanges<StoreDBContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailConfiguration());
            modelBuilder.Configurations.Add(new InventoryConfiguration());            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
    }
}
