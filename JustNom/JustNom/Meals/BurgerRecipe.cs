// BurgerRecipe.cs
using System;
using System.Collections.Generic;

namespace JustNom.Meals
{
    // Represents a burger recipe with name, garnishes, and price
    public class BurgerRecipe
    {
        public string Name { get; set; }
        public List<string> Garnishes { get; set; }
        public int Price { get; set; }

        public BurgerRecipe(string name, List<string> garnishes, int price)
        {
            Name = name;
            Garnishes = garnishes;
            Price = price;
        }
    }
}