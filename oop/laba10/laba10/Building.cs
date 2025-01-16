using ClassLibrary10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace laba10
{
    public class Building : IInit
    {
        private string address;
        private int floors;
        public string[] Feature { get; set; } // Поле ссылочного типа 

        public string Address
        {
            get { return address; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Console.WriteLine("Ошибка: Адрес не может быть пустым");
                address = value;
            }
        }

        public int Floors
        {
            get { return floors; }
            set
            {
                if (value < 0)
                    Console.WriteLine("Ошибка: Введите положительное число");
                floors = value;
            }
        }

        public Building()
        {
            Feature = new string[] { "Не указано" }; 
        }

        public void Init()
        {
            do
            {
                Console.WriteLine("Введите наименование здания: ");
                Address = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(Address));

            Floors = ReadPosInt("Введите количество этажей: ");

            Console.WriteLine("Введите особенности здания через запятую:");
            Feature = Console.ReadLine().Split(',');
        }

        public void RandomInit()
        {
            Random rnd = new Random();
            Address = "Улица_" + rnd.Next(1, 100);
            Floors = rnd.Next(1, 22);

            string[] allFeatures = { "подвал", "лифт", "парковка", "сауна", "конференц-зал", "смотровая площадка" };
            Feature = new string[] { allFeatures[rnd.Next(allFeatures.Length)] }; // Случайный выбор одной особенности
        }

        public void Show()
        {
            Console.WriteLine($"\nАдрес здания: {Address}, Количество этажей: {Floors}, Особенности: {string.Join(", ", Feature)}");
        }

        // Метод поверхностного копирования
        public Building ShallowCopy()
        {
            return (Building)this.MemberwiseClone();
        }

        // Метод глубокого копирования
        public object Clone()
        {
            // Создаем новый объект Building
            return new Building
            {
                Address = this.Address,
                Floors = this.Floors,
                Feature = (string[])this.Feature.Clone() // Копируем массив отдельно
            };
        }

        private int ReadPosInt(string prompt) // Проверка на положительное число
        {
            int result = 0;
            while (true)
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result > 0)
                    return result;
                Console.WriteLine("Ошибка: Введите положительное число");
            }
        }
    }
}

