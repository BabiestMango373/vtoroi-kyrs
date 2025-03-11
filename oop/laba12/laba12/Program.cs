using System;

class Program
{
    static void Main()
    {
        // Создаем хэш-таблицу
        var фрукты = new HashTable<string, int>();

        // Добавляем элементы
        фрукты.Add("Яблоко", 1);
        фрукты["Банан"] = 2;
        фрукты.Add("Вишня", 3);

        // Выводим элементы
        Console.WriteLine("Элементы в хэш-таблице:");
        foreach (var пара in фрукты)
        {
            Console.WriteLine($"{пара.Key}: {пара.Value}");
        }

        // Получаем значение по ключу
        Console.WriteLine("\nПолучаем значение для 'Яблоко': " + фрукты["Яблоко"]);

        // Проверяем наличие ключа
        Console.WriteLine("\nСодержит ли ключ 'Банан': " + фрукты.ContainsKey("Банан"));
        Console.WriteLine("Содержит ли ключ 'Виноград': " + фрукты.ContainsKey("Виноград"));

        // Удаляем элемент
        Console.WriteLine("\nУдаляем 'Вишня'");
        фрукты.Remove("Вишня");

        // Выводим обновленные элементы
        Console.WriteLine("\nЭлементы после удаления:");
        foreach (var пара in фрукты)
        {
            Console.WriteLine($"{пара.Key}: {пара.Value}");
        }

        // Пытаемся получить значение для несуществующего ключа
        try
        {
            Console.WriteLine("\nПолучаем значение для 'Виноград': " + фрукты["Виноград"]);
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("\nКлюч 'Виноград' не найден");
        }
    }
}