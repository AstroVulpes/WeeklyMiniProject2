using System.Globalization;
using System.Text;
using System.Linq;

namespace WeeklyMiniProject2
{
    internal partial class Program
    {
        private static List<Product> products = new List<Product>(); // list to store all the products

        private static bool exit1 = false; // simple program exit
        private static bool exit2 = false; // advanced program exit

        private static void AddProduct(bool advanced = false) // Adds a product to the list "products" after the user has inputed Category, Name, and Price;
                                                              // the bool is merely there to ensure that the right exit bool is activated.
        {
            string category = "";
            while (category == "") // Loops as long as the input is invalid.
            {
                Console.Write("Enter a Category: ");
                category = Console.ReadLine();
                if (category.ToLower() == "q") // Exits as soon as Q is pressed, case insensitive.
                {
                    if (advanced) // The actual exit logic; immediately quits the method as soon as it is registered.
                        exit2 = true;
                    else
                        exit1 = true;
                    Console.WriteLine(new string('-', 59));
                    return;
                }
                else if (category == "") // Error message is thrown if the string is empty.
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Category cannot be empty. ");
                    Console.ResetColor();
                }
            }

            if (category.Length > 19)
            {
                category = category.Substring(0, 18) + ".";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Category too long, truncated. ");
                Console.ResetColor();
            }

            string name = "";
            while (name == "")
            {
                Console.Write("Enter a Product Name: ");
                name = Console.ReadLine();
                if (name.ToLower() == "q")
                {
                    if (advanced)
                        exit2 = true;
                    else
                        exit1 = true;
                    Console.WriteLine(new string('-', 59));
                    return;
                }
                else if (name == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Product Name cannot be empty. ");
                    Console.ResetColor();
                }
            }

            if (name.Length > 21)
            {
                name = name.Substring(0, 20) + ".";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Name too long, truncated. ");
                Console.ResetColor();
            }

            string p = "";
            double price = 0;
            while (!double.TryParse(p, out price) && p == "") 
            {
                Console.Write("Enter a Price: ");
                p = Console.ReadLine();
                if (p.ToLower() == "q")
                {
                    if (advanced)
                        exit2 = true;
                    else
                        exit1 = true;
                    Console.WriteLine(new string('-', 59));
                    return;
                }
                else if (!double.TryParse(p, out price)) // Error message is thrown if the input cannot be parsed as a double i.e. a number with optional decimals.
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Price needs to be a number. ");
                    Console.ResetColor();
                }
                else if (p.Length > (17 - 3)) // Error message is thrown if the input is too long.
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Price is too large. ");
                    Console.ResetColor();
                    p = "";
                }
            }

            products.Add(new Product(category, name, price)); // A new Product object is created with the inputed variables and it is added to the list.

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("The product was successfully added! "); // Confirmation message.
            Console.ResetColor();
        }

        private static void ListProducts() // This method sorts all the products in descending order by Price and then prints them out.
        {
            products.Sort((p, q) => p.Price.CompareTo(q.Price));

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("{0,-20}{1,-22}{2,-17}", "Category", "Product", "Price"); // The numbers in curly braces indicate how much each string should be padded (column width);
                                                                                        // a negative numbers aligns left and a positive aligns right.
            Console.ResetColor();
            
            foreach (Product product in products) // Iterate through the list and print out Category, Name, and Price.
            {
                Console.WriteLine("{0,-20}{1,-22}{2,17}", product.Category, product.Name, product.Price.ToString("c2", new CultureInfo("sv-SE"))); // Outputs the Price in the format
                                                                                                                                                   // "1 234,56 kr", aligned right.
            }
            double total = products.Sum(prod => prod.Price); // LINQ sum of prices

            Console.WriteLine();
            Console.WriteLine("{0,-20}{1,-22}{2,17}", "", "Total amount:", total.ToString("c2", new CultureInfo("sv-SE")));
        }

        private static void SearchProducts() // Method searches through product list by Name.
        {
            string name = "";
            while (name == "")
            {
                Console.Write("Enter a Product Name: ");
                name = Console.ReadLine();
                if (name.ToLower() == "q")
                {
                    exit2 = true;
                    Console.WriteLine(new string('-', 59));
                    return;
                }
                else if (name == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Product Name cannot be empty. ");
                    Console.ResetColor();
                }
            }

            if (name.Length > 21)
            {
                name = name.Substring(0, 20) + ".";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Name too long, truncated. ");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("{0,-20}{1,-22}{2,-17}", "Category", "Product", "Price");
            Console.ResetColor();

            IEnumerable<Product> productQuery = // Selects all products where the Name matches the input.
                from product in products
                where product.Name == name
                select product;

            // Alternatively, IEnumerable<Product> productQuery = products.Where(p => p.Name == name);

            foreach (Product product in products)
            {
                //if (product.Name == name)
                if (productQuery.Contains(product))
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("{0,-20}{1,-22}{2,17}", product.Category, product.Name, product.Price.ToString("c2", new CultureInfo("sv-SE")));
                //if (product.Name == name)
                if (productQuery.Contains(product))
                    Console.ResetColor();
            }
        }

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (!exit1) // First loop: Simple program
            {
                Console.WriteLine(new string('-', 59));
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("To enter a new product - follow the steps | To quit - enter: \"Q\"");
                Console.ResetColor();
                AddProduct();
            }

            ListProducts();

            while (!exit2) // Second loop: advanced program
            {
                Console.WriteLine(new string('-', 59));
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("To enter a new product - enter: \"P\" | To search for a product - enter: \"S\" | To quit - enter: \"Q\"");
                Console.ResetColor();
                string input = Console.ReadLine();
                if (input.ToLower() == "q")
                {
                    exit2 = true;
                }
                else if (input.ToLower() == "p")
                {
                    AddProduct(advanced: true);
                }
                else if (input.ToLower() == "s")
                {
                    SearchProducts();
                }
            }

            ListProducts();
            Console.WriteLine(new string('-', 59));

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Thank you and goodbye. Press any key to close... ");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}