using System;

class Program
    {
        static void Main(string[] args)
        {
            int m, n;
            bool isConverted = false;

            // Ввод m с проверкой
            Console.WriteLine("Введите значение m:");
            do
            {
                isConverted = int.TryParse(Console.ReadLine(), out m);
                if (!isConverted)
                {
                    Console.WriteLine("Неверный тип числа, введите целое число:");
                }
            } while (!isConverted);

            // Ввод n с проверкой
            Console.WriteLine("Введите значение n:");
            isConverted = false;
            do
            {
                isConverted = int.TryParse(Console.ReadLine(), out n);
                if (!isConverted)
                {
                    Console.WriteLine("Неверный тип числа, введите целое число:");
                }
            } while (!isConverted);

            // m + --n
            int m1 = m, n1 = n;
            int result1 = m1 + --n1;
            Console.WriteLine($"m = {m1} n = {n1} m + --n = {result1}");

            // m++ < --n
            m1 = m; 
            n1 = n;
            bool result2 = m1++ < --n1;
            Console.WriteLine($"m = {m1} n = {n1} m++ < --n = {result2}");

            // --m > n--
            m1 = m;
            n1 = n;
            bool result3 = --m1 > n1--;
            Console.WriteLine($"m = {m1} n = {n1} --m > n-- = {result3}");

            // arccos(x + x^2)
            double x;
            isConverted = false;
            double sum;

            // Ввод x с проверкой для arccos
            do
            {
                Console.WriteLine("Введите значение x:");
                do
                {
                    isConverted = double.TryParse(Console.ReadLine(), out x);
                    if (!isConverted)
                    {
                        Console.WriteLine("Неверный тип числа, введите число:");
                    }
                } while (!isConverted);

                // Проверка для arccos
                sum = x + Math.Pow(x, 2);
                if (sum < -1 || sum > 1)
                {
                    Console.WriteLine("Ошибка: значение выражения выходит за пределы области определения arccos. Введите другое значение:");
                }

            } while (sum < -1 || sum > 1);

            // Вычисление arccos
            double result4 = Math.Acos(sum);
            Console.WriteLine($"arccos(x + x^2) = {result4}");
        }
    }

