// Order.cs
using System;
using System.Collections.Generic;
using System.Text;

namespace JustNom.Meals
{
    // Represents an order with customer details and food items
    public class Order
    {
        private double DeliveryCharge = 200;

        public string CustomerName { get; set; }
        public string Address { get; set; }
        public List<FoodItem> Items { get; private set; }
        public bool IsDelivery { get; set; }

        // Constructor for Order class
        public Order(string customerName, bool isDelivery, string address = "")
        {
            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Customer name cannot be empty", nameof(customerName));
            if (isDelivery && string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Address cannot be empty for delivery", nameof(address));

            CustomerName = customerName;
            Address = address;
            Items = new List<FoodItem>();
            IsDelivery = isDelivery;
        }

        // Adds an item to the order
        public void AddItem(FoodItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Items.Add(item);
        }

        // Calculates the total price of the order without delivery charge
        public int TotalPriceWithoutDelivery() => Items.Sum(item => item.CalculatePrice());

        // Calculates the total price of the order including delivery charge if applicable
        public int TotalPrice() => TotalPriceWithoutDelivery() + (IsDelivery && TotalPriceWithoutDelivery() <= 2000 ? 200 : 0);

        // Outputs the order details as a string
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Order for {CustomerName}");
            foreach (var item in Items)
            {
                builder.AppendLine($"{item.Name} - £{item.CalculatePrice() / 100.0:F2}");
                if (item is Pizza pizza)
                {
                    builder.AppendLine($"  Toppings: {string.Join(", ", pizza.Toppings)}");
                }
                else if (item is Burger burger)
                {
                    builder.AppendLine($"  Garnishes: {string.Join(", ", burger.Garnishes)}");
                }
            }
            builder.AppendLine($"Total price: £{TotalPrice() / 100.0:F2}");
            if (IsDelivery)
            {
                builder.AppendLine(IsDeliveryChargeApplicable() ? $"Delivery charge: £{DeliveryCharge / 100.0:F2}" : "No delivery charge (orders over £20)");
                builder.AppendLine($"Delivering to: {Address}");
            }
            return builder.ToString();
        }

        // Checks if the delivery charge is applicable
        private bool IsDeliveryChargeApplicable()
        {
            return IsDelivery && TotalPriceWithoutDelivery() <= 2000;
        }
    }
}