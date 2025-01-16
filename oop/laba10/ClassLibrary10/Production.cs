namespace ClassLibrary10
{
    public class Production : IInit, IComparable<Production>, ICloneable
    {
        static Random rnd = new Random();
        private string name;

        private int employees;

        public string Name //свойство
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Console.WriteLine("Ошибка: Наименование не может быть  пустым");
                name = value;
            }
        }

        public int Employees //свойство
        {
            get
            {
                return employees;
            }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Ошибка: Количество работников не может быть отрицательным");
                }
                employees = value;
            }
        }

        public Production() //конструктор без парамтров
        {

        }

        public Production(string name, int employees) //конструктор с параметрами
        {
            this.name = name;
            this.employees = employees;
        }

        public Production(Production production) //конструктор копирования
        {
            this.name = production.Name;
            this.employees = production.Employees;
        }

        public void NonVirtualShow() //невритуальный метод show
        {
            Console.WriteLine($"\nНаименование: {Name}, количество сотрудников: {Employees}");
        }

        public virtual void Show() //виртуальный метод show
        {
            Console.WriteLine($"\nНаименование: {Name}, количество сотрудников: {Employees}");
        }

        public virtual void Init() //метод для ввода с клавиатуры
        {
            do
            {
                Console.WriteLine("Введите Наименование: ");
                Name = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(Name));
            
            Employees = ReadPosInt("Введите количество сотрудников: ");
        }

        public virtual void RandomInit() //метод для формирования объектов с помощью ДСЧ
        {
            
            Name = "Производство_" + rnd.Next(1, 100);
            Employees = rnd.Next(30, 1000);
        }

        public override bool Equals(object? obj)
        {
            if(obj == null || obj.GetType() != this.GetType())
                    return false;
            Production production = (Production)obj;
            return this.Name == production.Name && this.Employees == production.Employees;
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


        //public int CompareTo(object obj)
        //{
        //    if (obj is Production other)
        //    {
        //        if (this.Name == other.Name)
        //            return 0; 
        //        if (this.Name.CompareTo(other.Name) > 0)
        //            return 1; 
        //        return -1;  
        //    }

        //    return 0; 
        //}

        public virtual object ShallowCopy()
        {
            return(Production) this.MemberwiseClone();
        }

        public virtual object Clone()
        {
            return new Production
            {
                Name = this.Name,
                Employees = this.Employees
            };
        }

        int IComparable<Production>.CompareTo(Production? other)
        {
            return String.Compare(this.Name, other?.Name);
        }

        public override string ToString()
        {
            return $"{Name}, {Employees}";
        }
    }

}
