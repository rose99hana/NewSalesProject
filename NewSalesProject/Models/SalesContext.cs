using NewSalesProject.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Model
{
    public partial class SalesContext : DbContext
    {
        public SalesContext() : base("name=SalesContextString")
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<ReceiptDetail>()
                .HasRequired(c => c.Product)
                .WithMany()
                .WillCascadeOnDelete(false);
        }


        public virtual DbSet<CargoFlight> CargoFlights { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<MoneyKeeper> MoneyKeepers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductPrice> ProductPrices { get; set; }
        public virtual DbSet<CustomerRank> CustomerRanks { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<GoodsReceipt> GoodsReceipts { get; set; }
        public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }
        public virtual DbSet<WishListDetail> WishListDetails { get; set; }

        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetCategory> AssetCategories { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<InstallationLocation> InstallationLocations { get; set; }

    }
}
