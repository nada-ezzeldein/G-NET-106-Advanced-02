using System.Collections;
using System.Drawing;
using System.Xml.Linq;

namespace C__Advanced_Assignmen_2
{
    internal class Program
    {
        #region Product Model 
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } // "Electronics", "Clothing", "Food", "Books" 
        public double Price { get; set; }
        public int Stock { get; set; }
     
            public override string ToString()
        {
            return $"{Name} - ${Price} (Stock: {Stock})";
        }
        }

       
        #endregion

        #region Product Catalog  
        static List<Product> catalog = new()
        {
    new Product { Id=1, Name="Laptop", Category="Electronics", Price=1200, Stock=10 },
    new Product { Id=2,Name="Phone", Category="Electronics", Price=800, Stock=25 },
    new Product { Id=3, Name="T-Shirt", Category="Clothing", Price=30, Stock=100 },
    new Product { Id=4, Name="Jeans", Category="Clothing", Price=60, Stock=50 },
    new Product { Id=5, Name="Chocolate", Category="Food", Price=5, Stock=200 },
    new Product { Id=6, Name="Coffee Beans", Category="Food", Price=15, Stock=80 },
    new Product { Id=7, Name="C# Book", Category="Books", Price=45, Stock=30 },
    new Product { Id=8, Name="Novel", Category="Books", Price=20, Stock=60 },
    new Product { Id=9,Name="Headphones", Category="Electronics", Price=150, Stock=40 },
    new Product { Id=10, Name="Jacket", Category="Clothing", Price=120, Stock=15 }
};

        #endregion

        #region Smart Product Search  
        public static List<Product> SearchProducts(List<Product> products, Func<Product, bool> filter)
        {
            List<Product> result = new List<Product>();

            foreach (var product in products)
            {
                if (filter(product))
                {
                    result.Add(product);
                }
            }

            return result;
        }


        static void PrintProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();
        }

        #endregion

        #region Custom Report Generator
        public static void PrintReport(List<Product> products, Action<Product> printAction)
        {
            foreach (var product in products)
            {
                printAction(product);
            }
        }
        #endregion

        #region Transform Products
        public static List<TResult> TransformProducts<TResult>(List<Product> products, Func<Product, TResult> transformer)
        {
            List<TResult> result = new List<TResult>();

            foreach (var product in products)
            {
                result.Add(transformer(product));
            }

            return result;
        }
        #endregion

        #region Filter Products
        public static List<Product> FilterProducts(List<Product> products, Predicate<Product> match)
        {
            List<Product> result = new List<Product>();

            foreach (var product in products)
            {
                if (match(product))
                {
                    result.Add(product);
                }
            }

            return result;
        }
        #endregion
        static void Main(string[] args)
        {
        #region Task_1
            Console.WriteLine("--- Electronics ---");
            var electronics = SearchProducts(catalog, p => p.Category == "Electronics");
            PrintProducts(electronics);

            Console.WriteLine("--- Under $50 ---");
            var cheapProducts = SearchProducts(catalog, p => p.Price < 50);
            PrintProducts(cheapProducts);

            Console.WriteLine("--- In Stock ---");
            var inStockProducts = SearchProducts(catalog, p => p.Stock > 0);
            PrintProducts(inStockProducts);

            Console.WriteLine("--- Clothing Under $100 ---");
            var cheapClothing = SearchProducts(catalog, p => p.Category == "Clothing" && p.Price < 100);
            PrintProducts(cheapClothing);
            #endregion

        #region Print Reports 
            Console.WriteLine("--- Short Report ---");
            PrintReport(catalog, p => Console.WriteLine($"{p.Name} - ${p.Price}"));

            Console.WriteLine();

            Console.WriteLine("--- Detailed Report ---");
            PrintReport(catalog, p => Console.WriteLine($"[{p.Category}] {p.Name} | Price: ${p.Price} | Stock: {p.Stock}"));
            #endregion

        #region transform Products
            Console.WriteLine("--- Summary List ---");
            List<string> summaryList = TransformProducts(catalog, p => $"{p.Name} (${p.Price})");
            foreach (var item in summaryList)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            Console.WriteLine("--- Price Labels ---");
            List<string> priceLabels = TransformProducts(catalog, p => $"{p.Name}: {(p.Price > 100 ? "Expensive!" : "Affordable")}");
            foreach (var item in priceLabels)
            {
                Console.WriteLine(item);
            }
            #endregion

        #region Low-Stock Alert
            Console.WriteLine("--- Low Stock Alert ---");
            List<Product> lowStockProducts = FilterProducts(catalog, p => p.Stock < 20);

            foreach (var product in lowStockProducts)
            {
                Console.WriteLine($"[LOW STOCK] {product.Name}: only {product.Stock} left!");
            }
            #endregion

        }
    }  
}
    
