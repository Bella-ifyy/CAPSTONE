using System;
using JustNom.Manager;

namespace JustNom.Menus
{
    // Menu item for outputting the total value of all orders
    public class OutputTotalValueItem : MenuItem
    {
        private OrderManager manager;

        public OutputTotalValueItem(OrderManager manager)
        {
            this.manager = manager;
            Title = "Output Total Value";
        }

        public override string Title { get; protected set; }

        public override void Execute()
        {
            int totalValue = manager.Orders.Sum(order => order.TotalPrice());
            Console.WriteLine($"Total Value of All Orders: £{totalValue / 100.0:F2}");
        }

        public override string MenuText()
        {
            return "Output Total Value";
        }

        internal override void Select()
        {
            Execute();
        }
    }
}
