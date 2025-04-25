using System;
using System.Collections.Generic;

namespace laba13
{
    class MyNewCollection : HashTable<int, object>
    {
        public string CollectionName { get; set; }

        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;

        public MyNewCollection(string name) : base()
        {
            CollectionName = name;
        }

        public void AddDefault()
        {
            int newKey = this.Count;
            for (int i = 0; i < 3; i++)
            {
                int key = newKey + i;
                object value = $"Default_{key}";
                base.Add(key, value);
                OnCollectionCountChanged("добавление", value);
            }
        }

        public void Add(object[] elements)
        {
            int newKey = this.Count;
            for (int i = 0; i < elements.Length; i++)
            {
                object value = elements[i];
                base.Add(newKey, value);
                OnCollectionCountChanged("добавление", value);
                newKey++;
            }
        }

        public bool Remove(int index)
        {
            if (index < 0 || index >= this.Count)
                return false;

            int currentIndex = 0;
            int removeKey = 0;
            object removedValue = null;

            foreach (KeyValuePair<int, object> pair in this)
            {
                if (currentIndex == index)
                {
                    removeKey = pair.Key;
                    removedValue = pair.Value;
                    break;
                }
                currentIndex++;
            }

            bool result = base.Remove(removeKey);
            if (result)
            {
                OnCollectionCountChanged("удаление", removedValue);
            }
            return result;
        }

        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    throw new IndexOutOfRangeException();

                int currentIndex = 0;
                foreach (KeyValuePair<int, object> pair in this)
                {
                    if (currentIndex == index)
                        return pair.Value;
                    currentIndex++;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index < 0 || index >= this.Count)
                    throw new IndexOutOfRangeException();

                int currentIndex = 0;
                int updateKey = 0;
                foreach (KeyValuePair<int, object> pair in this)
                {
                    if (currentIndex == index)
                    {
                        updateKey = pair.Key;
                        break;
                    }
                    currentIndex++;
                }
                base[updateKey] = value;
                OnCollectionReferenceChanged("изменение", value);
            }
        }


        public void OnCollectionCountChanged(string typeChange, object element)
        {
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs(CollectionName, typeChange, element));
        }

        public void OnCollectionReferenceChanged(string typeChange, object element)
        {
            CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs(CollectionName, typeChange, element));
        }
    }
}
