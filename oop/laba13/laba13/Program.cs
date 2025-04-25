using System;
using ClassLibrary10;
using laba13;

namespace laba_13
{
    class Program
    {
        static void Main(string[] args)
        {
            MyNewCollection collection1 = new MyNewCollection("Collection1");
            MyNewCollection collection2 = new MyNewCollection("Collection2");

            Journal journal1 = new ();
            Journal journal2 = new ();

            //Подписки
            collection1.CollectionCountChanged += journal1.CollectionCountChange;
            collection1.CollectionReferenceChanged += journal1.CollectionReferenceChange;

            collection1.CollectionReferenceChanged += journal2.CollectionReferenceChange;
            collection2.CollectionReferenceChanged += journal2.CollectionReferenceChange;


            Production prod = new Production("Производство_1", 235);
            Factory fact = new Factory("Производство_2", 123, "Фабрика_1", 423.546);
            Shop shop = new Shop("Производство_2", 78, "Фабрика_2", 123.45, "Цех_1", "основной");
            Workshop workshop = new Workshop("Производство_4", 234, "Фабрика_3", 657.9786, "Цех_2", "вспомогательный", "Мастерская_1", 70);

            collection1.Add(new object[] { prod, fact });
            collection2.Add(new object[] { shop, workshop });

            collection1.Remove(1);
            Production newProd = new Production("Новое_Производство_1", 342);
            Factory newFactory = new Factory("Новое_Производство_2", 988, "Новая_Фабрика", 6543.2114);
            collection1[0] = newFactory;
            collection2[0] = newProd;

            Console.WriteLine($"Журнал 1:\n{journal1}");
            Console.WriteLine($"Журнал 2:\n{journal2}");

        }
    }
}