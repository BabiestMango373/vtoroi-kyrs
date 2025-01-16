using System;
using System.Collections.Generic;
using ClassLibrary10;

namespace laba10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Production[] productions = null; 
            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Заполнить массив (случайно)");
                Console.WriteLine("2 - Заполнить массив (с клавиатуры)");
                Console.WriteLine("3 - Просмотреть массив (виртуальные функции)");
                Console.WriteLine("4 - Просмотреть массив (обычные функции)");
                Console.WriteLine("5 - Сравнить два элемента массива");
                Console.WriteLine("6 - Суммарный вес всех деталей");
                Console.WriteLine("7 - Количество рабочих в заданном цехе");
                Console.WriteLine("8 - Наименование всех цехов");
                Console.WriteLine("9 - Сортировка массива по наименованию (IComparable)");
                Console.WriteLine("10 - Сортировка массива по количеству работников (IComparer)");
                Console.WriteLine("11 - Поиск элемента в массиве по наименованию");
                Console.WriteLine("12 - Бинарный поиск");
                Console.WriteLine("13 - Массив элементов IInit");
                Console.WriteLine("14 - Копирование и клонирование");
                Console.WriteLine("15 - Выход");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        productions = FillArrayRandomly();
                        Console.WriteLine("Массив успешно заполнен случайными данными");
                        break;

                    case "2":
                        productions = FillArray();
                        Console.WriteLine("Массив успешно заполнен с клавиатуры");
                        break;

                    case "3":
                        if (productions == null)
                        {
                            Console.WriteLine("Ошибка: Массив не заполнен");
                        }
                        else
                        {
                            Console.WriteLine("\nПросмотр массива с помощью виртуальной функции функций:");
                            foreach (var production in productions)
                            {
                                production.Show();
                            }
                        }
                        break;

                    case "4":
                        if (productions == null)
                        {
                            Console.WriteLine("Ошибка: Массив не заполнен");
                        }
                        else
                        {
                            Console.WriteLine("\nПросмотр массива с помощью не виртуальной функции функций:");
                            foreach (var production in productions)
                            {
                                production.NonVirtualShow();
                            }
                        }
                        break;

                    case "5":
                        if (productions == null)
                        {
                            Console.WriteLine("Ошибка: Массив не заполнен");
                        }
                        else
                        {
                            EqualsElements(productions);
                        }
                        break;

                    case "6":
                        if (productions == null)
                        {
                            Console.WriteLine("Ошибка: Массив не заполнен");
                        }
                        else
                        {
                            double totalWeight = GetTotalWeight(productions);
                            Console.WriteLine($"Суммарный вес всех деталей: {totalWeight} кг");
                        }
                        break;

                    case "7":
                        if (productions == null)
                        {
                            Console.WriteLine("Ошибка: Массив не заполнен");
                        }
                        else
                        {
                            Console.Write("Введите имя цеха: ");
                            string shopName = Console.ReadLine();
                            int employees = GetEmployeeCountInShop(productions, shopName);
                            Console.WriteLine($"Количество рабочих в цехе \"{shopName}\": {employees}");
                        }
                        break;

                    case "8":
                        if (productions == null)
                        {
                            Console.WriteLine("Ошибка: Массив не заполнен");
                        }
                        else
                        {
                            List<string> allShops = GetAllShopNames(productions);
                            if (allShops.Count > 0)
                            {
                                Console.WriteLine("Список всех цехов в массиве:");
                                foreach (var shop in allShops)
                                {
                                    Console.WriteLine($"- {shop}");
                                }
                            }
                        }
                        break;

                    case "9":
                        if (productions == null)
                        {
                            Console.WriteLine("Ошибка: Массив не заполнен");
                        }
                        else
                        {
                            Array.Sort(productions); // Сортировка с использованием IComparable
                            Console.WriteLine("\nМассив отсортирован по имени:");
                            foreach (var item in productions)
                            {
                                item.Show();
                            }
                        }
                        break;

                    case "10":
                        if (productions == null)
                        {
                            Console.WriteLine("Ошибка: Массив не заполнен");
                        }
                        else
                        {
                            Array.Sort(productions, new ProductionComparer()); // Сортировка с использованием IComparer
                            Console.WriteLine("\nМассив отсортирован по количеству работников:");
                            foreach (var item in productions)
                            {
                                item.Show();
                            }
                        }
                        break;

                    case "11":
                        if (productions == null)
                        {
                            Console.WriteLine("Ошибка: Массив не заполнен");
                        }
                        else
                        {
                            Console.Write("Введите имя производства для поиска: ");
                            string searchName = Console.ReadLine();
                            Production found = Array.Find(productions, p => p.Name == searchName);

                            if (found != null)
                            {
                                Console.WriteLine("\nНайденный элемент:");
                                found.Show();
                            }
                            else
                            {
                                Console.WriteLine("Элемент не найден");
                            }
                        }
                        break;

                    case "12": // Бинарный поиск
                        if (productions == null)
                        {
                            Console.WriteLine("Ошибка: Массив не заполнен");
                        }
                        else
                        {
                            Console.WriteLine("Имя объекта для поиска: ");
                            string searchName = Console.ReadLine();
                            Production found = BinarySearch(productions, searchName);
                            if (found == null)
                                Console.WriteLine("Объект не найден");
                            else
                            {
                                Console.WriteLine("\nОбъект найден: ");
                                found.Show();
                            }
                        }
                        break;

                    case "13": // Работа с массивом IInit
                        IInit[] initArray = new IInit[]
                        {
                            new Production(),
                            new Factory(),
                            new Shop(),
                            new Workshop(),
                            new Building() 
                        };

                        Console.WriteLine("\nДемонстрация работы методов Init():");
                        foreach (var item in initArray)
                        {
                            item.Init(); 
                        }

                        Console.WriteLine("\nДемонстрация работы методов RandomInit():");
                        foreach (var item in initArray)
                        {
                            item.RandomInit(); 
                        }

                        Console.WriteLine("\nРезультаты после RandomInit():");
                        foreach (var item in initArray)
                        {
                            if (item is Production productionItem)
                            {
                                productionItem.Show();
                            }
                            else if (item is Building buildingItem)
                            {
                                buildingItem.Show();
                            }
                        }
                        break;

                    case "14":
                        Building origBuilding = new Building
                        {
                            Address = "Торговый центр",
                            Floors = 5,
                            Feature = new string[] { "лифт"}
                        };

                        Console.WriteLine("Оригинальный объект:");
                        origBuilding.Show();

                        // Поверхностное копирование
                        Building shallowCopy = origBuilding.ShallowCopy();
                        Console.WriteLine("\nПоверхностная копия:");
                        shallowCopy.Show();

                        // Глубокое копирование
                        Building deepCopy = (Building)origBuilding.Clone();
                        Console.WriteLine("\nГлубокая копия:");
                        deepCopy.Show();

                        // Изменяем оригинальный объект
                        origBuilding.Address = "Измененный торговый центр";
                        origBuilding.Feature[0] = "парковка"; 

                        Console.WriteLine("\nПосле изменения оригинала:");
                        Console.WriteLine("Оригинальный объект:");
                        origBuilding.Show();

                        Console.WriteLine("\nПоверхностная копия:");
                        shallowCopy.Show();

                        Console.WriteLine("\nГлубокая копия:");
                        deepCopy.Show();
                        break;


                    case "15":
                        Console.WriteLine("Выход из программы");
                        return;

                    default:
                        Console.WriteLine("Ошибка: Неверный ввод");
                        break;
                }
            }
        }

        // Метод для случайного заполнения массива
        static Production[] FillArrayRandomly()
        {
            Console.Write("Введите размер массива: ");
            int size = ReadPosInt();
            Production[] array = new Production[size];
            Random rnd = new Random();

            for (int i = 0; i < size; i++)
            {
                int type = rnd.Next(4); 
                switch (type)
                {
                    case 0: array[i] = new Production(); break;
                    case 1: array[i] = new Factory(); break;
                    case 2: array[i] = new Shop(); break;
                    case 3: array[i] = new Workshop(); break;
                }
                array[i].RandomInit();
            }

            return array;
        }

        // Метод для ручного заполнения массива
        static Production[] FillArray()
        {
            Console.Write("Введите размер массива: ");
            int size = ReadPosInt();
            Production[] array = new Production[size]; 

            for (int i = 0; i < size; i++)
            {
                Production obj = null;

                while (obj == null) 
                {
                    Console.WriteLine($"\nВыберите тип объекта для элемента {i + 1}:");
                    Console.WriteLine("1 - Production");
                    Console.WriteLine("2 - Factory");
                    Console.WriteLine("3 - Shop");
                    Console.WriteLine("4 - Workshop");
                    Console.Write("Ваш выбор: ");

                    string choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        obj = new Production();
                    }
                    else if (choice == "2")
                    {
                        obj = new Factory();
                    }
                    else if (choice == "3")
                    {
                        obj = new Shop();
                    }
                    else if (choice == "4")
                    {
                        obj = new Workshop();
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Неверный ввод");
                    }
                }

                obj.Init(); 
                array[i] = obj; 
            }

            return array; 
        }

        // Метод для сравнения двух элементов массива
        static void EqualsElements(Production[] array)
        {
            Console.Write("Введите индекс первого элемента: ");
            int index1 = ReadPosInt() - 1;
            Console.Write("Введите индекс второго элемента: ");
            int index2 = ReadPosInt() - 1;

            if (index1 < 0 || index1 >= array.Length || index2 < 0 || index2 >= array.Length)
            {
                Console.WriteLine("Ошибка: Индексы находятся вне границ массива");
                return;
            }

            if (array[index1].Equals(array[index2]))
            {
                Console.WriteLine("Элементы равны");
            }
            else
            {
                Console.WriteLine("Элементы не равны");
            }
        }

        // Суммарный вес всех деталей
        static double GetTotalWeight(Production[] array)
        {
            double totalWeight = 0;

            foreach (var item in array)
            {
                Factory factory = item as Factory; 
                if (factory != null) 
                {
                    totalWeight += factory.Weight;
                }
            }

            return totalWeight;
        }

        // Количество рабочих в заданном цехе
        static int GetEmployeeCountInShop(Production[] array, string shopName)
        {
            foreach (var item in array)
            {
                if (item is Shop shop && shop.ShopName == shopName)
                {
                    return shop.Employees;
                }
            }

            Console.WriteLine($"Цех с именем \"{shopName}\" не найден");
            return 0;
        }

        // Названия всех цехов 
        static List<string> GetAllShopNames(Production[] array)
        {
            List<string> shopNames = new List<string>();

            foreach (var item in array)
            {
                if (item is Shop shop) 
                {
                    shopNames.Add(shop.ShopName); 
                }
            }

            if (shopNames.Count == 0)
            {
                Console.WriteLine("Цехов нет");
            }

            return shopNames;
        }

        // Метод для безопасного ввода положительного числа
        static int ReadPosInt()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result) || result <= 0)
            {
                Console.WriteLine("Ошибка: Введите положительное целое число");
            }
            return result;
        }

        // Бинарный поиск
        static Production BinarySearch(Production[] array, string name)
        {
            Array.Sort(array);

            Production searchKey = new Production { Name = name };

            int index = Array.BinarySearch(array, searchKey);

            if (index >= 0)
            {
                return array[index];
            }

            return null;
        }


    }
}
