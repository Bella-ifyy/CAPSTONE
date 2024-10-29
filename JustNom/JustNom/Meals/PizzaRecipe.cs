// PizzaRecipe.cs
using System;
using System.Collections.Generic;

namespace JustNom.Meals
{
    // Represents a pizza recipe with name, toppings, and price
    public class PizzaRecipe
    {
        public string Name { get; set; }
        public List<string> Toppings { get; set; }
        public int Price { get; set; }

        public PizzaRecipe(string name, List<string> toppings, int price)
        {
            Name = name;
            Toppings = toppings;
            Price = price;
        }
    }
}
