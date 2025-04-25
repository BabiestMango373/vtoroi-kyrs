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
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        if (Contains(item))
        {
            Remove(item.Key);
            return true;
        }
        return false;
    }

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
}
