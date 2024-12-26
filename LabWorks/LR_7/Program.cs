using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1;

[Table("ProductCategory")]
public class ProductCategory
{
    [Column("Id")]
    public int Id { get; set; }
    [Column("Name")]
    public string Name { get; set; }
    public List<Product> Products { get; set; } = new();
}

[Table("Product")]
public class Product
{
    [Column("Id")]
    public int Id { get; set; }
    [Column("Name")]
    public string Name { get; set; }
    [Column("Price")]
    public int Price { get; set; }
    [Column("CategoryID")]
    public int CategoryID { get; set; }
    public ProductCategory Category { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<Product> products { get; set; }
    public DbSet<ProductCategory> categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=Shop;username=postgres;password=qwerty123;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.CategoryID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        using var context = new AppDbContext();
        context.Database.EnsureCreated();

        // 1. CREATE
        Console.WriteLine("=== CREATE ===");
        var c1 = new ProductCategory { Name = "Milk" };
        var p1 = new Product { Name = "double_milk", Price = 99 };
        var p2 = new Product { Name = "cheese", Price = 250 };
        var p3 = new Product { Name = "butter", Price = 129 };

        var c2 = new ProductCategory { Name = "Meat" };
        var p4 = new Product { Name = "pork", Price = 560 };
        var p5 = new Product { Name = "chicken", Price = 340 };

        context.categories.Add(c1);
        context.products.AddRange(p1, p2, p3);

        context.categories.Add(c2);
        context.products.AddRange(p4, p5);
        context.SaveChanges();

        Console.WriteLine("\nВсё создано корректно!");


        // 2. READ
        Console.WriteLine("\n=== READ ===");
        var prods = context.categories.Include(p => p.Name);
        foreach (var prod in prods)
        {
            Console.WriteLine($"Category: {prod.Name}");
            foreach (var p in prod.Products)
            {
                Console.WriteLine($"   product and price: {p.Name} - {p.Price}$");
            }
        }

        // 3. UPDATE
        Console.WriteLine("\n=== UPDATE ===");
        var NewProduct = context.products.FirstOrDefault(p => p.Name == "cheese");
        if (NewProduct != null)
        {
            NewProduct.Name = "Blue cheese";
            context.SaveChanges();
            Console.WriteLine("\nИзменения прошли успешно!");
        }

        // 4. DELETE
        Console.WriteLine("\n=== DELETE ===");
        var DelProduct = context.products.Include(p => p.Name).FirstOrDefault(p => p.Name == "Blue cheese");
        if (DelProduct != null)
        {
            context.products.Remove(DelProduct);
            context.SaveChanges();
            Console.WriteLine("Продукт успешно удалён!");
        }
    }
}