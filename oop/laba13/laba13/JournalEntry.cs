using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba13
{
    public class JournalEntry
    {
        public string CollectionName { get; set; }
        public string TypeChange { get; set; }
        public object ChangeObj { get; set; }

        public JournalEntry(string collectionName, string typeChange, object changeObj)
        {
            CollectionName = collectionName;
            TypeChange = typeChange;
            ChangeObj = changeObj;
        }

        public override string ToString()
        {
            return $"Коллекция: {CollectionName}, Тип изменений: {TypeChange}, Объект: {ChangeObj}";
        }
    }
}
