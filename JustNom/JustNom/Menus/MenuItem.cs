using System;

namespace JustNom.Menus
{
    // Abstract base class for menu items
    public abstract class MenuItem
    {
        public abstract string Title { get; protected set; }
        public abstract void Execute();
        public abstract string MenuText();
        internal abstract void Select();
    }
}
