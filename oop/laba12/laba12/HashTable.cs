using System;
using System.Collections;
using System.Collections.Generic;
public class ListPoint<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
    public ListPoint<TKey, TValue> Next { get; set; }

    public ListPoint(TKey key, TValue value)
    {
        Key = key;
        Value = value;
        Next = null;
    }

    public override string ToString()
    {
        return Key + ":" + Value.ToString();
    }
}

public class MyCollection<TKey, TValue> : IDictionary<TKey, TValue>
{
    private ListPoint<TKey, TValue>[] table;
    private int capacity;
    private int count;
    private const int defaultCapacity = 10;

    public MyCollection()
    {
        this.table = new ListPoint<TKey, TValue>[defaultCapacity];
        this.count = 0;
    }

    public MyCollection(int capacity)
    {
        this.capacity = capacity;
        this.table = new ListPoint<TKey, TValue>[capacity];
        this.count = 0;
    }

    public MyCollection(MyCollection<TKey, TValue> other)
    {
        this.capacity = other.capacity;
        this.table = new ListPoint<TKey, TValue>[capacity];
        this.count = other.count;

        for (int i = 0; i < capacity; i++)
        {
            if (other.table[i] != null)
            {
                ListPoint<TKey, TValue> current = other.table[i];
                while (current != null)
                {
                    Add(current.Key, current.Value);
                    current = current.Next;
                }
            }
        }
    }

    private int GetIndex(TKey key)
    {
        return Math.Abs(key.GetHashCode()) % capacity;
    }

    public void Add(TKey key, TValue value)
    {
        if (ContainsKey(key)) return;
        int index = GetIndex(key);
        ListPoint<TKey, TValue> newNode = new ListPoint<TKey, TValue>(key, value);

        if (table[index] == null)
            table[index] = newNode;
        else
        {
            ListPoint<TKey, TValue> cur = table[index];
            while (cur.Next != null) cur = cur.Next;
            cur.Next = newNode;
        }
        count++;
    }

    public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

    public bool Remove(TKey key)
    {
        int index = GetIndex(key);
        ListPoint<TKey, TValue> cur = table[index];
        ListPoint<TKey, TValue> prev = null;

        while (cur != null)
        {
            if (cur.Key.Equals(key))
            {
                if (prev == null) table[index] = cur.Next;
                else prev.Next = cur.Next;
                count--;
                return true;
            }
            prev = cur;
            cur = cur.Next;
        }
        return false;
    }

    public bool ContainsKey(TKey key)
    {
        int index = GetIndex(key);
        ListPoint<TKey, TValue> cur = table[index];
        while (cur != null)
        {
            if (cur.Key.Equals(key)) return true;
            cur = cur.Next;
        }
        return false;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        int index = GetIndex(key);
        ListPoint<TKey, TValue> cur = table[index];
        while (cur != null)
        {
            if (cur.Key.Equals(key))
            {
                value = cur.Value;
                return true;
            }
            cur = cur.Next;
        }
        value = default;
        return false;
    }

    public TValue this[TKey key]
    {
        get
        {
            if (TryGetValue(key, out TValue value)) return value;
            throw new KeyNotFoundException();
        }
        set => Add(key, value);
    }

    public ICollection<TKey> Keys
    {
        get
        {
            List<TKey> keys = new List<TKey>();
            foreach (var pair in this)
                keys.Add(pair.Key);
            return keys;
        }
    }

    public ICollection<TValue> Values
    {
        get
        {
            List<TValue> values = new List<TValue>();
            foreach (var pair in this)
                values.Add(pair.Value);
            return values;
        }
    }

    public void Clear()
    {
        for (int i = 0; i < capacity; i++)
            table[i] = null;
        count = 0;
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        foreach (var pair in this)
            array[arrayIndex++] = pair;
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return TryGetValue(item.Key, out TValue value) && EqualityComparer<TValue>.Default.Equals(value, item.Value);
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        return Remove(item.Key);
    }

    public bool IsReadOnly => false;
    public int Count => count;

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        foreach (var bucket in table)
        {
            ListPoint<TKey, TValue> cur = bucket;
            while (cur != null)
            {
                yield return new KeyValuePair<TKey, TValue>(cur.Key, cur.Value);
                cur = cur.Next;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
