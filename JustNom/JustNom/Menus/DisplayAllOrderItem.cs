using System;
using JustNom.Manager;
using JustNom.Meals;

namespace JustNom.Menus
{
    // Menu item for displaying all orders
    public class DisplayAllOrdersItem : MenuItem
    {
        private OrderManager manager;

        public DisplayAllOrdersItem(OrderManager manager)
        {
            this.manager = manager;
            Title = "Display all orders";
        }

        public override string Title { get; protected set; }

        public override void Execute()
        {
            Console.WriteLine("Display all orders");

            if (manager.Orders.Count == 0)
            {
                Console.WriteLine("No orders to display.");
                return;
            }

            for (int i = 0; i < manager.Orders.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Order for {manager.Orders[i].CustomerName} £{manager.Orders[i].TotalPrice() / 100.0:F2}");
            }

            int choice = ConsoleHelpers.GetIntegerInRange(1, manager.Orders.Count + 1, "Please enter a number:");

            if (choice <= manager.Orders.Count)
            {
                Order selectedOrder = manager.Orders[choice - 1];
                Console.WriteLine(selectedOrder.ToString());
            }
        }

        public override string MenuText()
        {
            return "Display all orders";
        }

        internal override void Select()
        {
            Execute();
        }
    }
}