using JustNom.Manager;

namespace JustNom.Menus
{
    // Main console menu for the application
    public class MainConsoleMenu : ConsoleMenu
    {
        private OrderManager manager;

        public MainConsoleMenu(OrderManager manager)
        {
            this.manager = manager;
            AddMenuItem(new PlaceNewOrderMenuItem(manager));
            AddMenuItem(new DisplayAllOrdersItem(manager));
            AddMenuItem(new OutputTotalValueItem(manager));
            AddMenuItem(new SaveOrdersToFileItem(manager));
            AddMenuItem(new ExitMenuItem(this));
            Title = "Ify's Fast Food Emporium";
        }

        public override void Execute()
        {
            Show();
        }
    }
}