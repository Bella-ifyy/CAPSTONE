//ConsoleMenu.cs
using System;

namespace JustNom.Menus
{
    // Abstract class for console menu
    public abstract class ConsoleMenu : MenuItem
    {
        protected List<MenuItem> menuItems = new List<MenuItem>();
        public bool IsActive { get; set; } = true;

        public override string Title { get; protected set; }

        protected ConsoleMenu()
        {
            Title = "Welcome";
        }

        // Executes the menu
        public override void Execute()
        {
            Show();
        }

        // Returns the menu text
        public override string MenuText()
        {
            return "Please choose an option:";
        }

        // Shows the menu options to the user
        public void Show()
        {
            int selection = 0;
            do
            {
                Console.WriteLine(MenuText());
                for (int i = 0; i < menuItems.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {menuItems[i].MenuText()}");
                }
                Console.Write("Select an option: ");
                if (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > menuItems.Count)
                {
                    Console.WriteLine("Invalid choice, please try again.");
                    continue;
                }

                menuItems[selection - 1].Select();
            } while (IsActive);
        }

        internal override void Select()
        {
            Execute();
        }

        // Adds a menu item to the menu
        public void AddMenuItem(MenuItem item)
        {
            menuItems.Add(item);
        }
    }
}
