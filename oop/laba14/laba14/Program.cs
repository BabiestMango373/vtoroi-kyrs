using System;
using System.Linq;
using System.Collections.Generic;
using ClassLibrary10;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace laba14
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<string, List<Production>> corporation = new SortedDictionary<string, List<Production>>();

            corporation["Фабрика"] = new List<Production>();
            corporation["Цех"] = new List<Production>();
            corporation["Мастерская"] = new List<Production>();

            for(int i = 0; i < 4; i++)
            {
                Factory factory = new Factory();
                factory.RandomInit();
                corporation["Фабрика"].Add(factory);

                Shop shop = new Shop();
                shop.RandomInit();
                corporation["Цех"].Add(shop);

                Workshop workshop = new Workshop();
                workshop.RandomInit();
                corporation["Мастерская"].Add(workshop);
            }
            

            foreach(var branch in corporation)
            {
                Console.WriteLine($"Филиал: {branch.Key}");
                foreach(var items in branch.Value)
                {
                    Console.WriteLine($"Значение: {items}");
                }
            }

            Console.WriteLine("\n1 Запрос на выборку, сотредников > 500");
            TWhere(corporation);
            Console.WriteLine("\n2 Зпрос на объединение филиалов");
            TUnion(corporation);
            Console.WriteLine("\n3 Запрос на среднее количество сотрудников");
            TAverage(corporation);
            Console.WriteLine("\n4 Запрос с let");
            TLet(corporation);
            Console.WriteLine("\n5 Запрос на группировку по количеству сотрудников");
            TGroupBy(corporation);
            Console.WriteLine("\nЗапросы в хэш-таблице");
            THashTable();



            static void TWhere(SortedDictionary<string, List<Production>> corporation)
            {
                Stopwatch stopwatch = new Stopwatch();

                //LINQ
                Console.WriteLine("\nLINQ запрос");
                stopwatch.Start();
                var linqWhere = (from branch in corporation
                            from item in branch.Value
                            where item.Employees > 500
                            select item).ToList();
                stopwatch.Stop();
                long linqTime = stopwatch.ElapsedTicks;
                foreach (var i in linqWhere.Take(3))
                {
                    i.Show();
                }

                //Методы расширения
                Console.WriteLine("\nМетоды расширения");
                stopwatch.Restart();
                var exp = corporation.SelectMany(b => b.Value)
                    .Where(i => i.Employees > 500);
                stopwatch.Stop();
                long expTime = stopwatch.ElapsedTicks;
                foreach(var i in exp.Take(3))
                {
                    i.Show();
                }

                //Цикл 
                Console.WriteLine("\nЦикл");
                
                var cycle = new List<Production>();
                stopwatch.Restart();

                foreach(var branch in corporation)
                {
                    foreach(var item in branch.Value)
                    {
                        if (item.Employees > 500)
                            cycle.Add(item);
                    }
                }
                stopwatch.Stop();
                long cycleTime = stopwatch.ElapsedTicks;
                foreach(var i in cycle.Take(3))
                {
                    i.Show();
                }

                Console.WriteLine($"\nВремя выполнения LINQ: {linqTime} тиков");
                Console.WriteLine($"Время выполнения Методов расширения: {expTime} тиков");
                Console.WriteLine($"Время выполнения циклом: {cycleTime} тиков");
            }


            static void TUnion(SortedDictionary<string, List<Production>> corporation)
            {
                var factories = corporation["Фабрика"];
                var shops = corporation["Цех"];
                Stopwatch stopwatch = new Stopwatch();

                //LINQ
                Console.WriteLine("\nLINQ запрос");
                stopwatch.Start();

                var linqUnion = (from f in factories select f)
                                 .Union(from s in shops select s).ToList();
                stopwatch.Stop();
                long linqTime = stopwatch.ElapsedTicks;
                Console.WriteLine($"Объединение элементов: {linqUnion.Count}");
                foreach (var i in linqUnion.Take(6))
                {
                    i.Show();
                }

                //Методы расширения
                Console.WriteLine("\nМетоды расширения");
                stopwatch.Restart();
                var exp = factories.Union(shops).ToList();
                stopwatch.Stop();
                long expTime = stopwatch.ElapsedTicks;
                Console.WriteLine($"Объединение элементов: {exp.Count}");
                foreach (var i in exp.Take(6))
                {
                    i.Show();
                }

                Console.WriteLine($"\nВремя выполнения LINQ: {linqTime} тиков");
                Console.WriteLine($"Время выполнения Методов расширения: {expTime} тиков");
            }

            static void TAverage(SortedDictionary<string, List<Production>> corporation)
            {
                var allProductions = corporation.Values.SelectMany(list => list).ToList();
                Stopwatch stopwatch = new Stopwatch();

                //LINQ
                Console.WriteLine("\nLINQ запрос");
                stopwatch.Start();
                var linqAvg = (from item in allProductions
                               select item.Employees).Average();
                stopwatch.Stop();
                long linqTime = stopwatch.ElapsedTicks;
                Console.WriteLine($"Среднее количество работников: {linqAvg}");

                //Метод расширения
                Console.WriteLine("\nМетод расширения");
                stopwatch.Restart();
                var exp = allProductions.Select(i => i.Employees).Average();
                stopwatch.Stop();
                long expTime = stopwatch.ElapsedTicks;
                Console.WriteLine($"Среднее количество работников: {exp}");

                Console.WriteLine($"\nВремя выполнения LINQ: {linqTime} тиков");
                Console.WriteLine($"Время выполнения Методов расширения: {expTime} тиков");
            }

            static void TLet(SortedDictionary<string, List<Production>> corporation)
            {
                Stopwatch stopwatch = new Stopwatch();

                //LINQ
                Console.WriteLine("\nLINQ запрос");
                stopwatch.Start();
                var linq = (from branch in corporation
                            from item in branch.Value
                            let sizeCategory = item.Employees > 500 ? "Крупное" : "Маленькое"
                            select new
                            {
                                item.Name, 
                                item.Employees, 
                                SizeCategory = sizeCategory
                            });
                stopwatch.Stop();
                long linqTime = stopwatch.ElapsedTicks;
                foreach(var item in linq.Take(3))
                {
                    Console.WriteLine($"{item.Name}: {item.Employees} сотрудников -> {item.SizeCategory} производство");
                }

                //Метод расширения
                Console.WriteLine("\nМетод расширения");
                stopwatch.Restart();
                var exp = corporation.SelectMany(b => b.Value)
                    .Select(i => new
                    {
                        i.Name,
                        i.Employees,
                        SizeCategory = i.Employees > 500 ? "Крупное" : "Маленькое"
                    });
                stopwatch.Stop();
                long expTime = stopwatch.ElapsedTicks;
                foreach (var item in exp.Take(3))
                {
                    Console.WriteLine($"{item.Name}: {item.Employees} сотрудников -> {item.SizeCategory} производство");
                }

                Console.WriteLine($"\nВремя выполнения LINQ: {linqTime} тиков");
                Console.WriteLine($"Время выполнения Методов расширения: {expTime} тиков");
            }

            static void TGroupBy(SortedDictionary<string, List<Production>> corporation)
            {
                var allProductions = corporation.Values.SelectMany(list => list);
                Stopwatch stopwatch = new Stopwatch();

                //LINQ
                Console.WriteLine("\nLINQ запрос");
                stopwatch.Start();
                var linq = (from item in allProductions
                            group item by item.Employees / 100 into g
                            select new
                            {
                                Range = g.Key,
                                Count = g.Count(),
                                Items = g.ToList()
                            });
                stopwatch.Stop();
                long linqTime = stopwatch.ElapsedTicks;
                foreach(var group in linq)
                {
                    Console.WriteLine($"\nДиапозон: {group.Range * 100} - {group.Range * 100 + 99}, Количество: {group.Count}");
                    foreach(var item in group.Items)
                    {
                        item.Show();
                    }
                }

                //Метод расширения
                Console.WriteLine("\nМетод расширения");
                stopwatch.Restart();
                var exp = allProductions.GroupBy(i => i.Employees / 100)
                    .Select(g => new
                    {
                        Range = g.Key,
                        Count = g.Count(),
                        Items = g.ToList()
                    });
                stopwatch.Stop();
                long expTime = stopwatch.ElapsedTicks;
                foreach (var group in exp)
                {
                    Console.WriteLine($"\nДиапозон: {group.Range * 100} - {group.Range * 100 + 99}, Количество: {group.Count}");
                    foreach (var item in group.Items)
                    {
                        item.Show();
                    }
                }
                Console.WriteLine($"\nВремя выполнения LINQ: {linqTime} тиков");
                Console.WriteLine($"Время выполнения Методов расширения: {expTime} тиков");

            }

            static void THashTable()
            {
                var collection = new HashTable<string, Production>
                {
                    {("Фабрика"), new Factory("Производство_123", 123, "Фабрика_123", 123.123) },
                    {("Цех"), new Shop("Производство_2", 78, "Фабрика_2", 123.45, "Цех_1", "основной") },
                    {("Мастерская"), new Workshop("Производство_4", 234, "Фабрика_3", 657.9786, "Цех_2", "вспомогательный", "Мастерская_1", 70)}
                };

                Console.WriteLine("\nМетод расширения Where (сторудников > 300 ):");
                var hashWhere = collection.Where(kvp => kvp.Value.Employees > 100);
                foreach(var item in hashWhere)
                {
                    Console.WriteLine($"{item.Key} - {item.Value.Name} - {item.Value.Employees} сотрудников");
                }

                int countOfCollection = collection.Count(kvp => kvp.Value.Employees > 200);
                Console.WriteLine($"\nКоличество производств, где сотрудников > 200 : {countOfCollection}");

                double maxEmployees = collection.Max(kvp => kvp.Value.Employees);
                double minEmployees = collection.Min(kvp => kvp.Value.Employees);
                Console.WriteLine($"Минимальное число сотрудников: {minEmployees}");
                Console.WriteLine($"Максимальное число сотрудников: {maxEmployees}");

                Console.WriteLine("\nСортировка методом расширения по количеству сотрудников:");
                var sortedCollection = collection.OrderBy(kvp => kvp.Value.Employees);
                foreach (var item in sortedCollection)
                {
                    Console.WriteLine($"{item.Value.Name} - {item.Value.Employees} сотрудника");
                }
            }

        }
    }
}

 