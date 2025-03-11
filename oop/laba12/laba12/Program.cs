using ClassLibrary10;
using System;
using System.Collections.Generic;

namespace DemoHashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            HashTable<string, Production> productionTable = new HashTable<string, Production>();
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить элементы");
                Console.WriteLine("2. Добавить элементы случайно");
                Console.WriteLine("3. Удалить несколько элементов");
                Console.WriteLine("4. Очистить таблицу");
                Console.WriteLine("5. Проверить наличие ключа");
                Console.WriteLine("6. Получить значение по ключу");
                Console.WriteLine("7. Вывести все элементы");
                Console.WriteLine("8. Поверхностное копирование таблицы");
                Console.WriteLine("9. Глубокое копирование таблицы");
                Console.WriteLine("10. Разница копирования и клонирования");
                Console.WriteLine("11. Выход");

                Console.Write("Выберите пункт меню: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddMultipleElements(productionTable);
                        break;
                    case "2":
                        AddMultipleRandomElements(productionTable);
                        break;
                    case "3":
                        RemoveMultipleElements(productionTable);
                        break;
                    case "4":
                        productionTable.Clear();
                        Console.WriteLine("Таблица очищена");
                        break;
                    case "5":
                        CheckKeyPresence(productionTable);
                        break;
                    case "6":
                        GetValueByKey(productionTable);
                        break;
                    case "7":
                        DisplayAllElements(productionTable);
                        break;
                    case "8":
                        ShallowCopying(productionTable);
                        break;
                    case "9":
                        DeepCopying(productionTable);
                        break;
                    case "10":
                        CopyAndCloneDemo();
                        break;
                    case "11":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void AddMultipleElements(HashTable<string, Production> table)
        {
            Console.Write("Сколько элементов добавить? ");
            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
            {
                Console.WriteLine("Неверное количество.");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nДобавление элемента {i + 1}:");
                Console.Write("Введите ключ: ");
                string key = Console.ReadLine();

                Console.WriteLine("Выберите тип объекта для элемента:");
                Console.WriteLine("1. Производство");
                Console.WriteLine("2. Фабрика");
                Console.WriteLine("3. Цех");
                Console.WriteLine("4. Мастерская");
                string choice = Console.ReadLine();

                Production production = null;
                switch (choice)
                {
                    case "1":
                        production = new Production();
                        production.RandomInit();
                        break;
                    case "2":
                        production = new Factory();
                        ((Factory)production).RandomInit();
                        break;
                    case "3":
                        production = new Shop();
                        ((Shop)production).RandomInit();
                        break;
                    case "4":
                        production = new Workshop();
                        ((Workshop)production).RandomInit();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Элемент не добавлен.");
                        continue;
                }

                table.Add(key, production);
                Console.WriteLine("Элемент добавлен.");
            }
        }

        static void AddMultipleRandomElements(HashTable<string, Production> table)
        {
            int count;
            Console.Write("Сколько случайных элементов добавить? ");
            if (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
            {
                Console.WriteLine("Неверное количество.");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                string key = $"Random_{i + 1}";
                Production production = GenerateRandomProduction();
                table.Add(key, production);
            }
            Console.WriteLine($"{count} случайных элементов добавлено.");
        }

        static Production GenerateRandomProduction()
        {
            Random rnd = new Random();
            int choice = rnd.Next(4); // 4 варианта: Production, Factory, Shop, Workshop

            switch (choice)
            {
                case 0:
                    Production production = new Production();
                    production.RandomInit();
                    return production;
                case 1:
                    Factory factory = new Factory();
                    factory.RandomInit();
                    return factory;
                case 2:
                    Shop shop = new Shop();
                    shop.RandomInit();
                    return shop;
                case 3:
                    Workshop workshop = new Workshop();
                    workshop.RandomInit();
                    return workshop;
                default:
                    return new Production();
            }
        }

        static void RemoveMultipleElements(HashTable<string, Production> table)
        {
            Console.WriteLine("Введите ключи элементов для удаления через запятую:");
            string input = Console.ReadLine();
            string[] keys = input.Split(',');

            table.Remove(keys);
            Console.WriteLine("Элементы удалены.");
        }

        static void CheckKeyPresence(HashTable<string, Production> table)
        {
            Console.Write("Введите ключ для проверки: ");
            string key = Console.ReadLine();
            Console.WriteLine($"Ключ '{key}' {(table.ContainsKey(key) ? "существует" : "не существует")} в таблице.");
        }

        static void GetValueByKey(HashTable<string, Production> table)
        {
            Console.Write("Введите ключ для получения значения: ");
            string key = Console.ReadLine();
            if (table.TryGetValue(key, out Production value))
            {
                Console.WriteLine($"Значение для ключа '{key}':");
                value.Show();
            }
            else
            {
                Console.WriteLine("Ключ не найден.");
            }
        }

        static void DisplayAllElements(HashTable<string, Production> table)
        {
            Console.WriteLine("\nВсе элементы в хэш-таблице:");
            foreach (var pair in table)
            {
                Console.WriteLine($"Ключ: {pair.Key}");
                pair.Value.Show();
            }
        }

        static void ShallowCopying(HashTable<string, Production> table)
        {
            HashTable<string, Production> shallowCopy = table.ShallowCopy();
            Console.WriteLine("\nПоверхностная копия таблицы:");
            foreach (var pair in shallowCopy)
            {
                Console.WriteLine($"Ключ: {pair.Key}");
                pair.Value.Show();
            }
        }

        static void DeepCopying(HashTable<string, Production> table)
        {
            HashTable<string, Production> deepCopy = table.DeepCopy();
            Console.WriteLine("\nГлубокая копия таблицы:");
            foreach (var pair in deepCopy)
            {
                Console.WriteLine($"Ключ: {pair.Key}");
                pair.Value.Show();
            }
        }

        static void CopyAndCloneDemo()
        {
            // Создаем оригинальную хэш-таблицу и добавляем элементы
            HashTable<string, Production> originalTable = new HashTable<string, Production>();
            originalTable.Add("Factory1", new Factory { Name = "Factory1", Employees = 100 });
            originalTable.Add("Shop1", new Shop { Name = "Shop1", Employees = 50 });
            originalTable.Add("Workshop1", new Workshop { Name = "Workshop1", Employees = 30 });

            Console.WriteLine("Оригинальная таблица:");
            DisplayHashTable(originalTable);

            // Создаем поверхностную копию и глубокий клон
            HashTable<string, Production> shallowCopy = originalTable.ShallowCopy();
            HashTable<string, Production> deepCopy = originalTable.DeepCopy();

            Console.WriteLine("\nПоверхностная копия:");
            DisplayHashTable(shallowCopy);

            Console.WriteLine("\nГлубокий клон:");
            DisplayHashTable(deepCopy);

            // Очищаем оригинальную таблицу
            originalTable.Clear();

            Console.WriteLine("\nПосле очистки оригинальной таблицы:");

            Console.WriteLine("\nОригинальная таблица (пустая):");
            DisplayHashTable(originalTable);

            Console.WriteLine("\nПоверхностная копия (также пустая):");
            DisplayHashTable(shallowCopy);

            Console.WriteLine("\nГлубокий клон (сохраняет данные):");
            DisplayHashTable(deepCopy);
        }

        static void DisplayHashTable(HashTable<string, Production> table)
        {
            foreach (var pair in table)
            {
                Console.WriteLine($"Ключ: {pair.Key}, Значение: {pair.Value.Name}");
            }
        }
    }
}