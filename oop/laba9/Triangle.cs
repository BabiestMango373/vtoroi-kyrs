using System;

public class Triangle
{
    private double a;
    private double b;
    private double c;
    
    // Статическое поле для подсчета созданных объектов
    private static int objectCount = 0;

    public double A
    {
        get => a;
        private set
        {
            if (value <= 0)
                throw new ArgumentException("Сторона треугольника должна быть положительным числом");
            a = value;
        }
    }

    public double B
    {
        get => b;
        private set
        {
            if (value <= 0)
                throw new ArgumentException("Сторона треугольника должна быть положительным числом");
            b = value;
        }
    }

    public double C
    {
        get => c;
        private set
        {
            if (value <= 0)
                throw new ArgumentException("Сторона треугольника должна быть положительным числом");
            c = value;
        }
    }

    public Triangle(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
        if (!CanExist())
            throw new ArgumentException("Треугольник с такими сторонами не может существовать");
        objectCount++;
    }

    public static int ObjectCount => objectCount;

    public bool CanExist() => (a + b > c) && (a + c > b) && (b + c > a);

    public static bool CanExist(double a, double b, double c) => (a + b > c) && (a + c > b) && (b + c > a);

    public double CalculateArea()
    {
        double semiPerimeter = (a + b + c) / 2;
        return Math.Sqrt(semiPerimeter * (semiPerimeter - a) * (semiPerimeter - b) * (semiPerimeter - c));
    }

    public void DisplayInfo()
    {
        Program.UI.DisplayTriangleInfo(this);
    }

    public static double operator -(Triangle triangle) => triangle.CalculateArea();

    public static implicit operator double(Triangle triangle) => triangle.a + triangle.b + triangle.c;

    public static explicit operator bool(Triangle triangle) => triangle.CanExist();

    public static bool operator <(Triangle t1, Triangle t2) => t1.CalculateArea() < t2.CalculateArea();
    public static bool operator >(Triangle t1, Triangle t2) => t1.CalculateArea() > t2.CalculateArea();
}

