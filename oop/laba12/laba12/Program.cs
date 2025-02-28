using System;
using System.Collections;
using System.Collections.Generic;

public class LPoint<K, V>
{
    public K Key { get; set; }
    public V Value { get; set; }
    public LPoint<K, V> Next { get; set; }

    public LPoint(K key, V value)
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

public class MyCollection<K, V> : IEnumerable<V>, ICloneable
{
    private LPoint<K, V>[] table;
    private int capacity;
    private int count;

    public MyCollection()
    {
        this.capacity = 10;
        this.table = new LPoint<K, V>[capacity];
        this.count = 0;
    }

    public MyCollection(int capacity)
    {
        this.capacity = capacity;
        this.table = new LPoint<K, V>[capacity];
        this.count = 0;
    }

    public MyCollection(MyCollection<K, V> c)
    {
        this.capacity = c.capacity;
        this.count = c.count;
        this.table = new LPoint<K, V>[capacity];
        for (int i = 0; i < capacity; i++)
        {
            if (c.table[i] != null)
            {
                LPoint<K, V> current = c.table[i];
                while (current != null)
                {
                    Add(current.Key, current.Value);
                    current = current.Next;
                }
            }
        }
    }

    private int GetIndex(K key)
    {
        return Math.Abs(key.GetHashCode()) % capacity;
    }

    public void Add(K key, V value)
    {
        if (key == null || value == null) return;
        int index = GetIndex(key);
        LPoint<K, V> newNode = new LPoint<K, V>(key, value);

        if (table[index] == null)
            table[index] = newNode;
        else
        {
            LPoint<K, V> cur = table[index];
            while (cur.Next != null)
            {
                if (cur.Key.Equals(key)) return;
                cur = cur.Next;
            }
            cur.Next = newNode;
        }
        count++;
    }

    public void Add(Dictionary<K, V> items)
    {
        foreach (var item in items)
        {
            Add(item.Key, item.Value);
        }
    }

    public void Remove(K key)
    {
        int index = GetIndex(key);
        LPoint<K, V> cur = table[index];
        LPoint<K, V> prev = null;

        while (cur != null)
        {
            if (cur.Key.Equals(key))
            {
                if (prev == null) table[index] = cur.Next;
                else prev.Next = cur.Next;
                count--;
                return;
            }
            prev = cur;
            cur = cur.Next;
        }
    }

    public void Remove(List<K> keys)
    {
        foreach (var key in keys)
        {
            Remove(key);
        }
    }

    public bool Find(K key)
    {
        int index = GetIndex(key);
        LPoint<K, V> cur = table[index];
        while (cur != null)
        {
            if (cur.Key.Equals(key)) return true;
            cur = cur.Next;
        }
        return false;
    }

    public void Clear()
    {
        for (int i = 0; i < capacity; i++)
            table[i] = null;
        count = 0;
    }

    public object Clone()
    {
        return new MyCollection<K, V>(this);
    }

    public MyCollection<K, V> ShallowCopy()
    {
        return (MyCollection<K, V>)this.MemberwiseClone();
    }

    public IEnumerator<V> GetEnumerator()
    {
        foreach (var bucket in table)
        {
            LPoint<K, V> current = bucket;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
