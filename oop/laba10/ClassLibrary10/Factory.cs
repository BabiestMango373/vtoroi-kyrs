using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary10
{
    public class Factory : Production, IInit
    {
        static Random rnd = new Random();
        private string factoryName;

        public string FactoryName
        {
            get { return factoryName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Console.WriteLine("Ошибка: Название фабркии не может быть пустым");
                factoryName = value;
            }
        }

        private double weight;

        public double Weight //свойство для проверки
        {
            get { return weight; }
            set
            {
                if (value < 0)
                    Console.WriteLine("Ошибка: Вес детали не может быть отрицательным");
                weight = value;
            }
        }
            
        public Factory() : base() //конструктор базового класса без параметров
        {
            FactoryName = "Без названия";
            Weight = 0;
        }

        public Factory(string name, int employees, string factoryName, double weight) : base(name, employees) //конструктор с парметрами базового класса 
        {
            FactoryName = factoryName;
            Weight = weight;
        }

        public Factory(Factory factory) : base(factory) //конструктор копирования базового класса 
        {
            FactoryName = FactoryName;
            Weight = factory.Weight;
        }

        public override void Show() //переопределенный метод Show
        {
            base.Show();
            Console.WriteLine($"Название фабрики: {factoryName}");
            Console.WriteLine($"Вес всех деталей: {Weight} кг");
        }

        public override void Init()
        {
            base.Init();
            do
            {
                Console.WriteLine("Введите название фабрики: ");
                FactoryName = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(FactoryName));

            Weight = ReadPosDouble("Введите вес всех деталей: ");
        }

        public override void RandomInit() //переопределнный метод RandomInit
        {
            base.RandomInit();
            FactoryName = "Фабрика_" + rnd.Next(1, 100);
            Weight = rnd.NextDouble() * 300;
        }

        public override bool Equals(object? obj) //переопределенный метод Equals
        {
            if(!base.Equals(obj))
                return false;
            Factory factory = obj as Factory;
            if(factory == null) return false;
            return this.FactoryName == factory.FactoryName && this.Weight == factory.Weight;
        }

        private double ReadPosDouble(string prompt) // Метод для проверки на положительное число
        {
            double result = 0;
            while (true)
            {
                Console.WriteLine(prompt);
                if (double.TryParse(Console.ReadLine(), out result) && result > 0)
                    return result; 
                Console.WriteLine("Ошибка: Введите положительное число");
            }
        }

        public override object ShallowCopy()
        {
            return base.ShallowCopy();
        }

        public override object Clone()
        {
            return new Factory
            {
                Name = this.Name,
                Employees = this.Employees,
                FactoryName = this.FactoryName,
                Weight = this.Weight
            };
        }

        public override string ToString()
        {
            return base.ToString() + $"{FactoryName}, {Weight}";
        }




    }
}
