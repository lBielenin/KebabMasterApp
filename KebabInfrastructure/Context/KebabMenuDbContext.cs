using KebabCore.DomainModels.Menu;
using KebabCore.DomainModels.Orders;
using KebabCore.Models.Orders;
using KebabInfrastructure.Views;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KebabInfrastructure.Context
{
    public class KebabMenuDbContext : DbContext
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<MenuView> MenuView { get; set; }
        public DbSet<OrderView> OrderView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(
                @"Data Source=localhost;Initial Catalog=KebabDB;Integrated Security=True");
        }

        public int GetOrderNumberById(Guid orderId)
        {
            object value = new object();
            string queryString = "SELECT TOP 1 [OrderNumber] FROM [dbo].[Orders]WHERE OrderId = @orderId";

            using (var conn = new SqlConnection(@"Data Source=DESKTOP-AH54VTK;Initial Catalog=KebabDB;Integrated Security=True"))
            {
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@orderId", orderId);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        value = reader["OrderNumber"];
                    }
                }
                finally
                {
                    reader.Close();
                }

            }

            return (int) value;

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuView>().HasNoKey();
            modelBuilder.Entity<OrderView>().HasNoKey();

            modelBuilder.Entity<MenuItem>().HasKey(mi => new { mi.MenuId, mi.ItemId });
            modelBuilder.Entity<MenuItem>().HasOne<Menu>().WithMany(m => m.MenuItems).HasForeignKey(e => e.MenuId).IsRequired();

            modelBuilder
                .Entity<Item>()
                .Property(e => e.Category)
                .HasConversion<int>();

            modelBuilder
                .Entity<MenuView>()
                .Property(v => v.ItemCategory)
                .HasConversion<string>();

            modelBuilder
                .Entity<OrderView>()
                .Property(v => v.OrderForm)
                .HasConversion<string>();

            modelBuilder
                .Entity<OrderView>()
                .Property(v => v.StatusName)
                .HasConversion<string>();

            modelBuilder
                .Entity<OrderView>()
                .Property(v => v.PaymentForm)
                .HasConversion<string>();

            modelBuilder
                .Entity<OrderView>()
                .Property(v => v.CategoryName)
                .HasConversion<string>();



            //                public Category CategoryName { get; set; }
            //public int OrderNumber { get; set; }
            //public int Quantity { get; set; }
            //public Status StatusName { get; set; }
            //public PaymentForm PaymentForm { get; set; }
            //public OrderForm OrderForm { get; set; }
        }
    }
}
