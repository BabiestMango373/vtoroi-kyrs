using System;

class Program
    {
        static void Main(string[] args)
        {
            // Входные данные для float
            float a = 1000f;
            float b = 0.0001f;

            // Вычисления для типа float
            float aMinusB = a - b;
            float aMinusBCubed = (float)Math.Pow(aMinusB, 3);
            float aCubed = (float)Math.Pow(a, 3);
            float numerator = aMinusBCubed - aCubed;

            float bCubed = (float)Math.Pow(b, 3);
            float aSquared = (float)Math.Pow(a, 2);
            float bSquared = (float)Math.Pow(b, 2);
            float denominator = -bCubed + 3 * a * bSquared - 3 * aSquared * b;

            float resultFloat = numerator / denominator;
            Console.WriteLine($"Float result = {resultFloat}");

            // Входные данные для double
            double a1 = 1000.0;
            double b1 = 0.0001;

            // Вычисления для типа double
            double a1MinusB1 = a1 - b1;
            double a1MinusB1Cubed = Math.Pow(a1MinusB1, 3);
            double a1Cubed = Math.Pow(a1, 3);
            double numerator1 = a1MinusB1Cubed - a1Cubed;

            double b1Cubed = Math.Pow(b1, 3);
            double a1Squared = Math.Pow(a1, 2);
            double b1Squared = Math.Pow(b1, 2);
            double denominator1 = -b1Cubed + 3 * a1 * b1Squared - 3 * a1Squared * b1;

            double resultDouble = numerator1 / denominator1;
            Console.WriteLine($"Double result = {resultDouble}");
        }
    }
