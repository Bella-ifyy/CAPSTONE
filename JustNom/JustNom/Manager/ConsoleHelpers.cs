using System;

namespace JustNom.Manager
{
    // Static helper class for console input-related methods
    public static class ConsoleHelpers
    {
        // Method to get an integer input from the user within a specified range
        public static int GetIntegerInRange(int pMin, int pMax, string pMessage)
        {
            if (pMin > pMax)
            {
                throw new ArgumentOutOfRangeException("pMin", $"Minimum value {pMin} cannot be greater than maximum value {pMax}");
            }

            int result;
            int attemptCount = 0;

            while (true)
            {
                Console.WriteLine(pMessage);
                Console.WriteLine($"Please enter a number between {pMin} and {pMax} inclusive.");

                string userInput = Console.ReadLine();

                // Try to parse the user input as an integer
                if (int.TryParse(userInput, out result))
                {
                    // Check if the parsed integer is within the specified range
                    if (result >= pMin && result <= pMax)
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine($"Error: Number is not within the specified range ({pMin} to {pMax}). Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input: not a valid number. Please try again.");
                }

                attemptCount++;
                if (attemptCount >= 5)
                {
                    Console.WriteLine("Too many invalid attempts. Exiting...");
                    throw new InvalidOperationException("Invalid input received too many times.");
                }
            }
        }
    }
}