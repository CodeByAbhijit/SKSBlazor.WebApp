using BlazorServerApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlazorServerApp.Data
{
    public class AppDbContext : DbContext // Fix: Inherit from DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } // Fix: Specify the generic type parameter for DbContextOptions

        public DbSet<Provider> Providers => Set<Provider>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<OrderReception> OrderReceptions => Set<OrderReception>();
        public DbSet<OrderLine> OrderLines => Set<OrderLine>();
        public DbSet<StockLog> StockLogs => Set<StockLog>();
    }
}
