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

public class HashTable<TKey, TValue> : IDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>
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
            throw new ArgumentException("Ёмкость должна быть положительная");
        this.capacity = capacity;
        table = new ListPoint<TKey, TValue>[capacity];
    }

    public HashTable(IDictionary<TKey, TValue> dictionary)
        : this(dictionary.Count)
    {
        foreach (KeyValuePair<TKey, TValue> pair in dictionary)
        {
            Add(pair.Key, pair.Value);
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

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return ContainsKey(item.Key) && this[item.Key].Equals(item.Value);
    }

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

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        if (Contains(item))
        {
            Remove(item.Key);
            return true;
        }
        return false;
    }

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

    // Перегруженный метод Add для добавления одного или нескольких элементов.
    public void Add(params KeyValuePair<TKey, TValue>[] items)
    {
        foreach (KeyValuePair<TKey, TValue> item in items)
        {
            Add(item.Key, item.Value);
        }
    }

    // Метод удаления одного элемента.
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

    // Перегруженный метод Remove для удаления одного или нескольких элементов.
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

    // Поверхностное копирование – создает новую коллекцию с копированием структуры, но без клонирования объектов.
    public HashTable<TKey, TValue> ShallowCopy()
    {
        return new HashTable<TKey, TValue>(this);
    }

    // Глубокое копирование – создает новую коллекцию, клонируя ключи и значения
    public HashTable<TKey, TValue> DeepCopy()
    {
        HashTable<TKey, TValue> clone = new HashTable<TKey, TValue>(capacity);
        foreach (KeyValuePair<TKey, TValue> item in this)
        {
            // Предполагается, что TKey и TValue реализуют ICloneable
            TKey clonedKey = (TKey)((ICloneable)item.Key).Clone();
            TValue clonedValue = (TValue)((ICloneable)item.Value).Clone();
            clone.Add(clonedKey, clonedValue);
        }
        return clone;
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
}
