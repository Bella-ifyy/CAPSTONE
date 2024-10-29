// Topping.cs
namespace JustNom.Meals
{
    // Represents a topping
    public class Topping : FoodItem
    {
        public Topping(string name, int price) : base(name, price)
        {
        }

        public override int CalculatePrice()
        {
            return Price;
        }
    }
}