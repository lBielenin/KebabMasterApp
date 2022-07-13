using KebabCore.DomainModels.Menu;
using KebabCore.DomainModels.Orders;
using KebabCore.Models.Orders;
using KebabInfrastructure.Options;
using KebabInfrastructure.Views;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KebabInfrastructure.Context
{
    public class KebabDbContext : DbContext, IDisposable
    {
        private readonly KebabDBConnectionSettings options;
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<MenuView> MenuView { get; set; }
        public DbSet<OrderView> OrderView { get; set; }

        public KebabDbContext(IOptions<KebabDBConnectionSettings> settings)
        {
            options = settings.Value;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(options.ConnectionString);
        }

        public int GetOrderNumberById(Guid orderId)
        {
            return MakeADONetCall<int>(
                ADONetQueries.OrderNumberByIdQuery, "OrderNumber", 
                new List<(string paramName, object par)> { ("@orderId", orderId) });
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
        }

        private T MakeADONetCall<T>(string query, string columnName, List<(string paramName, object par)> queryParams)
        {
            object value = new object();

            using (var conn = new SqlConnection(options.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, conn);
                queryParams.ForEach(par => command.Parameters.AddWithValue(par.paramName, par.par));
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        value = reader[columnName];
                    }
                }
                finally
                {
                    reader.Close();
                }

            }

            return (T)value;
        }

        public override void Dispose()
        {
            Console.WriteLine("Test");
            base.Dispose();
        }
    }
}
