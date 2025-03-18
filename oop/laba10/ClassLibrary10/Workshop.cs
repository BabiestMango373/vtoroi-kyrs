using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary10
{
    public class Workshop : Production, IInit
    {
        static Random rnd = new Random();

        private string workshopName;

        public string WorkshopName
        {
            get { return workshopName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Console.WriteLine("Ошибка: Название мастерской не может быть пустым");
                workshopName = value;
            }
        }

        private int area;

        public int Area
        {
            get { return area; }
            set
            {
                if (value <= 0)
                    Console.WriteLine("Ошибка: площадь не может быть отрицательной");
                area = value;
            }
        }

        public Workshop() : base()
        {
            Area = 10;
        }

        public Workshop(string name, int employees, string factoryName, double weight, string shopName, string type, string workshopName, int area) : base(name, employees)
        {
            WorkshopName = workshopName;
            Area = area;        
        }

        public Workshop(Workshop workshop) : base(workshop)
        {
            WorkshopName = workshop.Name;
            Area = workshop.Area;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Название мастерской: {workshopName}");
            Console.WriteLine($"Площадь мастерскуой: {area}");
        }

        public override void Init()
        {
            base.Init();
            do
            {
                Console.WriteLine("Введите название цеха: ");
                WorkshopName = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(WorkshopName));

            Area = ReadPosInt("Введите площадь мастерской: ");
        }

        public override void RandomInit()
        {
            base.RandomInit();
            WorkshopName = "Мастерская_" + rnd.Next(1, 100);
            Area = rnd.Next(10, 201);
        }

        public override bool Equals(object? obj)
        {
            if(!base.Equals(obj))
                return false;
            Workshop workshop = obj as Workshop;
            if(workshop == null)  return false;
            return this.WorkshopName == workshop.WorkshopName && this.Area == workshop.Area;

        }

        private int ReadPosInt(string prompt) //функция для проверки на положительное число
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

        public override object ShallowCopy()
        {
            return base.MemberwiseClone();
        }

        public override object Clone()
        {
            return new Workshop
            {
                Name = this.Name,
                Employees = this.Employees,
                WorkshopName = this.WorkshopName,
                Area = this.Area
            };
        }

        public override string ToString()
        {
            return base.ToString() + $"{workshopName}, {area}";
        }
    }
}
        
    