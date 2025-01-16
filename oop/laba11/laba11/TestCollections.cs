using ClassLibrary10;
using System.Collections.Generic;

namespace Laba11
{
    public class TestCollections
    {
        public Stack<Production> productionStack = new Stack<Production>();
        public Stack<string> stringStack = new Stack<string>();
        public SortedDictionary<Production, Factory> productionDictionary = new SortedDictionary<Production, Factory>();
        public SortedDictionary<string, Factory> stringDictionary = new SortedDictionary<string, Factory>();

        public void RandomInit(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                string name = $"Production_{i + 1}";
                int employees = (i + 1) * 10;
                string factoryName = $"Factory_{i + 1}";
                double weight = (i + 1) * 100.5;

                var factory = new Factory(name, employees, factoryName, weight);
                var production = new Production(factory.Name, factory.Employees);

                productionStack.Push(production);
                stringStack.Push(production.ToString());
                productionDictionary.Add(production, factory);
                stringDictionary.Add(production.ToString(), factory);
            }
        }
    }
}
