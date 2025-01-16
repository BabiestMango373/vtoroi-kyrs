using System;

class Program
{
    static void Main()
    {
        double a = 0.1; // Границы х
        double b = 1.0;
        int k = 10; // Количество шагов
        int n = 20; // Заданное количество слагаемых
        double e = 0.0001; // Заданная точность

        double step = (b - a) / k;

        // Внешний цикл с изменением параметра x
        for (double x = a; x <= b; x += step)
        {
            // Вычисление суммы SN 
            double SN = x;
            double term = x; 
            for (int i = 1; i <= n; i++)
            {
                term *= (x * x) / ((2 * i) * (2 * i + 1)); 
                SN += term;
            }

            // Вычисление суммы SE 
            double SE = x;
            term = x; 
            int iteration = 1;
            while (Math.Abs(term) >= e)
            {
                term *= (x * x) / ((2 * iteration) * (2 * iteration + 1)); 
                SE += term;
                iteration++;
            }

            // Точное значение функции Y
            double Y = (Math.Pow(Math.E, x) - Math.Pow(Math.E, -x)) / 2;

            // Вывод результатов
            Console.WriteLine($"X = {x:F4}  SN = {SN:F6}  SE = {SE:F6}  Y = {Y:F6}");
        }
    }
}
