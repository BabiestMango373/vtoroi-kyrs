using System;

class Program
{
    static void Main()
    {
        double x, y;
        bool isCorrected = false;

        // Ввод координаты x
        Console.WriteLine("Введите x:");
        do
        {
            isCorrected = double.TryParse(Console.ReadLine(), out x);
            if (!isCorrected)
            {
                Console.WriteLine("Некорректные данные, введите число:");
            }
        } while (!isCorrected);

        // Ввод координаты y
        isCorrected = false;
        Console.WriteLine("Введите y:");
        do
        {
            isCorrected = double.TryParse(Console.ReadLine(), out y);
            if (!isCorrected)
            {
                Console.WriteLine("Некорректные данные, введите число:");
            }
        } while (!isCorrected);

        // Находится ли точка в круге
        bool inCircle = ((x - 5) * (x - 5) + y * y <= 25);

        // Находится ли точка в треугольнике 
        bool inTriangle = (x >= 0 && x <= 10 && y >= -5 && y <= 5);

        // Принадлежит ли точка заштрихованной области

        if (inCircle || inTriangle)
        {
            Console.WriteLine("Точка в заштрихованной области");
        }
        else
        {
            Console.WriteLine("Точка вне заштрихованной области");
        }
    }
}