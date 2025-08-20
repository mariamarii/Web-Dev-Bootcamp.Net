
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

class ShopDemo
{
    static async Task Main()
    {
        try
        {
            string connectionString = "Server=DESKTOP-5T2IME1;Database=task_dapper;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;";
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            Console.WriteLine("Successfully connected to task_dapper");

            

            Console.WriteLine("\n=== Task Dapper Demo ===\n");

         
            Console.WriteLine("--- Managing Categories ---");

            // Add categories
            await connection.ExecuteAsync("sp_AddCategory", new { Name = "Electronics" }, commandType: CommandType.StoredProcedure);
            await connection.ExecuteAsync("sp_AddCategory", new { Name = "Books" }, commandType: CommandType.StoredProcedure);
            await connection.ExecuteAsync("sp_AddCategory", new { Name = "Clothing" }, commandType: CommandType.StoredProcedure);
            Console.WriteLine("[Stored Procedure] Added categories using sp_AddCategory");

            // Retrieve all categories
            var categories = await connection.QueryAsync<Category>("sp_GetCategories", commandType: CommandType.StoredProcedure);
            Console.WriteLine($"[Stored Procedure] sp_GetCategories returned {categories.Count()} categories:");
            foreach (var category in categories)
            {
                Console.WriteLine($"  - ID: {category.CategoryId}, Name: {category.CategoryName}");
            }

            // Retrieve a specific category
            var electronicsCategory = categories.FirstOrDefault(c => c.CategoryName == "Electronics");
            if (electronicsCategory != null)
            {
                var categoryDetails = await connection.QueryFirstOrDefaultAsync<CategoryDetails>(
                    "GetCategoryById",
                    new { CategoryId = electronicsCategory.CategoryId },
                    commandType: CommandType.StoredProcedure);
                Console.WriteLine($"[Stored Procedure] GetCategoryById returned: {categoryDetails?.CategoryName}");
            }

            // Update a category
            if (electronicsCategory != null)
            {
                await connection.ExecuteAsync("sp_UpdateCategory",
                    new { CategoryId = electronicsCategory.CategoryId, Name = "Consumer Electronics" },
                    commandType: CommandType.StoredProcedure);
                Console.WriteLine($"[Stored Procedure] Updated category ID {electronicsCategory.CategoryId} using sp_UpdateCategory");
            }

       
            Console.WriteLine("\n--- Managing Products ---");

            // Add products
            await connection.ExecuteAsync("sp_AddProduct",
                new { ProductName = "Gaming Laptop", Price = 1299.99m, CategoryId = electronicsCategory?.CategoryId },
                commandType: CommandType.StoredProcedure);
            await connection.ExecuteAsync("sp_AddProduct",
                new { ProductName = "Wireless Mouse", Price = 29.99m, CategoryId = electronicsCategory?.CategoryId },
                commandType: CommandType.StoredProcedure);
            await connection.ExecuteAsync("Sp_AddProduct",
                new { ProductName = "Mechanical Keyboard", Price = 89.99m, CategoryId = electronicsCategory?.CategoryId },
                commandType: CommandType.StoredProcedure);
            Console.WriteLine("[Stored Procedure] Added products using sp_AddProduct and Sp_AddProduct");

            // Retrieve all products
            var products = await connection.QueryAsync<ProductWithCategory>("sp_GetProducts", commandType: CommandType.StoredProcedure);
            Console.WriteLine($"[Stored Procedure] sp_GetProducts returned {products.Count()} products:");
            foreach (var product in products)
            {
                Console.WriteLine($"  - ID: {product.ProductId}, {product.ProductName} - ${product.Price} (Category: {product.CategoryName})");
            }

            // Retrieve a specific product
            var firstProduct = products.FirstOrDefault();
            if (firstProduct != null)
            {
                var productDetails = await connection.QueryFirstOrDefaultAsync<ProductDetails>(
                    "GetProductById",
                    new { ProductId = firstProduct.ProductId },
                    commandType: CommandType.StoredProcedure);
                Console.WriteLine($"[Stored Procedure] GetProductById returned: {productDetails?.ProductName} - ${productDetails?.Price}");
            }

            // Update a product
            if (firstProduct != null)
            {
                await connection.ExecuteAsync("sp_UpdateProduct",
                    new
                    {
                        ProductId = firstProduct.ProductId,
                        ProductName = "Gaming Laptop Pro",
                        Price = 1399.99m,
                        CategoryId = firstProduct.CategoryId
                    },
                    commandType: CommandType.StoredProcedure);
                Console.WriteLine($"[Stored Procedure] Updated product ID {firstProduct.ProductId} using sp_UpdateProduct");
            }

          
            Console.WriteLine("\n--- View Operations ---");

            var productsFromView = await connection.QueryAsync<ProductWithCategory>(
                "SELECT ProductId, ProductName, Price, CategoryName, CategoryId FROM vw_ProductWithCategory"
            );
            Console.WriteLine($"[View] vw_ProductWithCategory returned {productsFromView.Count()} products:");
            foreach (var product in productsFromView)
            {
                Console.WriteLine($"  - ID: {product.ProductId}, {product.ProductName} - ${product.Price} (Category: {product.CategoryName})");
            }

            Console.WriteLine("\n--- Function Operations ---");

            // Count products in a category
            if (electronicsCategory != null)
            {
                var productCount = await connection.ExecuteScalarAsync<int>(
                    "SELECT dbo.fn_CountProductsByCategory(@CategoryId)",
                    new { CategoryId = electronicsCategory.CategoryId }
                );
                Console.WriteLine($"[Function] fn_CountProductsByCategory returned: {productCount} products in Electronics");
            }

          

        
            Console.WriteLine("\n--- Comprehensive Test ---");

            // Show updated products
            var updatedProducts = await connection.QueryAsync<ProductWithCategory>("sp_GetProducts", commandType: CommandType.StoredProcedure);
            Console.WriteLine("Updated products from sp_GetProducts:");
            foreach (var product in updatedProducts)
            {
                Console.WriteLine($"  - ID: {product.ProductId}, {product.ProductName} - ${product.Price} (Category: {product.CategoryName})");
            }

            // Show updated categories
            var updatedCategories = await connection.QueryAsync<Category>("sp_GetCategories", commandType: CommandType.StoredProcedure);
            Console.WriteLine("Updated categories from sp_GetCategories:");
            foreach (var category in updatedCategories)
            {
                Console.WriteLine($"  - ID: {category.CategoryId}, Name: {category.CategoryName}");
            }

          

            
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Error: {ex.Message}");
            Console.WriteLine($"Error Number: {ex.Number}, State: {ex.State}, Class: {ex.Class}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}

// Data Transfer Objects (DTOs)
public class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = "";
}

public class CategoryDetails
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = "";
}

public class ProductWithCategory
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = "";
    public int CategoryId { get; set; }
}

public class ProductDetails
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
s