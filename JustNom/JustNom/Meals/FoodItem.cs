// FoodItem.cs
using System;

namespace JustNom.Meals
{
    // Abstract base class for food items
    public abstract class FoodItem
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int BasePrice { get; set; }

        // Constructor for FoodItem
        public FoodItem(string name, int basePrice)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));
            if (basePrice < 0)
                throw new ArgumentException("Base price cannot be negative", nameof(basePrice));

            Name = name;
            BasePrice = basePrice;
        }

        // Abstract method to calculate price
        public abstract int CalculatePrice();

        public override string ToString() => $"{Name} - £{BasePrice / 100.0:F2}";
    }
}