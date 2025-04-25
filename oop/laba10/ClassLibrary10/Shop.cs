using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary10
{
    public class Shop : Production, IInit
    {
        static Random rnd = new Random();

        private string shopName;

        public string ShopName
        {
            get { return shopName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Console.WriteLine("Ошибка: Название цеха не может быть пустым");
                shopName = value;
            }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Console.WriteLine("Ошибка: Тип цеха не может быть пустым");
                type = value;
            }
        }

        public Shop() : base()
        {
            ShopName = "Без названия";
            Type = "Без типа";
        }

        public Shop(string name, int employees, string factoryName, double weight, string shopName, string type) : base(name, employees)
        {
            ShopName = shopName;
            Type = type;
        }

        public Shop(Shop shop) : base(shop)
        {
            ShopName = shop.ShopName;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Название цеха: {shopName}");
            Console.WriteLine($"Тип цеха: {type}");
        }

        public override void Init()
        {
            base.Init();
            do
            {
                Console.WriteLine("Введите название цеха: ");
                ShopName = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(ShopName));

            do
            {
                Console.WriteLine("Введите тип цеха: ");
                Type = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(Type));
        }

        public override void RandomInit()
        {
            base.RandomInit(); 
            ShopName = "Цех_" + rnd.Next(1, 100);
            string[] types = { "основной", "вспомогательный", "обслуживающий", "подсобный", "побочный", "экспериментальный" };
            Type = types[rnd.Next(types.Length)];
        }

        public override bool Equals(object? obj)
        {
            if(!base.Equals(obj))
                return false;
            Shop shop = obj as Shop;
            if(shop == null) return false;
            return this.ShopName == shop.ShopName && this.Type == shop.Type;
        }

        public override string ToString()
        {
            return base.ToString() + $", {shopName}, {type}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Name, Employees, shopName, ShopName, type, Type);
        }
    }
}
