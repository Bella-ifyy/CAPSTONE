using System;
using System.IO;
using JustNom.Manager;
using JustNom.Meals;

namespace JustNom.Menus
{
    // Menu item for saving orders to a file
    public class SaveOrdersToFileItem : MenuItem
    {
        private OrderManager manager;

        public SaveOrdersToFileItem(OrderManager manager)
        {
            this.manager = manager;
            Title = "Save orders to file";
        }

        public override string Title { get; protected set; }

        public override void Execute()
        {
            Console.WriteLine("Enter the file name to save orders (e.g., orders.txt):");
            string fileName = Console.ReadLine();

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var order in manager.Orders)
                    {
                        writer.WriteLine($"Order: {order.CustomerName}, {order.TotalPrice() / 100.0:F2}");
                        foreach (var item in order.Items)
                        {
                            writer.WriteLine($"- {item.Name} - £{item.CalculatePrice() / 100.0:F2}");
                            if (item is Pizza pizza)
                            {
                                writer.WriteLine($"  Toppings: {string.Join(", ", pizza.Toppings)}");
                            }
                            else if (item is Burger burger)
                            {
                                writer.WriteLine($"  Garnishes: {string.Join(", ", burger.Garnishes)}");
                            }
                        }
                        writer.WriteLine();
                    }
                }

                Console.WriteLine($"Orders saved successfully to: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving orders to file: {ex.Message}");
            }
        }

        public override string MenuText()
        {
            return "Save orders to file";
        }

        internal override void Select()
        {
            Execute();
        }
    }
}