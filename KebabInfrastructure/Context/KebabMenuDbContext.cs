using KebabCore.Entities.Menu;
using KebabCore.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace KebabInfrastructure.Context
{
    public class KebabMenuDbContext : DbContext
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        //public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=DESKTOP-AH54VTK;Initial Catalog=KebabDB;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<MenuItem>()
            //    .HasOne<KebabMenu>();
            modelBuilder.Entity<Menu>()
                .HasMany(p => p.MenuItems)
                .WithOne(c => c.Menu).IsRequired();

        }
    }
}
