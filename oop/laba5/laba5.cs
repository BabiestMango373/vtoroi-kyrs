using System;

class Program
{
    // Глобальные переменные для хранения массивов
    static int[] oneDimensionalArray = null;
    static int[,] twoDimensionalArray = null;
    static int[][] jaggedArray = null;

    static void Main()
    {
        bool continueProgram = true;

        while (continueProgram)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Одномерный массив: создание и обработка");
            Console.WriteLine("2. Двумерный массив: создание и обработка");
            Console.WriteLine("3. Рваный массив: создание и обработка");
            Console.WriteLine("4. Выход");

            Console.Write("Выберите действие: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    OneDimensionalArray();
                    break;
                case "2":
                    TwoDimensionalArray();
                    break;
                case "3":
                    JaggedArray();
                    break;
                case "4":
                    continueProgram = false;
                    break;
                default:
                    Console.WriteLine("Некорректный выбор, попробуйте снова");
                    break;
            }
        }
    }

    // Одномерный массив: создание и обработка
    static void OneDimensionalArray()
    {
        oneDimensionalArray = FillArray("одномерный");
        PrintArray(oneDimensionalArray);

        bool continueActions = true;
        while (continueActions)
        {
            Console.WriteLine("Выберите действие с одномерным массивом:");
            Console.WriteLine("1. Удалить элементы с нечётными номерами");
            Console.WriteLine("2. Выйти");

            string action = Console.ReadLine();
            switch (action)
            {
                case "1":
                    RemoveElements(ref oneDimensionalArray);
                    PrintArray(oneDimensionalArray);
                    break;
                case "2":
                    continueActions = false;
                    break;
                default:
                    Console.WriteLine("Некорректный выбор, попробуйте снова.");
                    break;
            }
        }
    }

    // Двумерный массив: создание и обработка
    static void TwoDimensionalArray()
    {
        int rows = GetNonNegativeInt("Введите количество строк для двумерного массива: ");
        int cols = GetNonNegativeInt("Введите количество столбцов для двумерного массива: ");
        twoDimensionalArray = FillArray(rows, cols, "двумерный");
        PrintArray(twoDimensionalArray);

        bool continueActions = true;
        while (continueActions)
        {
            Console.WriteLine("Выберите действие с двумерным массивом:");
            Console.WriteLine("1. Добавить столбец после максимального элемента");
            Console.WriteLine("2. Выйти");

            string action = Console.ReadLine();
            switch (action)
            {
                case "1":
                    AddColumnAfterMaxElement(ref twoDimensionalArray);
                    PrintArray(twoDimensionalArray);
                    break;
                case "2":
                    continueActions = false;
                    break;
                default:
                    Console.WriteLine("Некорректный выбор, попробуйте снова");
                    break;
            }
        }
    }

    // Рваный массив: создание и обработка
    static void JaggedArray()
    {
        jaggedArray = CreateOrFillJaggedArray();
        PrintJaggedArray(jaggedArray);

        bool continueActions = true;
        while (continueActions)
        {
            Console.WriteLine("Выберите действие с рваным массивом:");
            Console.WriteLine("1. Добавить строку в конец");
            Console.WriteLine("2. Выйти");

            string action = Console.ReadLine();
            switch (action)
            {
                case "1":
                    AddRowToJaggedArray(ref jaggedArray);
                    PrintJaggedArray(jaggedArray);
                    break;
                case "2":
                    continueActions = false;
                    break;
                default:
                    Console.WriteLine("Некорректный выбор, попробуйте снова");
                    break;
            }
        }
    }

    // Метод для создания или заполнения одномерного массива
    static int[] FillArray(string type)
    {
        string option = "";
        while (option != "1" && option != "2")
        {
            Console.WriteLine($"Выберите способ заполнения {type} массива:");
            Console.WriteLine("1. Ввод с клавиатуры");
            Console.WriteLine("2. Случайные числа");
            option = Console.ReadLine();

            if(option != "1" && option != "2")
            {
                Console.WriteLine("Некорректный выбор, попробуйте снова");
            }
        }


        int size = GetNonNegativeInt("Введите размер массива: ");

        if (size == 0)
        {
            Console.WriteLine("Массив пустой");
            return new int[0];  
        }

        int[] array;

        if (option == "1")
        {
            array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = GetInt($"Введите элемент {i + 1}: ");
            }
        }
        else
        {
            array = CreateRandomArray(size);
        }

        return array;
    }
    

    // Метод для создания и заполнения двумерного массива
    static int[,] FillArray(int rows, int cols, string type)
    {
        string option = "";
        while (option != "1" && option != "2")  // Проверка на корректность выбора
        {
            Console.WriteLine($"Выберите способ заполнения {type} массива:");
            Console.WriteLine("1. Ввод с клавиатуры");
            Console.WriteLine("2. Случайные числа");
            option = Console.ReadLine();

            if (option != "1" && option != "2")
            {
                Console.WriteLine("Некорректный выбор, попробуйте снова");
            }
        }

    int[,] array = new int[rows, cols];

    if (option == "1")
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = GetInt($"Введите элемент [{i}, {j}]: ");  // Ввод элементов вручную
            }
        }
    }
    else
    {
        array = CreateRandomArray(rows, cols);  // Заполнение случайными числами
    }

    return array;
    }


    // Создание одномерного массива со случайными числами 
    static int[] CreateRandomArray(int size)
    {
        Random rand = new Random();
        int[] array = new int[size];

        for (int i = 0; i < size; i++)
        {
            array[i] = rand.Next(-100, 101);
        }

        return array;
    }

    // Создание двумерного массива со случайными числами
    static int[,] CreateRandomArray(int rows, int cols)
    {
        Random rand = new Random();
        int[,] array = new int[rows, cols];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                array[i, j] = rand.Next(-100, 101);

        return array;
    }

    
    // Печать одномерного массива
    static void PrintArray(int[] array)
    {
        if (array.Length == 0)
        {
            Console.WriteLine("Массив пуст");
            return;
        }

        Console.WriteLine("Одномерный массив:");
        foreach (var item in array)
        {
            Console.Write(item + "\t");
        }
        Console.WriteLine();
    }

    // Печать двумерного массива
    static void PrintArray(int[,] array)
    {
        if (array.GetLength(0) == 0 || array.GetLength(1) == 0)
        {
            Console.WriteLine("Массив пуст");
            return;
        }

        Console.WriteLine("Двумерный массив:");
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write(array[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    // Печать рваного массива
    static void PrintJaggedArray(int[][] jaggedArray)
    {
        if (jaggedArray.Length == 0)
        {
            Console.WriteLine("Рваный массив пуст");
            return;
        }

        Console.WriteLine("Рваный массив:");
        foreach (var row in jaggedArray)
        {
            foreach (var item in row)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
        }
    }

    // Удаление элементов с нечетными индексами
    static void RemoveElements(ref int[] array)
    {
        if (array.Length == 0)
        {
            Console.WriteLine("Невозможно удалить элементы из пустого массива");
            return;
        }

        int newSize = (array.Length) / 2;
        int[] newArray = new int[newSize];
        for (int i = 0, j = 0; i < array.Length; i++)
        {
            if (i % 2 == 1) 
            {
                newArray[j++] = array[i];
            }
        }

        array = newArray;
    }

    // Добавление столбца после максимального элемента в двумерном массиве
    static void AddColumnAfterMaxElement(ref int[,] array)
    {
        if (array.GetLength(0) == 0 || array.GetLength(1) == 0)
        {
            Console.WriteLine("Невозможно изменить пустой массив");
            return;
        }

        int maxElement = int.MinValue;
        int maxCol = 0;
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] > maxElement)
                {
                    maxElement = array[i, j];
                    maxCol = j; // индекс столбца с максимальным элементом
                }
            }
        }

        int[,] newArray = new int[array.GetLength(0), array.GetLength(1) + 1];

        for (int i = 0; i < array.GetLength(0); i++)
        {
            int newCol = 0;
            for (int j = 0; j < array.GetLength(1); j++, newCol++)
            {
                newArray[i, newCol] = array[i, j];
                if (j == maxCol)
                {
                    newArray[i, ++newCol] = new Random().Next(-100, 101);
                }
            }
        }

        array = newArray;
    }

    // Создание рваного массива с вводом
    static int[][] CreateOrFillJaggedArray()
    {
        Console.Write("Введите количество строк рваного массива: ");
        int rowCount = GetNonNegativeInt();

        if (rowCount == 0)
        {
            Console.WriteLine("Рваный массив пуст");
            return new int[0][];
        }

    int[][] jaggedArray = new int[rowCount][];
    for (int i = 0; i < rowCount; i++)
    {
        Console.Write($"Введите количество элементов в строке {i + 1}: ");
        int colCount = GetNonNegativeInt();
        jaggedArray[i] = new int[colCount];

        string option = "";
        while (option != "1" && option != "2")  
        {
            Console.WriteLine($"Выберите способ заполнения строки {i + 1}:");
            Console.WriteLine("1. Ввод с клавиатуры");
            Console.WriteLine("2. Случайные числа");
            option = Console.ReadLine();

            if (option != "1" && option != "2")
            {
                Console.WriteLine("Некорректный выбор, попробуйте снова");
            }
        }

        if (option == "1")
        {
            for (int j = 0; j < colCount; j++)
            {
                jaggedArray[i][j] = GetInt($"Элемент {j + 1}: "); 
            }
        }
        else
        {
            Random rand = new Random();
            for (int j = 0; j < colCount; j++)
            {
                jaggedArray[i][j] = rand.Next(-100, 101);  
            }
        }
    }

    return jaggedArray;
    }

    // Добавление строки в конец рваного массива
    static void AddRowToJaggedArray(ref int[][] jaggedArray)
    {
    Console.Write("Введите количество элементов в новой строке: ");
    int colCount = GetNonNegativeInt();  
    int[] newRow = new int[colCount];

    string option = "";
    while (option != "1" && option != "2")
    {
        Console.WriteLine("Выберите способ заполнения новой строки:");
        Console.WriteLine("1. Ввод с клавиатуры");
        Console.WriteLine("2. Случайные числа");
        option = Console.ReadLine();

        if (option != "1" && option != "2")
        {
            Console.WriteLine("Некорректный выбор, попробуйте снова");
        }
    }

    if (option == "1")
    {
        for (int j = 0; j < colCount; j++)
        {
            newRow[j] = GetInt($"Элемент {j + 1}: "); 
        }
    }
    else
    {
        Random rand = new Random();
        for (int j = 0; j < colCount; j++)
        {
            newRow[j] = rand.Next(-100, 101);  
        }
    }

    Array.Resize(ref jaggedArray, jaggedArray.Length + 1);
    jaggedArray[^1] = newRow;  
}

    // Проверка и получение числа от пользователя 
    static int GetInt(string prompt = "Введите число: ")
    {
        int number;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out number))
            {
                return number;
            }
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число");
        }
    }

    // Проверка и получение неотрицательных чисел
    static int GetNonNegativeInt(string prompt = null)
    {
        int number;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out number) && number >= 0)
            {
                return number;
            }
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите неотрицательное число.");
        }
    }
}
