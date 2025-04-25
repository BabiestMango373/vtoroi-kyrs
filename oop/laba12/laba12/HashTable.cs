using System;
using System.Collections;
using System.Collections.Generic;

public class ListPoint<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
    public ListPoint<TKey, TValue>? Next { get; set; }

    public ListPoint(TKey key, TValue value)
    {
        Key = key;
        Value = value;
        Next = null;
    }
}

public class HashTable<TKey, TValue> : IDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>, ICloneable
{
    private ListPoint<TKey, TValue>[] table;
    private int count;
    private int capacity;

    public HashTable()
    {
        capacity = 1;
        table = new ListPoint<TKey, TValue>[capacity];
    }

    public HashTable(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("ёмкость должна быть положительная");
        this.capacity = capacity;
        table = new ListPoint<TKey, TValue>[capacity];
    }

<<<<<<< HEAD
    public HashTable(HashTable<TKey, TValue> c)
    {
        if (c == null)
            throw new ArgumentNullException("");

        this.capacity = c.capacity;
        this.table = new ListPoint<TKey, TValue>[capacity];
        this.count = 0;

        foreach (var pair in c)
        {
            this.Add(pair.Key, pair.Value);
=======
    public HashTable(IDictionary<TKey, TValue> dictionary)
        : this(dictionary.Count)
    {
        foreach (KeyValuePair<TKey, TValue> pair in dictionary)
        {
            Add(pair.Key, pair.Value);
>>>>>>> 1b853a8f3cf1e78a08029632c43956805cdf982a
        }
    }

    public int Count { get { return count; } }

    public bool IsReadOnly { get { return false; } }

    public ICollection<TKey> Keys
    {
        get
        {
            List<TKey> keys = new List<TKey>();
            foreach (KeyValuePair<TKey, TValue> kvp in this)
            {
                keys.Add(kvp.Key);
            }
            return keys;
        }
    }

    public ICollection<TValue> Values
    {
        get
        {
            List<TValue> values = new List<TValue>();
            foreach (KeyValuePair<TKey, TValue> kvp in this)
            {
                values.Add(kvp.Value);
            }
            return values;
        }
    }

    public TValue this[TKey key]
    {
        get
        {
            if (TryGetValue(key, out TValue value))
                return value;
            throw new KeyNotFoundException();
        }
        set { Add(key, value); }
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
    {
        Add(item.Key, item.Value);
    }

<<<<<<< HEAD
=======
    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return ContainsKey(item.Key) && this[item.Key].Equals(item.Value);
    }

>>>>>>> 1b853a8f3cf1e78a08029632c43956805cdf982a
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException("массив пустой");

        if (arrayIndex < 0 || arrayIndex >= array.Length)
            throw new ArgumentOutOfRangeException("индекс массива некорректный");

        int index = arrayIndex;
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            if (index >= array.Length)
                break;
            array[index] = pair;
            index++;
        }
    }

<<<<<<< HEAD
    public void Clear()
    {
        for (int i = 0; i < capacity; i++)
        {
            table[i] = null;
        }
        count = 0;
    }

    // Метод добавления
    public void Add(TKey key, TValue value)
    {
        int index = GetIndex(key);
        ListPoint<TKey, TValue> newNode = new ListPoint<TKey, TValue>(key, value);

        if (table[index] == null)
        {
            table[index] = newNode;
        }
        else
        {
            ListPoint<TKey, TValue> current = table[index];
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
        count++;
    }



    public void Add(params KeyValuePair<TKey, TValue>[] items)
    {
        foreach (KeyValuePair<TKey, TValue> item in items)
        {
            Add(item.Key, item.Value);
        }
    }

    // Метод удаления одного элемента.
=======
>>>>>>> 1b853a8f3cf1e78a08029632c43956805cdf982a
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        if (Contains(item))
        {
            Remove(item.Key);
            return true;
        }
        return false;
    }

<<<<<<< HEAD
=======
    public void Clear()
    {
        for (int i = 0; i < capacity; i++)
        {
            table[i] = null;
        }
        count = 0;
    }

    public bool ContainsKey(TKey key)
    {
        int index = GetIndex(key);
        ListPoint<TKey, TValue> current = table[index];
        while (current != null)
        {
            if (current.Key.Equals(key))
                return true;
            current = current.Next;
        }
        return false;
    }

    // Метод добавления одного элемента.
    public void Add(TKey key, TValue value)
    {
        int index = GetIndex(key);
        ListPoint<TKey, TValue> current = table[index];
        ListPoint<TKey, TValue> previous = null;

        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                current.Value = value;
                return;
            }
            previous = current;
            current = current.Next;
        }

        ListPoint<TKey, TValue> newNode = new ListPoint<TKey, TValue>(key, value);
        if (previous == null)
        {
            table[index] = newNode;
        }
        else
        {
            previous.Next = newNode;
        }
        count++;
    }

    // Метод Add для добавления одного или нескольких элементов.
    public void Add(params KeyValuePair<TKey, TValue>[] items)
    {
        foreach (KeyValuePair<TKey, TValue> item in items)
        {
            Add(item.Key, item.Value);
        }
    }

    // Метод удаления одного элемента.
>>>>>>> 1b853a8f3cf1e78a08029632c43956805cdf982a
    public bool Remove(TKey key)
    {
        int index = GetIndex(key);
        ListPoint<TKey, TValue> current = table[index];
        ListPoint<TKey, TValue> previous = null;

        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                if (previous == null)
                {
                    table[index] = current.Next;
                }
                else
                {
                    previous.Next = current.Next;
                }
                count--;
                return true;
            }
            previous = current;
            current = current.Next;
        }
        return false;
    }

<<<<<<< HEAD
=======
    // Метод Remove для удаления одного или нескольких элементов.
>>>>>>> 1b853a8f3cf1e78a08029632c43956805cdf982a
    public void Remove(params TKey[] keys)
    {
        foreach (TKey key in keys)
        {
            Remove(key);
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        int index = GetIndex(key);
        ListPoint<TKey, TValue> current = table[index];
        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                value = current.Value;
                return true;
            }
            current = current.Next;
        }
        value = default(TValue);
        return false;
    }

<<<<<<< HEAD
    private int GetIndex(TKey key)
    {
        return Math.Abs(key.GetHashCode() % capacity);
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return ContainsKey(item.Key) && this[item.Key].Equals(item.Value);
    }

    //Поиск элемента поключу
    public bool ContainsKey(TKey key)
    {
        int index = GetIndex(key);
        ListPoint<TKey, TValue> current = table[index];
        while (current != null)
        {
            if (current.Key.Equals(key))
                return true;
            current = current.Next;
        }
        return false;
    }

    // Поиск элемента по значению
    public bool ContainsValue(TValue value)
    {
        foreach (KeyValuePair<TKey, TValue> item in this)
        {
            if (object.Equals(item.Value, value))
                return true;
        }
        return false;
    }

    // Поверхностное копирование
    public HashTable<TKey, TValue> ShallowCopy()
    {
        return (HashTable<TKey, TValue>)this.MemberwiseClone();
    }

    // Глубокое копирование
    public HashTable<TKey, TValue> DeepCopy()
    {
        HashTable<TKey, TValue> clone = new HashTable<TKey, TValue>(capacity);
        foreach (var pair in this)
        {
            TKey clonedKey = (TKey)((ICloneable)pair.Key).Clone();
            TValue clonedValue = (TValue)((ICloneable)pair.Value).Clone();
            clone.Add(clonedKey, clonedValue);
        }
        return clone;
    }

    public object Clone()
    {
        return DeepCopy();
    }

=======
>>>>>>> 1b853a8f3cf1e78a08029632c43956805cdf982a
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        for (int i = 0; i < capacity; i++)
        {
            ListPoint<TKey, TValue> current = table[i];
            while (current != null)
            {
                yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
                current = current.Next;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
<<<<<<< HEAD
=======

    private int GetIndex(TKey key)
    {
        return Math.Abs(key.GetHashCode() % capacity);
    }

    public void CopyTo(Array array, int index)
    {
        if (array == null)
            throw new ArgumentNullException("массив пустой");

        if (index < 0 || index >= array.Length)
            throw new ArgumentOutOfRangeException("индекс некорректный");

        List<object> list = new List<object>();
        foreach (KeyValuePair<TKey, TValue> item in this)
        {
            list.Add(item);
        }
        list.CopyTo((object[])array, index);
    }

    // Поиск элемента по значению.
    public bool ContainsValue(TValue value)
    {
        foreach (KeyValuePair<TKey, TValue> item in this)
        {
            if (object.Equals(item.Value, value))
                return true;
        }
        return false;
    }

    // Поверхностное копирование
    public HashTable<TKey, TValue> ShallowCopy()
    {
        HashTable<TKey, TValue> copy = new HashTable<TKey, TValue>(capacity);
        copy.table = this.table;
        copy.count = this.count;
        return copy;
    }

    // Глубокое копирование
    public HashTable<TKey, TValue> DeepCopy()
    {
        HashTable<TKey, TValue> clone = new HashTable<TKey, TValue>(capacity);
        foreach (var pair in this)
        {
            // Клонируем ключ и значение, учитывая их реальный тип
            TKey clonedKey = (TKey)((ICloneable)pair.Key).Clone();
            TValue clonedValue = (TValue)((ICloneable)pair.Value).Clone();
            clone.Add(clonedKey, clonedValue);
        }
        return clone;
    }

    public object Clone()
    {
        return DeepCopy();
    }

    public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private HashTable<TKey, TValue> hashTable;
        private int index;
        private ListPoint<TKey, TValue>? currentNode;
        private KeyValuePair<TKey, TValue> currentPair;

        public Enumerator(HashTable<TKey, TValue> hashTable)
        {
            this.hashTable = hashTable;
            index = -1;
            currentNode = null;
            currentPair = new KeyValuePair<TKey, TValue>(default(TKey), default(TValue));
        }

        public KeyValuePair<TKey, TValue> Current
        {
            get { return currentPair; }
        }

        object IEnumerator.Current
        {
            get { return currentPair; }
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            while (true)
            {
                if (currentNode != null && currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                    currentPair = new KeyValuePair<TKey, TValue>(currentNode.Key, currentNode.Value);
                    return true;
                }
                index++;
                if (index >= hashTable.capacity)
                    return false;
                currentNode = hashTable.table[index];
                if (currentNode != null)
                {
                    currentPair = new KeyValuePair<TKey, TValue>(currentNode.Key, currentNode.Value);
                    return true;
                }
            }
        }

        public void Reset()
        {
            index = -1;
            currentNode = null;
        }
    }
>>>>>>> 1b853a8f3cf1e78a08029632c43956805cdf982a
}
