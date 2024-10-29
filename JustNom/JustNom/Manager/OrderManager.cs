// OrderManager.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JustNom.Meals;

namespace JustNom.Manager
{
    // Manages orders, food items, and menu operations
    public class OrderManager
    {
        public List<FoodItem> FoodItems { get; set; } = new List<FoodItem>();
        public List<PizzaRecipe> Pizzas { get; set; } = new List<PizzaRecipe>();
        public List<BurgerRecipe> Burgers { get; set; } = new List<BurgerRecipe>();
        public List<Order> Orders { get; private set; } = new List<Order>();
        public string ShopName { get; set; }

        // Constructor that loads the menu from a file
        public OrderManager()
        {
            LoadMenuFromFile("menu.txt");
        }

        // Adds a food item to the list
        public void AddFoodItem(FoodItem item)
        {
            FoodItems.Add(item);
        }

        // Method for selecting the restaurant
        public void SelectRestaurant()
        {
            List<string> restaurantNames = new List<string> { "Ify's Fast Food Emporium" };
            Console.WriteLine("Select a restaurant:");
            for (int i = 0; i < restaurantNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {restaurantNames[i]}");
            }

            Console.Write("Enter the number of the restaurant to select: ");
            string input = Console.ReadLine();
            int selection;
            if (!int.TryParse(input, out selection) || selection <= 0 || selection > restaurantNames.Count)
            {
                selection = 1;
            }

            ShopName = restaurantNames[selection - 1];
            LoadMenuFromFile("menu.txt");
        }

        // Loads the menu from a specified file
        public void LoadMenuFromFile(string fileName)
        {
            string filePath = Path.Combine("Data", fileName);

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                FoodItems.Clear();
                Pizzas.Clear();
                Burgers.Clear();

                foreach (string line in lines.Select(l => l.Trim()))
                {
                    if (line.StartsWith("Name:"))
                    {
                        ShopName = line.Substring(5).Trim();
                    }
                    else if (line.StartsWith("Toppings:") || line.StartsWith("Garnishes:"))
                    {
                        int startIndex = line.IndexOf("[");
                        int endIndex = line.IndexOf("]");
                        if (startIndex >= 0 && endIndex >= 0 && endIndex > startIndex)
                        {
                            string itemsData = line.Substring(startIndex + 1, endIndex - startIndex - 1);
                            string[] items = itemsData.Split(',');
                            foreach (string item in items)
                            {
                                string[] itemParts = item.Trim('<', '>').Split(',');
                                if (itemParts.Length == 2)
                                {
                                    string name = itemParts[0].Trim();
                                    if (int.TryParse(itemParts[1].Trim(), out int price))
                                    {
                                        if (line.StartsWith("Toppings:"))
                                        {
                                            FoodItems.Add(new Topping(name, price));
                                        }
                                        else if (line.StartsWith("Garnishes:"))
                                        {
                                            FoodItems.Add(new Garnish(name, price));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (line.StartsWith("Pizza:"))
                    {
                        ParsePizza(line);
                    }
                    else if (line.StartsWith("Burger:"))
                    {
                        ParseBurger(line);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Menu file not found: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error parsing menu file: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading menu file: {ex.Message}");
            }
        }

        // Parses a pizza line from the menu file
        private void ParsePizza(string line)
        {
            string pizzaData = line.Substring("Pizza:".Length).Trim('<', '>');
            var parts = pizzaData.Split(new[] { ",Toppings:[" }, StringSplitOptions.None);
            string name = parts[0].Substring("Name:".Length).Trim();
            string toppingsPart = parts[1];
            var toppings = toppingsPart.Substring(0, toppingsPart.LastIndexOf("],Price:")).Split(',').Select(t => t.Trim()).ToList();
            int price = int.Parse(toppingsPart.Substring(toppingsPart.LastIndexOf("Price:") + "Price:".Length).Trim());

            List<string> availableToppings = FoodItems.OfType<Topping>()
                .Select(t => t.Name)
                .Except(toppings)
                .ToList();
            Pizzas.Add(new PizzaRecipe(name, toppings, price));
            FoodItems.Add(new Pizza(name, toppings, price, availableToppings, this));
        }

        // Parses a burger line from the menu file
        private void ParseBurger(string line)
        {
            string burgerData = line.Substring("Burger:".Length).Trim('<', '>');
            var parts = burgerData.Split(new[] { ",Garnishes:[" }, StringSplitOptions.None);
            string name = parts[0].Substring("Name:".Length).Trim();
            string garnishesPart = parts[1];
            List<string> garnishes = garnishesPart.Substring(0, garnishesPart.LastIndexOf("],Price:"))
                                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(g => g.Trim()).ToList();
            int price = int.Parse(garnishesPart.Substring(garnishesPart.LastIndexOf("Price:") + "Price:".Length).Trim());

            List<string> availableGarnishes = FoodItems.OfType<Garnish>()
                .Select(g => g.Name)
                .Except(garnishes)
                .ToList();
            Burgers.Add(new BurgerRecipe(name, garnishes, price));
            FoodItems.Add(new Burger(name, garnishes, price, availableGarnishes, this));
        }

        // Outputs the takeaway shop menu to the console
        public void OutputTakeawayShopMenu()
        {
            Console.WriteLine($"{ShopName} - Takeaway Shop Menu:");
            Console.WriteLine("Pizzas:");
            foreach (var pizza in Pizzas)
            {
                Console.WriteLine($"- {pizza.Name}: £{pizza.Price / 100.0:F2}");
                Console.WriteLine($"  Toppings: {string.Join(", ", pizza.Toppings)}");
            }
            Console.WriteLine("Burgers:");
            foreach (var burger in Burgers)
            {
                Console.WriteLine($"- {burger.Name}: £{burger.Price / 100.0:F2}");
                Console.WriteLine($"  Garnishes: {string.Join(", ", burger.Garnishes)}");
            }
        }

        // Outputs the items in a specific order to the console
        public void OutputOrderItems(Order order)
        {
            Console.WriteLine($"Order Items for {order.CustomerName}:");
            foreach (var item in order.Items)
            {
                Console.WriteLine($"- {item}");
            }
        }

        // Outputs the total value of all orders to the console
        public void OutputTotalValueOfAllOrders()
        {
            int totalValue = Orders.Sum(order => order.TotalPrice());
            Console.WriteLine($"Total Value of All Orders: £{totalValue / 100.0:F2}");
        }
    }
}