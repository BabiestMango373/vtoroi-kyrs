using System;

public static class Program
{
    private static readonly Random rnd = new Random();

    public static class UI
    {
        public static double ReadPositiveDouble(string prompt)
        {
            double result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (double.TryParse(input, out result) && result > 0)
                {
                    return result;
                }
                Console.WriteLine("Ошибка: Введите положительное число");
            }
        }

        public static int ReadPositiveInt(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out result) && result > 0)
                {
                    return result;
                }
                Console.WriteLine("Ошибка: Введите положительное целое число");
            }
        }

// Метод для создания треугольника по вводу
        public static Triangle ReadTriangleFromUser(string prompt)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    double a = ReadPositiveDouble("Введите сторону a: ");
                    double b = ReadPositiveDouble("Введите сторону b: ");
                    double c = ReadPositiveDouble("Введите сторону c: ");

                    return new Triangle(a, b, c); 
                }
                catch
                {
                    Console.WriteLine("Ошибка: Треугольник с такими сторонами не может существовать");
                    Console.WriteLine("Попробуйте снова");
                }
            }
        }

// Метод для вывода информации о треугольнике
        public static void DisplayTriangleInfo(Triangle triangle)
        {
            Console.WriteLine($"Стороны треугольника: a = {triangle.A}, b = {triangle.B}, c = {triangle.C}");
            Console.WriteLine($"Периметр: {(double)triangle}");
            Console.WriteLine($"Площадь: {-triangle}");
            Console.WriteLine($"Существует ли: {(bool)triangle}");
        }

// Метод для вывода информации обо всех треугольниках в массиве
        public static void DisplayTriangleArrayInfo(TriangleArray triangleArray)
        {
            triangleArray.DisplayElements();
        }

        public static void DisplayMinAreaTriangle(Triangle triangle)
        {
            Console.WriteLine("Треугольник с минимальной площадью:");
            DisplayTriangleInfo(triangle);
        }
    }

    static void CaseOne()
    {
        Console.WriteLine("Создание одного треугольника:");
        Triangle triangle = UI.ReadTriangleFromUser("Введите стороны треугольника:");
        triangle.DisplayInfo();
    }

    static void CaseTwo()
    {
        Console.WriteLine("Сравнение двух треугольников по площади:");
        Triangle triangle1 = UI.ReadTriangleFromUser("Первый треугольник");
        Triangle triangle2 = UI.ReadTriangleFromUser("Второй треугольник");

        Console.WriteLine($"Площадь первого треугольника: {-triangle1}");
        Console.WriteLine($"Площадь второго треугольника: {-triangle2}");
        Console.WriteLine($"Первый треугольник меньше второго? {triangle1 < triangle2}");
        Console.WriteLine($"Первый треугольник больше второго? {triangle1 > triangle2}");
    }

    static void CaseThree()
    {
    Console.WriteLine("Работа с массивом треугольников:");

    int size = UI.ReadPositiveInt("Введите количество треугольников в массиве: ");
    Console.WriteLine("Как заполнить массив: 1 - Автоматически, 2 - Вручную");
    int choice;

    while (true)
    {
        choice = UI.ReadPositiveInt("Ваш выбор: ");
        if (choice == 1 || choice == 2)
            break;
        Console.WriteLine("Ошибка: Введите 1 для автоматического или 2 для ручного ввода");
    }

    TriangleArray array = choice switch
    {
        1 => new TriangleArray(size, rnd),  
        2 => new TriangleArray(size, true), 
    };

    UI.DisplayTriangleArrayInfo(array);

    
    Console.WriteLine("\nВывести конкретный треугольник? (1 - Да, 2 - Нет)");
    int vC = UI.ReadPositiveInt("Ваш выбор: ");
    if (vC == 1)
    {
        while (true)
        {
            Console.WriteLine("Введите индекс треугольника для вывода:");
            int index = UI.ReadPositiveInt("Индекс: ") - 1;

            try
            {
                if (index < 0 || index >= array.Length)
                    throw new ArgumentException("Индекс вне диапазона массива");

                Console.WriteLine($"Информация о треугольнике {index + 1}:");
                array[index].DisplayInfo(); 
                break; 
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine("Попробуйте снова");
            }
        }
    }


    try
    {
        Triangle minAreaTriangle = array.FindMinAreaTriangle();
        UI.DisplayMinAreaTriangle(minAreaTriangle);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка: Треугольник с такими сторонами не может существовать");
    }

    Console.WriteLine($"\nКоличество созданных массивов треугольников: {TriangleArray.GetAmount()}");
}


    public static void Main()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("Выберите действие: 1 - Один треугольник, 2 - Сравнение, 3 - Массив, 4 - Выход");
            int choice;

            while (true)
            {
                choice = UI.ReadPositiveInt("Ваш выбор: ");
                if (choice >= 1 && choice <= 4)
                    break;
                Console.WriteLine("Ошибка: Введите число от 1 до 4");
            }

            switch (choice)
            {
                case 1:
                    CaseOne();
                    break;
                case 2:
                    CaseTwo();
                    break;
                case 3:
                    CaseThree();
                    break;
                case 4:
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова");
                    break;
            }
        }

        Console.WriteLine($"\nОбщее количество созданных объектов Triangle: {Triangle.ObjectCount}");
    }
}