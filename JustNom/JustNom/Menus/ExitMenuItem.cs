using System;

namespace JustNom.Menus
{
    // Menu item for exiting the application
    public class ExitMenuItem : MenuItem
    {
        private ConsoleMenu parentMenu;

        public ExitMenuItem(ConsoleMenu parentMenu)
        {
            this.parentMenu = parentMenu;
            Title = "Exit"; // Set the title
        }

        public override string Title { get; protected set; }

        public override void Execute()
        {
            parentMenu.IsActive = false;
        }

        public override string MenuText()
        {
            return "Exit the application";
        }

        internal override void Select()
        {
            Execute();
        }
    }
}
