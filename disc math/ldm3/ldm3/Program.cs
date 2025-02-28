using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        while (true) 
        {
            int[,] adjacencyMatrix = InputAdjacencyMatrix();

            Console.WriteLine("\nВведенная матрица смежности:");
            PrintMatrix(adjacencyMatrix);

            int[,] reachabilityMatrix = ReachabilityMatrix(adjacencyMatrix);
            Console.WriteLine("\nМатрица достижимости R:");
            PrintMatrix(reachabilityMatrix);

            int[,] strongConnectivityMatrix = StronglyConnectedMatrix(reachabilityMatrix);
            Console.WriteLine("\nМатрица сильной связности S:");
            PrintMatrix(strongConnectivityMatrix);

            List<List<int>> components = SearchComponents(strongConnectivityMatrix);
            Console.WriteLine("\nКомпоненты сильной связности:");
            PrintComponents(components);

            Console.WriteLine("\nХотите ввести новую матрицу? (да - Enter / нет - введите 0)");
            string input = Console.ReadLine();
            if (input == "0") break; 
        }
    }

    static int[,] InputAdjacencyMatrix()
    {
        Console.WriteLine("Выберите способ ввода матрицы:");
        Console.WriteLine("1 - Выбрать из готовых тестов");
        Console.WriteLine("2 - Ввести вручную");
        int choice;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out choice) && (choice == 1 || choice == 2))
                break;
            Console.WriteLine("Ошибка! Введите 1 или 2.");
        }

        if (choice == 1)
        {
            int[][,] testMatrices = {
                new int[,] {
                    {0, 1, 0, 0, 0, 0},
                    {1, 1, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0},
                    {0, 0, 1, 0, 1, 0},
                    {0, 0, 0, 1, 0, 0},
                    {0, 1, 1, 0, 1, 1}
                },
                new int[,] {
                    {0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0},
                    {0, 1, 0, 0, 0},
                    {0, 0, 1, 0, 0},
                    {1, 0, 1, 1, 1}
                }
            };

            Console.WriteLine("Доступные тестовые матрицы:");
            for (int i = 0; i < testMatrices.Length; i++)
            {
                Console.WriteLine($"Тест {i + 1}:");
                PrintMatrix(testMatrices[i]);
                Console.WriteLine();
            }

            int testIndex;
            while (true)
            {
                Console.Write($"Выберите номер теста (1-{testMatrices.Length}): ");
                if (int.TryParse(Console.ReadLine(), out testIndex) &&
                    testIndex >= 1 && testIndex <= testMatrices.Length)
                    break;
                Console.WriteLine("Ошибка! Введите корректный номер теста.");
            }

            return testMatrices[testIndex - 1];
        }
        else
        {
            int n;
            while (true)
            {
                Console.Write("Введите размерность графа (n <= 10): ");
                if (int.TryParse(Console.ReadLine(), out n) && n > 0 && n <= 10)
                    break;
                Console.WriteLine("Ошибка! Введите число от 1 до 10.");
            }

            int[,] matrix = new int[n, n];

            Console.WriteLine("Введите матрицу смежности построчно (0 или 1):");
            for (int i = 0; i < n; i++)
            {
                while (true)
                {
                    Console.Write($"Строка {i + 1}: ");
                    string[] input = Console.ReadLine().Split();
                    if (input.Length != n)
                    {
                        Console.WriteLine($"Ошибка! Введите {n} чисел.");
                        continue;
                    }

                    bool valid = true;
                    for (int j = 0; j < n; j++)
                    {
                        if (int.TryParse(input[j], out matrix[i, j]) && (matrix[i, j] == 0 || matrix[i, j] == 1))
                            continue;

                        valid = false;
                        break;
                    }

                    if (valid) break;
                    Console.WriteLine("Ошибка! Введите только 0 или 1.");
                }
            }

            return matrix;
        }
    }

    static int[,] ReachabilityMatrix(int[,] adjacencyMatrix)
    {
        int n = adjacencyMatrix.GetLength(0);
        int[,] R = (int[,])adjacencyMatrix.Clone();

        for (int k = 0; k < n; k++)
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    R[i, j] |= (R[i, k] & R[k, j]);

        for (int i = 0; i < n; i++)
            R[i, i] = 1;

        return R;
    }

    static int[,] StronglyConnectedMatrix(int[,] R)
    {
        int n = R.GetLength(0);

        int[,] poweredR = PowerMatrix(R, n);
        int[,] RT = TranspMatrix(poweredR);

        int[,] S = new int[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                S[i, j] = (poweredR[i, j] == 1 && RT[i, j] == 1) ? 1 : 0;

        return S;
    }

    static int[,] PowerMatrix(int[,] matrix, int power)
    {
        int n = matrix.GetLength(0);
        int[,] result = (int[,])matrix.Clone();

        for (int p = 1; p < power; p++)
            result = MultiplyMatrices(result, matrix);

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                result[i, j] = result[i, j] > 0 ? 1 : 0;

        return result;
    }

    static int[,] TranspMatrix(int[,] matrix)
    {
        int n = matrix.GetLength(0);
        int[,] transposed = new int[n, n];

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                transposed[j, i] = matrix[i, j];

        return transposed;
    }

    static int[,] MultiplyMatrices(int[,] A, int[,] B)
    {
        int n = A.GetLength(0);
        int[,] result = new int[n, n];

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                for (int k = 0; k < n; k++)
                    result[i, j] += A[i, k] * B[k, j];

        return result;
    }

    static List<List<int>> SearchComponents(int[,] S)
    {
        int n = S.GetLength(0);
        List<List<int>> components = new List<List<int>>();
        List<int> visited = new List<int>();

        for (int i = 0; i < n; i++)
        {
            if (!visited.Contains(i))
            {
                List<int> component = new List<int>();
                for (int j = 0; j < n; j++)
                {
                    if (S[i, j] == 1)
                    {
                        component.Add(j + 1);
                        visited.Add(j);
                    }
                }
                components.Add(component);
            }
        }

        return components;
    }

    static void PrintMatrix(int[,] matrix)
    {
        int n = matrix.GetLength(0);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                Console.Write(matrix[i, j] + " ");
            Console.WriteLine();
        }
    }

    static void PrintComponents(List<List<int>> components)
    {
        for (int i = 0; i < components.Count; i++)
        {
            Console.Write($"Компонента {i + 1}: ");
            Console.WriteLine(string.Join(", ", components[i]));
        }
    }
}
