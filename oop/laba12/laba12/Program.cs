using System;
using System.Collections;
using System.Collections.Generic;

public class MyCollection<K, V> : IEnumerable<V>
{
    private class HashNode
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public HashNode Next { get; set; }

        public HashNode(K key, V value)
        {
            Key = key;
            Value = value;
            Next = null;
        }
    }

    private List<HashNode>[] table;
    private int capacity;
    private int count;

    public MyCollection() : this(10) { }

    public MyCollection(int capacity)
    {
        this.capacity = capacity;
        table = new List<HashNode>[capacity];
        for (int i = 0; i < capacity; i++)
            table[i] = new List<HashNode>();
        count = 0;
    }

    public MyCollection(MyCollection<K, V> other)
    {
        capacity = other.capacity;
        count = other.count;
        table = new List<HashNode>[capacity];
        for (int i = 0; i < capacity; i++)
        {
            table[i] = new List<HashNode>();
            foreach (var node in other.table[i])
            {
                table[i].Add(new HashNode(node.Key, node.Value));
            }
        }
    }

    private int GetIndex(K key)
    {
        return Math.Abs(key.GetHashCode()) % capacity;
    }

    public void Add(K key, V value)
    {
        int index = GetIndex(key);
        foreach (var node in table[index])
        {
            if (node.Key.Equals(key))
            {
                node.Value = value;
                return;
            }
        }
        table[index].Add(new HashNode(key, value));
        count++;
    }

    public bool Remove(K key)
    {
        int index = GetIndex(key);
        for (int i = 0; i < table[index].Count; i++)
        {
            if (table[index][i].Key.Equals(key))
            {
                table[index].RemoveAt(i);
                count--;
                return true;
            }
        }
        return false;
    }

    public V Find(K key)
    {
        int index = GetIndex(key);
        foreach (var node in table[index])
        {
            if (node.Key.Equals(key))
                return node.Value;
        }
        throw new KeyNotFoundException("Элемент не найден");
    }

    public void Clear()
    {
        for (int i = 0; i < capacity; i++)
            table[i].Clear();
        count = 0;
    }

    public MyCollection<K, V> DeepClone()
    {
        return new MyCollection<K, V>(this);
    }

    public IEnumerator<V> GetEnumerator()
    {
        foreach (var bucket in table)
        {
            foreach (var node in bucket)
                yield return node.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
