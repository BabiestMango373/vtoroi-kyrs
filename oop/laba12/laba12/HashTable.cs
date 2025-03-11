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
    private ListPoint<TKey, TValue>[] buckets;
    private int count;
    private int capacity;

    public HashTable()
    {
        capacity = 1;
        buckets = new ListPoint<TKey, TValue>[capacity];
    }

    public HashTable(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be positive");
        this.capacity = capacity;
        buckets = new ListPoint<TKey, TValue>[capacity];
    }

    public HashTable(IDictionary<TKey, TValue> dictionary)
        : this(dictionary.Count)
    {
        foreach (var pair in dictionary)
        {
            Add(pair.Key, pair.Value);
        }
    }

    public int Count => count;

    public bool IsReadOnly => false;

    public ICollection<TKey> Keys => new List<TKey>(this.Select(p => p.Key));

    public ICollection<TValue> Values => new List<TValue>(this.Select(p => p.Value));

    public TValue this[TKey key]
    {
        get
        {
            if (TryGetValue(key, out TValue value))
                return value;
            throw new KeyNotFoundException();
        }
        set => AddOrUpdate(key, value);
    }

    public void Add(TKey key, TValue value)
    {
        if (ContainsKey(key))
            throw new ArgumentException("An item with the same key has already been added");

        AddOrUpdate(key, value);
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
            throw new ArgumentNullException(nameof(array));

        if (arrayIndex < 0 || arrayIndex >= array.Length)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));

        int index = 0;
        foreach (var pair in this)
        {
            if (index + arrayIndex >= array.Length)
                break;

            array[index + arrayIndex] = pair;
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
        Array.Fill(buckets, null);
        count = 0;
    }

    public bool ContainsKey(TKey key)
    {
        int index = GetIndex(key);
        var current = buckets[index];
        while (current != null)
        {
            if (current.Key.Equals(key))
                return true;
            current = current.Next;
        }
        return false;
    }

    public void AddOrUpdate(TKey key, TValue value)
    {
        int index = GetIndex(key);
        var current = buckets[index];
        ListPoint<TKey, TValue>? previous = null;

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

        var newNode = new ListPoint<TKey, TValue>(key, value);
        if (previous == null)
        {
            buckets[index] = newNode;
        }
        else
        {
            previous.Next = newNode;
        }
        count++;
    }

    public bool Remove(TKey key)
    {
        int index = GetIndex(key);
        var current = buckets[index];
        ListPoint<TKey, TValue>? previous = null;

        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                if (previous == null)
                {
                    buckets[index] = current.Next;
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

    public bool TryGetValue(TKey key, out TValue value)
    {
        int index = GetIndex(key);
        var current = buckets[index];
        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                value = current.Value;
                return true;
            }
            current = current.Next;
        }
        value = default!;
        return false;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return new Enumerator(this);
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
            throw new ArgumentNullException(nameof(array));

        if (index < 0 || index >= array.Length)
            throw new ArgumentOutOfRangeException(nameof(index));

        var list = new List<object>();
        foreach (var item in this)
        {
            list.Add(item);
        }

        list.CopyTo((object[])array, index);
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
            currentPair = new KeyValuePair<TKey, TValue>(default!, default!);
        }

        public KeyValuePair<TKey, TValue> Current => currentPair;

        object IEnumerator.Current => Current;

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

                currentNode = hashTable.buckets[index];
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