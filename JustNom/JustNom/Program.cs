// Program.cs
using System;
using JustNom.Manager;
using JustNom.Menus;

namespace JustNom
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderManager manager = new OrderManager();
            manager.SelectRestaurant();
            manager.OutputTakeawayShopMenu();
            ConsoleMenu mainMenu = new MainConsoleMenu(manager);
            mainMenu.Show();
        }
    }
}