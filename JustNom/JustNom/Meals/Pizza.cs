// Pizza.cs
using System;
using System.Collections.Generic;
using JustNom.Manager;

namespace JustNom.Meals
{
    // Represents a pizza with toppings
    public class Pizza : FoodItem
    {
        public List<string> Toppings { get; private set; }
        public List<string> AvailableToppings { get; private set; }
        private OrderManager manager;

        // Constructor for Pizza class
        public Pizza(string name, List<string> toppings, int basePrice, List<string> availableToppings, OrderManager manager)
            : base(name, basePrice)
        {
            Toppings = new List<string>(toppings);
            AvailableToppings = new List<string>(availableToppings);
            Price = basePrice;
            this.manager = manager;
        }

        // Calculates the price of the pizza with its toppings
        public override int CalculatePrice()
        {
            int toppingsPrice = Toppings.Sum(topping => manager.FoodItems.OfType<Topping>().FirstOrDefault(t => t.Name == topping)?.Price ?? 0);
            return BasePrice + toppingsPrice;
        }

        // Adds a topping to the pizza
        public void AddTopping(string topping, int price)
        {
            if (!Toppings.Contains(topping))
            {
                Toppings.Add(topping);
                Price += price;
            }
        }

        // Removes a topping from the pizza
        public void RemoveTopping(string topping)
        {
            Toppings.Remove(topping);
        }
    }
}