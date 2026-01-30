using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlazorServerApp.Data;
using BlazorServerApp.Models;

namespace BlazorServerApp.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Ensure database exists
            await db.Database.EnsureCreatedAsync();

            if (!db.Providers.Any())
            {
                db.Providers.AddRange(
                    new Provider { CompanyName = "ACME Supplies", ContactFirstName = "John", ContactLastName = "Doe", Phone = "555-0100" },
                    new Provider { CompanyName = "Global Traders", ContactFirstName = "Ana", ContactLastName = "Smith", Phone = "555-0200" }
                );
            }

            if (!db.Products.Any())
            {
                db.Products.AddRange(
                    new Product { Code = "P100", Name = "Widget A", UnitPrice = 9.99m, UnitsInStock = 100, QuantityPerUnit = 1 },
                    new Product { Code = "P200", Name = "Gadget B", UnitPrice = 19.50m, UnitsInStock = 50, QuantityPerUnit = 1 }
                );
            }

            await db.SaveChangesAsync();
        }
    }
}