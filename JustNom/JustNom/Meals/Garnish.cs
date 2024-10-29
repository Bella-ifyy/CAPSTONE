// Garnish.cs
namespace JustNom.Meals
{
    // Represents a garnish
    public class Garnish : FoodItem
    {
        public Garnish(string name, int price) : base(name, price)
        {
        }

        public override int CalculatePrice()
        {
            return Price;
        }
    }
}