<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;

namespace ldm4
{
    class Program
    {
        static int n;
        static int[,] matrix;
        static void Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n---------------\nГлавное меню");
                Console.WriteLine("1. Ярусно-параллельная форма");
                Console.WriteLine("2. Метод Краскалла");
                Console.WriteLine("3. Алгоритм Дейкстры");
                Console.WriteLine("4. Выход\n---------------");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GetLayeredForm();
                        break;
                    case "2":
                        Kraskal();
                        break;
                    case "3":
                        Deikstra();
                        break;
                    case "4":
                        isRunning = false;
                        break;
                }
            }
        }
         
        static void InputGraph()
        {
            Console.WriteLine("Введите размерность графа: ");
            while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
                Console.WriteLine("неверный ввод");

            matrix = new int[n, n];
            Console.WriteLine("введите матрицу смежности");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Строка {i + 1}:");
                string[] input = Console.ReadLine().Split();

                while(input.Length != n)
                {
                    Console.WriteLine("Неверное количество элементов. Повторите ввод");
                    Console.WriteLine($"Строка {i + 1}:");
                    input = Console.ReadLine().Split();
                }
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = int.Parse(input[j]);
                }
            }
        }

        static void GetLayeredForm()
        {
            InputGraph();
            int[] inEntry = new int[matrix.GetLength(0)];
            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                int count = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, j] != 0)
                    {
                        count++;
                    }
                }
                inEntry[j] = count;
            }

            Queue<int> queue = new Queue<int>();
            for (int k = 0; k < matrix.GetLength(0); k++)
            {
                if (inEntry[k] == 0)
                {
                    queue.Enqueue(k);
                }
            }

            List<List<int>> layers = new List<List<int>>();
            while (queue.Count > 0)
            {
                int currentLayerCount = queue.Count;
                List<int> currentLayer = new List<int>();

                for (int i = 0; i < currentLayerCount; i++)
                {
                    int currentVertex = queue.Dequeue();
                    currentLayer.Add(currentVertex);

                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (matrix[currentVertex, j] != 0)
                        {
                            inEntry[j]--;
                            if (inEntry[j] == 0)
                            {
                                queue.Enqueue(j);
                            }
                        }
                    }
                }
                layers.Add(currentLayer);
            }

            Console.WriteLine("\nЯрусно-параллельная форма графа:");
            for (int i = 0; i < layers.Count; i++)
            {
                Console.WriteLine($"Ярус {i + 1}: {string.Join(", ", layers[i])}");
            }

        }

        static void Kraskal()
        {
            Console.WriteLine("Введите количество вершин графа:");

            while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
                Console.WriteLine("Неверный ввод");

            Console.WriteLine("Вводите матрицу смежности выше главной диагонали");

            matrix = new int[n, n];
            for(int i = 0; i < n; i++)
            {
                if(i < n - 1)
                {
                    Console.WriteLine($"Строка {i + 1}:");
                    string[] input = Console.ReadLine().Split();
                    while(input.Length != n - i - 1)
                    {
                        Console.WriteLine("Неверное количество элементов. Повторите ввод");
                        Console.WriteLine($"Строка {i + 1}:");
                        input = Console.ReadLine().Split();
                    }

                    for(int j = i + 1; j < n; j++)
                    {
                        int weight = int.Parse(input[j - i - 1]);
                        matrix[i, j] = weight;
                        matrix[j, i] = matrix[i, j];
                    }
                }
            }

            List<Edge> edges = new List<Edge>();

            for(int i = 0; i < n; i++)
            {
                for(int j = i + 1; j < n; j++)
                {
                    if (matrix[i, j] != 0)
                        edges.Add(new Edge(i, j, matrix[i, j]));
                }
            }

            edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));

            List<List<int>> components = new List<List<int>>();
            for(int i = 0; i < n; i++)
            {
                components.Add(new List<int> { i });
            }

            List<Edge> minSkeleton = new List<Edge>();

            foreach(Edge edge in edges)
            {
                int cFirst = -1;
                int cSecond = -1;

                for(int i = 0; i < components.Count; i++)
                {
                    if (components[i].Contains(edge.First))
                        cFirst = i;
                    if (components[i].Contains(edge.Second))
                        cSecond = i;
                }

                if(cFirst != cSecond)
                {

                    minSkeleton.Add(edge);
                    components[cFirst].AddRange(components[cSecond]);
                    components.RemoveAt(cSecond);
                }
            }
              
            Console.WriteLine("Минамальный остов: ");
            foreach(Edge item in minSkeleton)
            {
                Console.WriteLine(item);
            }

            int totalWeight = 0;
            foreach (Edge edge in minSkeleton)
            {
                totalWeight += edge.Weight;
            }

            Console.WriteLine($"Общий вес остова: {totalWeight}");
        }
        
        static void Deikstra()
        {
            InputGraph();
            int src = 0;
            int final = n - 1;

            // 2. Инициализация
            const int INF = int.MaxValue;
            int[] dist = new int[n];
            int[] prev = new int[n];
            bool[] used = new bool[n];
            for (int i = 0; i < n; i++)
            {
                dist[i] = INF;
                prev[i] = -1;
            }
            dist[src] = 0;

            // 3. Основной цикл Dijkstra
            for (int j = 0; j < n; j++)
            {
                int u = -1, best = INF;
                // Найти необработанную вершину с min dist
                for (int i = 0; i < n; i++)
                    if (!used[i] && dist[i] < best)
                    {
                        best = dist[i];
                        u = i;
                    }
                if (u == -1) break;       // больше некуда идти
                used[u] = true;           // «закрываем» u

                // Релаксация рёбер из u
                for (int v = 0; v < n; v++)
                {
                    int w = matrix[u, v];
                    if (w > 0 && !used[v] && dist[u] + w < dist[v])
                    {
                        dist[v] = dist[u] + w;
                        prev[v] = u;
                    }
                }
            }

            // 4. Восстановление и вывод пути до target
            Console.WriteLine($"\nКратчайший путь из вершины 1 в вершину {n}:");
            if (dist[final] == INF)
            {
                Console.WriteLine("  пути нет");
            }
            else
            {
                // Собираем путь в стек
                var stack = new Stack<int>();
                for (int cur = final; cur != -1; cur = prev[cur])
                    stack.Push(cur);
                // Выводим, преобразуя индексы в 1-based нумерацию
                Console.Write("  путь: ");
                Console.Write(string.Join("=>", stack.Select(x => x + 1)));
                Console.WriteLine($"\n  длина: {dist[final]}");
            }
        }
    }
}
=======
﻿// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
>>>>>>> 1b853a8f3cf1e78a08029632c43956805cdf982a
