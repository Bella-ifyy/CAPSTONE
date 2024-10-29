// Burger.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using JustNom.Manager;

namespace JustNom.Meals
{
    // Represents a burger with garnishes
    public class Burger : FoodItem
    {
        public List<string> Garnishes { get; private set; }
        public List<string> AvailableGarnishes { get; private set; }

        private OrderManager manager;

        // Constructor for Burger class
        public Burger(string name, List<string> garnishes, int basePrice, List<string> availableGarnishes, OrderManager manager)
            : base(name, basePrice)
        {
            Garnishes = new List<string>(garnishes);
            AvailableGarnishes = new List<string>(availableGarnishes);
            Price = basePrice;
            this.manager = manager;
        }

        // Calculates the price of the burger with its garnishes
        public override int CalculatePrice()
        {
            int garnishesPrice = Garnishes.Sum(garnish => manager.FoodItems.OfType<Garnish>().FirstOrDefault(g => g.Name == garnish)?.Price ?? 0);
            return BasePrice + garnishesPrice;
        }

        // Adds a garnish to the burger
        public void AddGarnish(string garnish, int price)
        {
            if (!Garnishes.Contains(garnish))
            {
                Garnishes.Add(garnish);
                Price += price;
            }
        }

        // Removes a garnish from the burger
        public void RemoveGarnish(string garnish)
        {
            Garnishes.Remove(garnish);
        }
    }
}