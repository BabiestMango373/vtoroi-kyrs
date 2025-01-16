using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        while (true)
        {
            DisplayMenu();
            int choice = ReadInt("Выберите действие: ", minValue: 1, maxValue: 3);

            switch (choice)
            {
                case 1: // Генерация таблицы истинности с помощью ДСЧ
                    int n1 = ReadInt("Введите количество переменных (n <= 5): ", minValue: 1, maxValue: 5);
                    var rows1 = (int)Math.Pow(2, n1);
                    var truthTable1 = GenerateTruthTable(n1);
                    var functionValues1 = GenerateRandomFunctionValues(rows1);

                    Console.WriteLine("\nТаблица истинности:");
                    for (int i = 0; i < rows1; i++)
                    {
                        Console.WriteLine(string.Join(" ", truthTable1[i]) + " " + functionValues1[i]);
                    }

                    PrintResults(truthTable1, functionValues1, n1);
                    break;

                case 2: // Ввод таблицы истинности вручную
                    int n2 = ReadInt("Введите количество переменных (n <= 5): ", minValue: 1, maxValue: 5);
                    var rows2 = (int)Math.Pow(2, n2);
                    var truthTable2 = GenerateTruthTable(n2);
                    var functionValues2 = ReadFunctionValues(rows2);

                    Console.WriteLine("\nТаблица истинности:");
                    for (int i = 0; i < rows2; i++)
                    {
                        Console.WriteLine(string.Join(" ", truthTable2[i]) + " " + functionValues2[i]);
                    }

                    PrintResults(truthTable2, functionValues2, n2);
                    break;

                case 3: // Завершение работы
                    Console.WriteLine("Завершение работы.");
                    return;

                default:
                    Console.WriteLine("Ошибка: выберите действие из меню.");
                    break;
            }
        }
    }

    public static void DisplayMenu()
    {
        Console.WriteLine("\nВыберите действие:");
        Console.WriteLine("1. Генерация таблицы истинности");
        Console.WriteLine("2. Ввести таблицу истинности вручную");
        Console.WriteLine("3. Завершить работу\n");
    }

    static int ReadInt(string message, int minValue, int maxValue)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= minValue && value <= maxValue)
            {
                return value;
            }
            Console.WriteLine($"Ошибка: введите число от {minValue} до {maxValue}.");
        }
    }

    static List<int[]> GenerateTruthTable(int n)
    {
    var table = new List<int[]>();
    int rows = (int)Math.Pow(2, n);

    for (int i = 0; i < rows; i++)
    {
        var binaryRow = Convert.ToString(i, 2).PadLeft(n, '0');
        var row = binaryRow.Select(c => c - '0').ToArray();
        table.Add(row);
    }

    return table;
    }


    static int[] ReadFunctionValues(int rows)
    {
        Console.WriteLine($"Введите значения функции для каждой строки таблицы истинности:");
        var values = new int[rows];

        for (int i = 0; i < rows; i++)
        {
            while (true)
            {
                Console.Write($"Строка {i + 1}: ");
                var input = Console.ReadLine();
                if (input == "0" || input == "1")
                {
                    values[i] = int.Parse(input);
                    break;
                }
                Console.WriteLine("Ошибка: введите 0 или 1.");
            }
        }

        return values;
    }

    static int[] GenerateRandomFunctionValues(int rows)
    {
        Random random = new Random();
        var values = Enumerable.Range(0, rows).Select(_ => random.Next(2)).ToArray();
        Console.WriteLine("Случайные значения функции: " + string.Join(" ", values));
        return values;
    }

    static void PrintResults(List<int[]> truthTable, int[] functionValues, int n)
    {
        string sdnf = GenerateSDNF(truthTable, functionValues);
        string sknf = GenerateSKNF(truthTable, functionValues);
        string mdnf = GenerateMDNF(truthTable, functionValues, n);

        Console.WriteLine("\nСДНФ: " + sdnf);
        Console.WriteLine("СКНФ: " + sknf);
        Console.WriteLine("МДНФ: " + mdnf);
    }

    static string GenerateSDNF(List<int[]> truthTable, int[] functionValues)
    {
    var terms = new List<string>();

    for (int i = 0; i < functionValues.Length; i++)
    {
        if (functionValues[i] == 1)
        {
            var term = new List<string>();
            for (int j = 0; j < truthTable[i].Length; j++)
            {
                term.Add(truthTable[i][j] == 1 ? $"x{j + 1}" : $"!x{j + 1}");
            }
            terms.Add(string.Join("*", term));
        }
    }

    return string.Join(" v ", terms);
    }


    static string GenerateSKNF(List<int[]> truthTable, int[] functionValues)
    {
    var terms = new List<string>();

    for (int i = 0; i < functionValues.Length; i++)
    {
        if (functionValues[i] == 0)
        {
            var term = new List<string>();
            for (int j = 0; j < truthTable[i].Length; j++)
            {
                term.Add(truthTable[i][j] == 0 ? $"x{j + 1}" : $"!x{j + 1}");
            }
            terms.Add("(" + string.Join(" v ", term) + ")");
        }
    }

    return string.Join(" * ", terms);
    }

    static string GenerateMDNF(List<int[]> truthTable, int[] functionValues, int n)
    {
        var minterms = new List<string>();
        for (int i = 0; i < functionValues.Length; i++)
        {
            if (functionValues[i] == 1)
            {
                minterms.Add(string.Join("", truthTable[i]));
            }
        }

        var primeImplicants = Gluing(minterms);
        return string.Join(" v ", primeImplicants.Select(p => ConvertToExpression(p, n)));
    }

    static List<string> Gluing(List<string> minterms)
    {
    var groups = minterms.GroupBy(m => m.Count(c => c == '1')).ToDictionary(g => g.Key, g => g.ToList());
    var finalImplicants = new List<string>();
    var used = new HashSet<string>();

    while (groups.Any())
    {
        var nextGroups = new Dictionary<int, List<string>>();
        foreach (var (count, group) in groups)
        {
            foreach (var m1 in group)
            {
                foreach (var m2 in groups.GetValueOrDefault(count + 1, new List<string>()))
                {
                    if (CanCombine(m1, m2, out string combined))
                    {
                        used.Add(m1);
                        used.Add(m2);
                        if (!nextGroups.ContainsKey(count))
                            nextGroups[count] = new List<string>();
                        if (!nextGroups[count].Contains(combined))
                            nextGroups[count].Add(combined);
                    }
                }
            }
        }

        foreach (var group in groups.Values)
        {
            foreach (var minterm in group)
            {
                if (!used.Contains(minterm))
                {
                    finalImplicants.Add(minterm);
                }
            }
        }

        groups = nextGroups;
    }

    return finalImplicants;
    }

    static bool CanCombine(string m1, string m2, out string combined)
    {
    combined = null;
    int diff = 0;

    var temp = m1.Zip(m2, (a, b) =>
    {
        if (a != b) diff++;
        return a == b ? a : '-';
    }).ToArray();

    if (diff == 1)
    {
        combined = new string(temp);
        return true;
    }
    return false;
    }


    static string ConvertToExpression(string implicant, int n)
    {
    var terms = new List<string>();

    for (int i = 0; i < implicant.Length; i++)
    {
        if (implicant[i] == '1')
        {
            terms.Add($"x{i + 1}"); 
        }
        else if (implicant[i] == '0')
        {
            terms.Add($"!x{i + 1}"); 
        }
    }

    return string.Join(" * ", terms); 
    }

}
