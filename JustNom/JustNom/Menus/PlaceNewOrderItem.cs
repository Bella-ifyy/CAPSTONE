using System;
using System.Linq;
using JustNom.Manager;
using JustNom.Meals;

namespace JustNom.Menus
{
    // Menu item for placing a new order
    public class PlaceNewOrderMenuItem : MenuItem
    {
        private OrderManager manager;

        public PlaceNewOrderMenuItem(OrderManager manager)
        {
            this.manager = manager;
            Title = "Place new order";
        }

        public override string Title { get; protected set; }

        public override void Execute()
        {
            Order order = CreateOrder();
            if (order != null)
            {
                bool addingItems = true;
                while (addingItems)
                {
                    Console.WriteLine("Add food to order.");
                    int index = 1;
                    foreach (var item in manager.FoodItems)
                    {
                        Console.WriteLine($"{index}. {item}");
                        index++;
                    }
                    Console.WriteLine($"{index}. Exit");
                    int choice = ConsoleHelpers.GetIntegerInRange(1, index, "Please enter a number:");

                    if (choice == index)
                    {
                        addingItems = false;
                    }
                    else
                    {
                        FoodItem selectedItem = manager.FoodItems[choice - 1];
                        order.AddItem(selectedItem);
                        Console.WriteLine($"{selectedItem.Name} added to the order.");

                        bool customizing = true;
                        while (customizing)
                        {
                            Console.WriteLine($"{selectedItem}");
                            if (selectedItem is Pizza pizza)
                            {
                                Console.WriteLine("1. Remove topping");
                                Console.WriteLine("2. Add topping");
                                Console.WriteLine("3. Exit");
                                int customizeChoice = ConsoleHelpers.GetIntegerInRange(1, 3, "Please enter a number:");

                                if (customizeChoice == 1)
                                {
                                    Console.WriteLine("Available toppings to remove:");
                                    for (int i = 0; i < pizza.Toppings.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {pizza.Toppings[i]}");
                                    }
                                    int removeChoice = ConsoleHelpers.GetIntegerInRange(1, pizza.Toppings.Count, "Select a topping to remove:");
                                    string toppingToRemove = pizza.Toppings[removeChoice - 1];
                                    pizza.RemoveTopping(toppingToRemove);
                                    Console.WriteLine($"{toppingToRemove} removed.");
                                }
                                else if (customizeChoice == 2)
                                {
                                    Console.WriteLine("Available toppings to add:");
                                    Console.WriteLine("1. Tomato sauce");
                                    Console.WriteLine("2. Cheese");
                                    Console.WriteLine("3. Ham");
                                    Console.WriteLine("4. Pepperoni");
                                    int addChoice = ConsoleHelpers.GetIntegerInRange(1, 4, "Select a topping to add:");
                                    string toppingToAdd = "";
                                    int toppingPrice = 0;
                                    switch (addChoice)
                                    {
                                        case 1:
                                            toppingToAdd = "Tomato sauce";
                                            toppingPrice = 40;
                                            break;
                                        case 2:
                                            toppingToAdd = "Cheese";
                                            toppingPrice = 100;
                                            break;
                                        case 3:
                                            toppingToAdd = "Ham";
                                            toppingPrice = 150;
                                            break;
                                        case 4:
                                            toppingToAdd = "Pepperoni";
                                            toppingPrice = 120;
                                            break;
                                    }
                                    pizza.AddTopping(toppingToAdd, toppingPrice);
                                    Console.WriteLine($"{toppingToAdd} added for £{toppingPrice / 100.0:F2}.");
                                }
                                else
                                {
                                    customizing = false;
                                }
                            }
                            else if (selectedItem is Burger burger)
                            {
                                Console.WriteLine("1. Remove garnish");
                                Console.WriteLine("2. Add garnish");
                                Console.WriteLine("3. Exit");
                                int customizeChoice = ConsoleHelpers.GetIntegerInRange(1, 3, "Please enter a number:");

                                if (customizeChoice == 1)
                                {
                                    Console.WriteLine("Available garnishes to remove:");
                                    for (int i = 0; i < burger.Garnishes.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {burger.Garnishes[i]}");
                                    }
                                    int removeChoice = ConsoleHelpers.GetIntegerInRange(1, burger.Garnishes.Count, "Select a garnish to remove:");
                                    string garnishToRemove = burger.Garnishes[removeChoice - 1];
                                    burger.RemoveGarnish(garnishToRemove);
                                    Console.WriteLine($"{garnishToRemove} removed.");
                                }
                                else if (customizeChoice == 2)
                                {
                                    Console.WriteLine("Available garnishes to add:");
                                    Console.WriteLine("1. Cheese");
                                    Console.WriteLine("2. Fried onions");
                                    int addChoice = ConsoleHelpers.GetIntegerInRange(1, 2, "Select a garnish to add:");
                                    string garnishToAdd = "";
                                    int garnishPrice = 0;
                                    switch (addChoice)
                                    {
                                        case 1:
                                            garnishToAdd = "Cheese";
                                            garnishPrice = 100;
                                            break;
                                        case 2:
                                            garnishToAdd = "Fried onions";
                                            garnishPrice = 80;
                                            break;
                                    }
                                    burger.AddGarnish(garnishToAdd, garnishPrice);
                                    Console.WriteLine($"{garnishToAdd} added for £{garnishPrice / 100.0:F2}.");
                                }
                                else
                                {
                                    customizing = false;
                                }
                            }
                        }
                    }
                }

                manager.Orders.Add(order);
                Console.WriteLine(order.ToString());
            }
        }

        public override string MenuText()
        {
            return "Place a new order";
        }

        internal override void Select()
        {
            Execute();
        }

        // Creates a new order based on user input
        private Order CreateOrder()
        {
            Console.WriteLine("Please enter customer name:");
            string customerName = Console.ReadLine();
            Console.WriteLine("Is this delivery? (Yes/No)");
            bool isDelivery = string.Equals(Console.ReadLine(), "yes", StringComparison.OrdinalIgnoreCase);

            string address = "";
            if (isDelivery)
            {
                Console.WriteLine("Please enter the delivery address:");
                address = Console.ReadLine();
            }

            return new Order(customerName, isDelivery, address);
        }
    }
}
