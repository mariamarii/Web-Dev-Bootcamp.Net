
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp4.Data;
using ConsoleApp4.Models;

namespace ConsoleApp4;

class Program
{
    static async Task Main()
    {
        try
        {
            using var context = new EFTaskContext(
                new DbContextOptionsBuilder<EFTaskContext>()
                    .UseSqlServer("Server=DESKTOP-5T2IME1;Database=EFTask;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;")
                    .Options);

            await context.Database.EnsureCreatedAsync();
            Console.WriteLine("Successfully connected to EFTask");

            Console.WriteLine("--- Cleaning Up Existing Data ---");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Categories");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Products");
            Console.WriteLine("[Cleanup] Deleted all products and categories");

            Console.WriteLine("\n=== EF Task Demo ===\n");

            Console.WriteLine("--- Managing Categories ---");

            // Add categories
            await context.Categories.AddRangeAsync(
                new Category { Name = "Electronics" },
                new Category { Name = "Books" },
                new Category { Name = "Clothing" }
            );
            await context.SaveChangesAsync();
            Console.WriteLine("[EF Core] Added categories");

            // Retrieve all categories
            var categories = await context.Categories
                .Select(c => new Category { CategoryId = c.CategoryId, Name = c.Name })
                .ToListAsync();
            Console.WriteLine($"[EF Core] Retrieved {categories.Count} categories:");
            foreach (var category in categories)
            {
                Console.WriteLine($"  - ID: {category.CategoryId}, Name: {category.Name}");
            }

            // Retrieve a specific category
            var electronicsCategory = categories.FirstOrDefault(c => c.Name == "Electronics");
            if (electronicsCategory != null)
            {
                var categoryDetails = await context.Categories
                    .Where(c => c.CategoryId == electronicsCategory.CategoryId)
                    .Select(c => new CategoryDetails { CategoryId = c.CategoryId, Name = c.Name })
                    .FirstOrDefaultAsync();
                Console.WriteLine($"[EF Core] Retrieved category: {categoryDetails?.Name}");
            }

            // Update a category
            if (electronicsCategory != null)
            {
                var categoryToUpdate = await context.Categories.FindAsync(electronicsCategory.CategoryId);
                if (categoryToUpdate != null)
                {
                    categoryToUpdate.Name = "Consumer Electronics";
                    await context.SaveChangesAsync();
                    Console.WriteLine($"[EF Core] Updated category ID {electronicsCategory.CategoryId}");
                }
            }

         
            Console.WriteLine("\n--- Managing Products ---");

            // Add products
            if (electronicsCategory != null)
            {
                await context.Products.AddRangeAsync(
                    new Product { Name = "Gaming Laptop", Price = 1299.99m, CategoryId = electronicsCategory.CategoryId },
                    new Product { Name = "Wireless Mouse", Price = 29.99m, CategoryId = electronicsCategory.CategoryId },
                    new Product { Name = "Mechanical Keyboard", Price = 89.99m, CategoryId = electronicsCategory.CategoryId }
                );
                await context.SaveChangesAsync();
                Console.WriteLine("[EF Core] Added products");
            }

            Console.WriteLine("\n--- ProductWithCategory (LINQ instead of View) ---");

            var productsWithCategories = await context.Products
                .Include(p => p.Category)
                .Select(p => new ProductWithCategory
                {
                    ProductId = p.ProductId,
                    ProductName = p.Name,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category!.Name
                })
                .ToListAsync();

            Console.WriteLine($"[EF Core] Retrieved {productsWithCategories.Count} products with categories:");
            foreach (var product in productsWithCategories)
            {
                Console.WriteLine($"  - ID: {product.ProductId}, {product.ProductName} - ${product.Price} (Category: {product.CategoryName})");
            }

            // Retrieve a specific product
            var firstProduct = productsWithCategories.FirstOrDefault();
            if (firstProduct != null)
            {
                var productDetails = await context.Products
                    .Where(p => p.ProductId == firstProduct.ProductId)
                    .Select(p => new ProductDetails { ProductId = p.ProductId, Name = p.Name, Price = p.Price, CategoryId = p.CategoryId })
                    .FirstOrDefaultAsync();
                Console.WriteLine($"[EF Core] Retrieved product: {productDetails?.Name} - ${productDetails?.Price}");
            }

            // Update a product
            if (firstProduct != null)
            {
                var productToUpdate = await context.Products.FindAsync(firstProduct.ProductId);
                if (productToUpdate != null)
                {
                    productToUpdate.Name = "Gaming Laptop Pro";
                    productToUpdate.Price = 1399.99m;
                    await context.SaveChangesAsync();
                    Console.WriteLine($"[EF Core] Updated product ID {firstProduct.ProductId}");
                }
            }

         
            Console.WriteLine("\n--- Function Operations ---");

            if (electronicsCategory != null)
            {
                var productCount = await context.Products
                    .CountAsync(p => p.CategoryId == electronicsCategory.CategoryId);
                Console.WriteLine($"[LINQ] Count of products in {electronicsCategory.Name}: {productCount}");
            }

            var totalPrice = await context.Products
                .SumAsync(p => (decimal?)p.Price) ?? 0.00m;
            Console.WriteLine($"[LINQ] Total price of all products: ${totalPrice}");

           
            Console.WriteLine("\n--- Comprehensive Test ---");

            var updatedProducts = await context.Products
                .Include(p => p.Category)
                .Select(p => new ProductWithCategory
                {
                    ProductId = p.ProductId,
                    ProductName = p.Name,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category!.Name
                })
                .ToListAsync();

            Console.WriteLine("Updated products:");
            foreach (var product in updatedProducts)
            {
                Console.WriteLine($"  - ID: {product.ProductId}, {product.ProductName} - ${product.Price} (Category: {product.CategoryName})");
            }

            var updatedCategories = await context.Categories
                .Select(c => new Category { CategoryId = c.CategoryId, Name = c.Name })
                .ToListAsync();
            Console.WriteLine("Updated categories:");
            foreach (var category in updatedCategories)
            {
                Console.WriteLine($"  - ID: {category.CategoryId}, Name: {category.Name}");
            }

           
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}