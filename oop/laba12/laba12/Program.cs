﻿using ClassLibrary10;
using System;
using System.Collections.Generic;

namespace lab_12
{
    class Program
    {
        static void Main(string[] args)
        {
            HashTable<string, Production> productionTable = new HashTable<string, Production>();
            bool isRunning = true;

            while (isRunning)
            {
                AddMultipleRandomElements(productionTable);
                DisplayHashTable(productionTable);
                RemoveMultipleElements(productionTable);
                CheckKeyPresence(productionTable);
                GetValueByKey(productionTable);
                DisplayHashTable(productionTable);
                HashTable<string, Production> shallowCopy = productionTable.ShallowCopy();
                HashTable<string, Production> deepCopy = productionTable.DeepCopy();
                Console.WriteLine("\nПоверхностная копия:");
                DisplayHashTable(shallowCopy);
                Console.WriteLine("\nГлубокая копия:");
                DisplayHashTable(deepCopy);
                productionTable.Clear();
                Console.WriteLine("\n----------------\nТаблица очищена\n----------------\n");
                Console.WriteLine("\nПоверхностная копия:");
                DisplayHashTable(shallowCopy);
                Console.WriteLine("\nГлубокая копия:");
                DisplayHashTable(deepCopy);
                isRunning = false;
            }
        }

        //static void AddMultipleElements(HashTable<string, Production> table)
        //{
        //    Console.Write("Сколько элементов добавить? ");
        //    if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
        //    {
        //        Console.WriteLine("Неверное количество.");
        //        return;
        //    }

        //    for (int i = 0; i < count; i++)
        //    {
        //        Console.WriteLine($"\nДобавление элемента {i + 1}:");
        //        Console.Write("Введите ключ: ");
        //        string key = Console.ReadLine();

        //        Console.WriteLine("Выберите тип объекта для элемента:");
        //        Console.WriteLine("1. Производство");
        //        Console.WriteLine("2. Фабрика");
        //        Console.WriteLine("3. Цех");
        //        Console.WriteLine("4. Мастерская");
        //        string choice = Console.ReadLine();

        //        Production production = null;
        //        switch (choice)
        //        {
        //            case "1":
        //                production = new Production();
        //                production.RandomInit();
        //                break;
        //            case "2":
        //                production = new Factory();
        //                ((Factory)production).RandomInit();
        //                break;
        //            case "3":
        //                production = new Shop();
        //                ((Shop)production).RandomInit();
        //                break;
        //            case "4":
        //                production = new Workshop();
        //                ((Workshop)production).RandomInit();
        //                break;
        //            default:
        //                Console.WriteLine("Неверный выбор. Элемент не добавлен.");
        //                continue;
        //        }

        //        table.Add(key, production);
        //        Console.WriteLine("Элемент добавлен");
        //    }
        //}

        static void AddMultipleRandomElements(HashTable<string, Production> table)
        {
            int count;
            Console.Write("Количество элементов: ");
            while(!int.TryParse(Console.ReadLine(), out count) || count <= 0)
            {
                Console.WriteLine("Неверный ввод, введите количество элементов: ");
            }

            for (int i = 0; i < count; i++)
            {
                string key = $"Key_{i + 1}";
                Production production = GenerateRandomProduction();
                table.Add(key, production);
            }
            Console.WriteLine($"{count} случайных элементов добавлено.");
        }

        static Production GenerateRandomProduction()
        {
            Random rnd = new Random();
            int choice = rnd.Next(4);

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
        static void DisplayHashTable(HashTable<string, Production> table)
        {
            foreach (var pair in table)
            {
                Console.WriteLine($"\nКлюч: {pair.Key}");
                pair.Value.Show();
                Console.WriteLine("\n----------------");
            }
        }
    }
}