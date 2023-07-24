using System.Linq;

namespace Product_manager
{
    // Product class represents a product with category, name, and price.
    class Product
    {
        public Product(string category, string name, decimal price)
        {
            Category = category;
            Name = name;
            Price = price;
        }

        public string Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    class ProductManager
    {
        // List of all products
        private static readonly List<Product> Products = new List<Product>();

        // Add a new product.
        public void AddProduct(string category, string name, decimal price)
        {
            // Checks if the name is similar to already stored products.
            if (Products.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("A product with the same name already exists. Please enter a unique name.");
                Console.ResetColor();
                Console.WriteLine(new string('-', 50));
                return;
            }

            // Adding successfully!
            Products.Add(new Product(category, name, price));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The product was successfully added!");
            Console.ResetColor();
            Console.WriteLine(new string('-', 50));
        }

        // Method to display all products in the list.
        public void ShowProducts()
        {
            Console.WriteLine(new string('-', 50));

            // Print the header with column names.
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0,-20} {1,-20} {2,-20}", "Category", "Name", "Price");
            Console.ResetColor();

            foreach (var product in Products.OrderBy(p => p.Price))
            {
                Console.WriteLine($"{product.Category,-20} {product.Name,-20} {product.Price,-20}");
            }

            Console.WriteLine(new string('-', 50));

            // Print the total amount of all products' prices.
            Console.WriteLine("{0,-20} {1,-20} {2,-20}", "", "Total amount:", Products.Sum(p => p.Price));
        }

        // Method to add more products interactively.
        public void AddMoreProducts()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("To enter a new product - follow the steps | To quit - Enter Q");
            Console.ResetColor();

            while (true)
            {
                Console.WriteLine("Enter product details (or 'q' to quit adding more products):");
                Console.Write("Category: ");
                string? category = Console.ReadLine();

                if (category?.ToLower() == "q")
                    break;

                string? name = null;
                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.Write("Product Name: ");
                    name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                        Console.WriteLine("Invalid input. Please enter a non-empty product name.");
                }

                if (name != null)
                {
                    decimal price;
                    while (true)
                    {
                        Console.Write("Price: ");
                        if (decimal.TryParse(Console.ReadLine(), out price))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid price. Please enter a valid decimal number.");
                        }
                    }

                    AddProduct(category!, name, price);
                }
            }
        }

        // Search method
        public void SearchProduct(string searchTerm)
        {
            var results = Products
                .Where(p => p.Name.Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Console.WriteLine("\nSearch Results:");

            // Print the header with column names in green.
            Console.WriteLine(new string('-', 50));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0,-20} {1,-20} {2,-20}", "Category", "Name", "Price");
            Console.ResetColor();

            // Mark the search result in purple.
            foreach (var product in Products.OrderBy(p => p.Price))
            {
                if (results.Any(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"{product.Category,-20} {product.Name,-20} {product.Price,-20}");
                }
                else
                {
                    Console.ResetColor();
                    Console.WriteLine($"{product.Category,-20} {product.Name,-20} {product.Price,-20}");
                }
            }

            Console.ResetColor();
            Console.WriteLine(new string('-', 50));
        }
    }

    class Program
    {
        static void Main()
        {
            
            ProductManager manager = new ProductManager();

            // Loop to add more products.
            while (true)
            {
                manager.AddMoreProducts();

                // Display all products in the list.
                manager.ShowProducts();

                Console.Write("Do you want to add more products? (y/n): ");
                string? choice = Console.ReadLine();

                if (choice?.ToLower() != "y")
                    break;
            }

            // Loop to search for products.
            while (true)
            {
                Console.Write("Enter the product name to search (or 'q' to quit searching): ");
                string? searchTerm = Console.ReadLine();

                if (searchTerm != null && searchTerm.ToLower() != "q")
                {
                    manager.SearchProduct(searchTerm);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
