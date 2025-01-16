using ClassLibrary10;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Laba11
{
    class Program
    {
        protected static string MeasureSearchTime<T>(Stack<T> stack, T item)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool found = stack.Contains(item);
            stopwatch.Stop();
            return $"{stopwatch.ElapsedTicks} тиков Найден: {found}";
        }

        protected static string MeasureSearchTime<TKey, TValue>(SortedDictionary<TKey, TValue> dictionary, TKey key)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool found = dictionary.ContainsKey(key);
            stopwatch.Stop();
            return $"{stopwatch.ElapsedTicks} тиков Найден: {found}";
        }

        protected static string MeasureSearchTime<TKey, TValue>(SortedDictionary<TKey, TValue> dictionary, TValue value)
        {
            if (value == null) return "0 тиков Найден: false";
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool found = dictionary.ContainsValue(value);
            stopwatch.Stop();
            return $"{stopwatch.ElapsedTicks} тиков Найден: {found}";
        }

        public static void TestSearchTimes(ref TestCollections test)
        {
            Console.WriteLine("\n--- Время поиска ---");

            // Stack<Production>
            var productionStackArray = test.productionStack.ToArray();
            var firstStackProduction = productionStackArray[^1]; 
            var midStackProduction = productionStackArray[productionStackArray.Length / 2];
            var lastStackProduction = productionStackArray[0];
            Production missingProduction = new Production("Missing", 0);

            Console.WriteLine("\nStack<Production>:");
            Console.WriteLine($"Первый элемент: {MeasureSearchTime(test.productionStack, firstStackProduction).Clone()}");
            Console.WriteLine($"Центральный элемент: {MeasureSearchTime(test.productionStack, midStackProduction).Clone()}");
            Console.WriteLine($"Последний элемент: {MeasureSearchTime(test.productionStack, lastStackProduction).Clone()}");
            Console.WriteLine($"Не существующий элемент: {MeasureSearchTime(test.productionStack, missingProduction)}");

            // Stack<string>
            var stringStackArray = test.stringStack.ToArray();
            var firstStackString = stringStackArray[^1];
            var midStackString = stringStackArray[stringStackArray.Length / 2];
            var lastStackString = stringStackArray[0];
            string missingString = "Missing";

            Console.WriteLine("\nStack<string>:");
            Console.WriteLine($"Первый элемент: {MeasureSearchTime(test.stringStack, firstStackString).Clone()}");
            Console.WriteLine($"Центральный элемент: {MeasureSearchTime(test.stringStack, midStackString).Clone()}");
            Console.WriteLine($"Последний элемент: {MeasureSearchTime(test.stringStack, lastStackString).Clone()}");
            Console.WriteLine($"Не существующий элемент: {MeasureSearchTime(test.productionStack, missingProduction)}");

            // SortedDictionary<Production, Factory> ключи
            var productionKeys = test.productionDictionary.Keys.ToArray();
            var firstDictKey = productionKeys[0];
            var midDictKey = productionKeys[productionKeys.Length / 2];
            var lastDictKey = productionKeys[^1];

            Console.WriteLine("\nSortedDictionary<Production, Factory> (ключ):");
            Console.WriteLine($"Первый ключ: {MeasureSearchTime(test.productionDictionary, firstDictKey).Clone()}");
            Console.WriteLine($"Центральный ключ: {MeasureSearchTime(test.productionDictionary, midDictKey).Clone()}");
            Console.WriteLine($"Последний ключ: {MeasureSearchTime(test.productionDictionary, lastDictKey).Clone()}");
            Console.WriteLine($"Не существующий ключ: {MeasureSearchTime(test.productionDictionary, missingProduction)}");

            // SortedDictionary<Production, Factory> значение
            var factoryValues = test.productionDictionary.Values.ToArray();
            var firstDictValue = factoryValues[0];
            var midDictValue = factoryValues[factoryValues.Length / 2];
            var lastDictValue = factoryValues[^1];

            Console.WriteLine("\nSortedDictionary<Production, Factory> (значение):");
            Console.WriteLine($"Первое значение: {MeasureSearchTime(test.productionDictionary, firstDictValue).Clone()}");
            Console.WriteLine($"Центральное значение: {MeasureSearchTime(test.productionDictionary, midDictValue).Clone()}");
            Console.WriteLine($"Последнее значение: {MeasureSearchTime(test.productionDictionary, lastDictValue).Clone()}");
            Console.WriteLine($"Не существующее значение: {MeasureSearchTime(test.productionDictionary, missingProduction)}");

            // SortedDictionary<string, Factory>
            var stringKeys = test.stringDictionary.Keys.ToArray();
            var firstStringKey = stringKeys[0];
            var midStringKey = stringKeys[stringKeys.Length / 2];
            var lastStringKey = stringKeys[^1];

            Console.WriteLine("\nSortedDictionary<string, Factory>:");
            Console.WriteLine($"Первый ключ: {MeasureSearchTime(test.stringDictionary, firstStringKey).Clone()}");
            Console.WriteLine($"Центральный ключ: {MeasureSearchTime(test.stringDictionary, midStringKey).Clone()}");
            Console.WriteLine($"Последний ключ: {MeasureSearchTime(test.stringDictionary, lastStringKey).Clone()}");
            Console.WriteLine($"Не существующий ключ: {MeasureSearchTime(test.stringDictionary, missingString)}");
        }

        static void Main(string[] args)
        {
            TestCollections test = new TestCollections();
            while (true)
            {
                Console.WriteLine("\n1 - Добавить элементы");
                Console.WriteLine("2 - Тест времени поиска");
                Console.WriteLine("3 - Выход");

                int option = int.TryParse(Console.ReadLine(), out var result) ? result : -1;

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Введите количество элементов для добавления:");
                        int count = int.TryParse(Console.ReadLine(), out var countResult) ? countResult : 0;
                        test.RandomInit(count);
                        Console.WriteLine($"{count} элементов добавлено.\n");
                        break;

                    case 2:
                        if (test.productionStack.Count > 0)
                        {
                            TestSearchTimes(ref test);
                        }
                        else
                        {
                            Console.WriteLine("Коллекции пусты. Добавьте элементы сначала.\n");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Выход из программы.");
                        return;

                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.\n");
                        break;
                }
            }
        }
    }
}
