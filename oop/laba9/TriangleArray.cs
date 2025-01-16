using System;

public class TriangleArray
{
    private Triangle[] arr;
    private static int amount = 0;

    public int Length => arr.Length;

    // Конструктор без параметров
    public TriangleArray()
    {
        arr = new Triangle[0];
        amount++;
    }

    // Конструктор с параметром для заполнения случайными значениями
    public TriangleArray(int size, Random rnd)
    {
        arr = new Triangle[size];
        for (int i = 0; i < size; i++)
        {
            double a, b, c;
            do
            {
                a = rnd.Next(1, 10);
                b = rnd.Next(1, 10);
                c = rnd.Next(1, 10);
            } while (!Triangle.CanExist(a, b, c));

            arr[i] = new Triangle(a, b, c);
        }
        amount++;
    }

    // Конструктор для заполнения массива значениями от пользователя
    public TriangleArray(int size, bool byUserInput)
    {
        arr = new Triangle[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = Program.UI.ReadTriangleFromUser($"Введите стороны треугольника {i + 1}");
        }
        amount++;
    }

    // Индексатор для доступа к элементам массива с проверкой границ
    public Triangle this[int index]
    {
        get
        {
            if (index < 0 || index >= arr.Length)
                throw new ArgumentException("Индекс вне диапазона массива");
            return arr[index];
        }
        set
        {
            if (index < 0 || index >= arr.Length)
                throw new ArgumentException("Индекс вне диапазона массива");
            arr[index] = value;
        }
    }

    // Метод для отображения всех треугольников в массиве
    public void DisplayElements()
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Console.WriteLine($"Треугольник {i + 1}:");
            arr[i].DisplayInfo();
        }
    }

    // Метод для нахождения треугольника с минимальной площадью
    public Triangle FindMinAreaTriangle()
    {
        if (arr.Length == 0)
            throw new InvalidOperationException("Массив пуст");

        Triangle minTriangle = arr[0];
        double minArea = -arr[0];

        for (int i = 1; i < arr.Length; i++)
        {
            double area = -arr[i];
            if (area < minArea)
            {
                minArea = area;
                minTriangle = arr[i];
            }
        }
        return minTriangle;
    }

    // Статический метод для получения количества созданных массивов
    public static int GetAmount() => amount;
}
