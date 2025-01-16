using System;
using System.Collections.Generic;

namespace Calc
{
    internal class Program
    {
        // Удаление дубликатов из списка
        static void deleteDublicates(List<int> numbers)
        {
            int check = 0;
            while (check < numbers.Count)
            {
                for (int i = check + 1; i < numbers.Count; i++)
                {
                    if (numbers[check] == numbers[i])
                    {
                        numbers.RemoveAt(i);
                        i--; // Уменьшаем индекс, чтобы не пропустить следующий элемент
                    }
                }
                check++;
            }
        }

        // Ввод множества с клавиатуры
        static void putSet(List<int> numbers, int left, int right)
        {
            string input;
            int number;

            Console.WriteLine("Введите натуральные числа. Для завершения ввода введите 'S'.");
            while (true)
            {
                Console.Write("Введите число: ");
                input = Console.ReadLine();

                if (input.ToUpper() == "S")
                {
                    break;
                }

                if (int.TryParse(input, out number) && number > left && number < right)
                {
                    numbers.Add(number);
                }
                else
                {
                    Console.WriteLine("Неверные данные, повторите попытку.");
                }
            }
            numbers.Sort();
            deleteDublicates(numbers);
        }

        // Ввод множества случайными числами
        static void randomSet(List<int> numbers, int left, int right, int count)
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int randomNum = random.Next(left + 1, right); // Генерация случайного числа в пределах [left, right)
                numbers.Add(randomNum);
            }
            numbers.Sort();
            deleteDublicates(numbers);
        }

        // Операция пересечения множеств
        static void Intersaction(List<int> M1, List<int> M2)
        {
            M1.Sort();
            M2.Sort();
            List<int> tempArr = new List<int>();
            int i = 0, j = 0;

            while (i < M1.Count && j < M2.Count)
            {
                if (M1[i] < M2[j])
                {
                    i++;
                }
                else if (M1[i] > M2[j])
                {
                    j++;
                }
                else
                {
                    tempArr.Add(M1[i]);
                    i++;
                    j++;
                }
            }

            if (tempArr.Count == 0)
            {
                Console.WriteLine("Пустое множество");
            }
            else
            {
                Console.WriteLine("Результат пересечения:");
                foreach (int num in tempArr)
                {
                    Console.Write(num + " ");
                }
            }
            Console.WriteLine();
        }

        // Операция объединения множеств
        static void Union(List<int> M1, List<int> M2)
        {
            M1.Sort();
            M2.Sort();
            List<int> tempArr = new List<int>(M1);
            foreach (int num in M2)
            {
                if (!tempArr.Contains(num))
                {
                    tempArr.Add(num);
                }
            }
            tempArr.Sort();

            if (tempArr.Count == 0)
                Console.Write("Пустое множество");
            else
            {
                Console.WriteLine("Результат объединения:");
                foreach (int num in tempArr)
                    Console.Write(num + " ");
            }
            Console.WriteLine();
        }

        // Операция разности множеств
        static void Difference(List<int> M1, List<int> M2)
        {
            List<int> tempArr = new List<int>(M1);
            foreach (int num in M2)
            {
                tempArr.Remove(num);
            }

            if (tempArr.Count == 0)
                Console.WriteLine("Пустое множество");
            else
            {
                Console.WriteLine("Результат разности:");
                foreach (int num in tempArr)
                    Console.Write(num + " ");
            }
            Console.WriteLine();
        }

        // Операция симметрической разности множеств
        static void SymmetricDifference(List<int> M1, List<int> M2)
        {
            List<int> tempArr = new List<int>();
            foreach (int num in M1)
            {
                if (!M2.Contains(num))
                {
                    tempArr.Add(num);
                }
            }
            foreach (int num in M2)
            {
                if (!M1.Contains(num))
                {
                    tempArr.Add(num);
                }
            }

            if (tempArr.Count == 0)
                Console.WriteLine("Пустое множество");
            else
            {
                Console.WriteLine("Результат симметрической разности:");
                foreach (int num in tempArr)
                    Console.Write(num + " ");
            }
            Console.WriteLine();
        }

        // Операция дополнения
        static void Inversion(int[] U, List<int> M)
        {
            List<int> tempArr = new List<int>();
            foreach (int num in U)
            {
                if (!M.Contains(num))
                {
                    tempArr.Add(num);
                }
            }

            if (tempArr.Count == 0)
                Console.WriteLine("Пустое множество");
            else
            {
                Console.WriteLine("Результат дополнения:");
                foreach (int num in tempArr)
                    Console.Write(num + " ");
            }
            Console.WriteLine();
        }

        // Проверка принадлежности элемента первому множеству
        static void CheckElementFirstSet(List<int> set1)
        {
            Console.Write("Введите элемент для проверки в первом множестве: ");
            int element;
            if (int.TryParse(Console.ReadLine(), out element))
            {
                if (set1.Contains(element))
                {
                    Console.WriteLine($"Элемент {element} принадлежит первому множеству.");
                }
                else
                {
                    Console.WriteLine($"Элемент {element} не принадлежит первому множеству.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }

        // Проверка принадлежности элемента второму множеству
        static void CheckElementSecondSet(List<int> set2)
        {
            Console.Write("Введите элемент для проверки во втором множестве: ");
            int element;
            if (int.TryParse(Console.ReadLine(), out element))
            {
                if (set2.Contains(element))
                {
                    Console.WriteLine($"Элемент {element} принадлежит второму множеству.");
                }
                else
                {
                    Console.WriteLine($"Элемент {element} не принадлежит второму множеству.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }

        // Проверка на подмножество
        static void CheckSubset(List<int> subset, List<int> set)
        {
            if (subset.All(item => set.Contains(item)))
            {
                Console.WriteLine("Первое множество является подмножеством второго.");
            }
            else
            {
                Console.WriteLine("Первое множество не является подмножеством второго.");
            }
        }

        // Метод для выбора двух множеств
        static List<int> SelectSet(string setName, List<int> setA, List<int> setB, List<int> setC, List<int> setD, List<int> setE)
        {
            switch (setName.ToUpper())
            {
                case "A": return setA;
                case "B": return setB;
                case "C": return setC;
                case "D": return setD;
                case "E": return setE;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    return null;
            }
        }

        // Метод для выполнения всех операций с выбранными множествами
        static void PerformOperations(List<int> set1, List<int> set2, int[] setU)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите операцию:");
                Console.WriteLine("1. Пересечение");
                Console.WriteLine("2. Объединение");
                Console.WriteLine("3. Разность");
                Console.WriteLine("4. Симметрическая разность");
                Console.WriteLine("5. Дополнение для первого множества");
                Console.WriteLine("6. Дополнение для второго множества");
                Console.WriteLine("7. Проверить принадлежность элементу первого множества");
                Console.WriteLine("8. Проверить принадлежность элементу второго множества");
                Console.WriteLine("9. Проверить, является ли подмножеством");
                Console.WriteLine("10. Назад в главное меню");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Intersaction(set1, set2);
                        break;
                    case "2":
                        Union(set1, set2);
                        break;
                    case "3":
                        Difference(set1, set2);
                        break;
                    case "4":
                        SymmetricDifference(set1, set2);
                        break;
                    case "5":
                        Inversion(setU, set1);
                        break;
                    case "6":
                        Inversion(setU, set2);
                        break;
                    case "7":
                        CheckElementFirstSet(set1);
                        break;
                    case "8":
                        CheckElementSecondSet(set2);
                        break;
                    case "9":
                        CheckSubset(set1, set2);
                        break;
                    case "10":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        // Основной метод
        static void ShowMenu(List<int> setA, List<int> setB, List<int> setC, List<int> setD, List<int> setE, int[] setU)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите два множества для операций (A, B, C, D, E) или введите 'Q' для выхода:");
                Console.Write("Выберите первое множество: ");
                string set1Name = Console.ReadLine();
                if (set1Name.ToUpper() == "Q") break;

                List<int> set1 = SelectSet(set1Name, setA, setB, setC, setD, setE);
                if (set1 == null) continue;

                Console.Write("Выберите второе множество: ");
                string set2Name = Console.ReadLine();
                if (set2Name.ToUpper() == "Q") break;

                List<int> set2 = SelectSet(set2Name, setA, setB, setC, setD, setE);
                if (set2 == null) continue;

                PerformOperations(set1, set2, setU);
            }
        }

        // Точка входа в программу
        static void Main(string[] args)
        {
            List<int> setA = new List<int>();
            List<int> setB = new List<int>();
            List<int> setC = new List<int>();
            List<int> setD = new List<int>();
            List<int> setE = new List<int>();

            Console.WriteLine("Введите границы универсума:");
            int leftLimit = int.Parse(Console.ReadLine());
            int rightLimit = int.Parse(Console.ReadLine());
            int[] setU = new int[rightLimit - leftLimit + 1];
            for (int i = 0; i < setU.Length; i++) setU[i] = leftLimit + i;

            // Выбор способа ввода множеств
            Console.WriteLine("Выберите способ ввода множеств:");
            Console.WriteLine("1. Ввод вручную");
            Console.WriteLine("2. Генерация случайных чисел");
            string inputChoice = Console.ReadLine();

            switch (inputChoice)
            {
                case "1":
                    Console.WriteLine("Введите элементы множества A:");
                    putSet(setA, leftLimit, rightLimit);
                    Console.WriteLine("Введите элементы множества B:");
                    putSet(setB, leftLimit, rightLimit);
                    Console.WriteLine("Введите элементы множества C:");
                    putSet(setC, leftLimit, rightLimit);
                    Console.WriteLine("Введите элементы множества D:");
                    putSet(setD, leftLimit, rightLimit);
                    Console.WriteLine("Введите элементы множества E:");
                    putSet(setE, leftLimit, rightLimit);
                    break;
                case "2":
                    Console.WriteLine("Сколько элементов должно быть в каждом множестве?");
                    int count = int.Parse(Console.ReadLine());
                    randomSet(setA, leftLimit, rightLimit, count);
                    randomSet(setB, leftLimit, rightLimit, count);
                    randomSet(setC, leftLimit, rightLimit, count);
                    randomSet(setD, leftLimit, rightLimit, count);
                    randomSet(setE, leftLimit, rightLimit, count);
                    break;
                default:
                    Console.WriteLine("Неверный ввод.");
                    return;
            }

            // Показать меню
            ShowMenu(setA, setB, setC, setD, setE, setU);
        }
    }
}
