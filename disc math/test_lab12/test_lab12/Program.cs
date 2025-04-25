using System.Collections;

public class HashTable<TKey, TValue> : IDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>
{
    private class Node
    {
        public TKey Key;
        public TValue Value;
        public Node Next;

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Next = null;
        }
    }

    private Node[] table;
    private int count;
    public int Count
    {
        get
        {
            return count;
        }
    }
    public bool IsReadOnly
    {
        get
        {
            return false;
        }
    }
    public HashTable() : this(10) { }

    public HashTable(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("Размер не может быть меньше 0");

        table = new Node[capacity];
    }
    public TValue this[TKey key]
    {
        get
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (TryGetValue(key, out TValue value))
                return value;
            else
                throw new KeyNotFoundException($"Ключ'{key}' не найден");
        }
        set => Add(key, value);
    }

    public ICollection<TKey> Keys
    {
        get
        {
            var keys = new List<TKey>();
            foreach (var node in table)
            {
                var current = node;
                while (current != null)
                {
                    keys.Add(current.Key);
                    current = current.Next;
                }
            }
            return keys;
        }
    }

    public ICollection<TValue> Values
    {
        get
        {
            var values = new List<TValue>();
            foreach (var node in table)
            {
                var current = node;
                while (current != null)
                {
                    values.Add(current.Value);
                    current = current.Next;
                }
            }
            return values;
        }
    }
    public void Add(TKey key, TValue value)
    {
        Add(new KeyValuePair<TKey, TValue>(key, value));
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        int index = Math.Abs(item.Key.GetHashCode()) % table.Length;
        Node current = table[index];

        while (current != null)
        {
            if (current.Key.Equals(item.Key))
                throw new ArgumentException("Ключ уже существует");
            current = current.Next;
        }

        Node newNode = new Node(item.Key, item.Value)
        {
            Next = table[index]
        };
        table[index] = newNode;
        count++;
    }
    public bool ContainsKey(TKey key)
    {
        return TryGetValue(key, out _);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        int index = Math.Abs(key.GetHashCode()) % table.Length;
        Node current = table[index];

        while (current != null)
        {
            if (Equals(current.Key, key))
            {
                value = current.Value;
                return true;
            }
            current = current.Next;
        }

        value = default;
        return false;
    }
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        if (!Contains(item))
            return false;
        return Remove(item.Key);
    }
    public bool Remove(TKey key)
    {
        int index = Math.Abs(key.GetHashCode()) % table.Length;
        Node current = table[index];
        Node previous = null;

        while (current != null)
        {
            if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
            {
                if (previous == null)
                    table[index] = current.Next;
                else
                    previous.Next = current.Next;
                count--;
                return true;
            }
            previous = current;
            current = current.Next;
        }
        return false;
    }
    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return


TryGetValue(item.Key, out TValue value) && value.Equals(item.Value);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));
        if (arrayIndex < 0 || arrayIndex >= array.Length)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        if (array.Length - arrayIndex < count)
            throw new ArgumentException("Массив слишком маленький.");

        foreach (var pair in this)
        {
            array[arrayIndex] = pair;
            arrayIndex++;
        }
    }
    public void Zm()
    {
        for (int i = 0; i < table.Length; i++)
        {
            Node current = table[i];

            while (current != null)
            {
                if (current.Value is Engine engine)
                {
                    engine.Size = new ForCopy(999);
                }
                current = current.Next;
            }
        }
    }
    public void Clear()
    {
        table = new Node[table.Length];
        count = 0;
    }
    public void Print()
    {
        if (count == 0)
        {
            Console.WriteLine("Пусто");
        }
        else
        {
            for (int i = 0; i < table.Length; i++)
            {
                Node current = table[i];
                while (current != null)
                {
                    Console.WriteLine($"{current.Key} :{current.Value} ");
                    current = current.Next;
                }
            }
        }
    }
    public void PrintNum()
    {
        foreach (var pair in this)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }
    public void FindElement(string str)
    {
        bool flag = false;

        foreach (var init in this)
        {
            if (init.Value is Engine engine && engine.Model == str)
            {
                Console.WriteLine($"{init.Key}: {init.Value}");
                flag = true;
            }
        }

        if (!flag)
        {
            Console.WriteLine("Модель не найдена");
        }
    }
    public HashTable<TKey, TValue> DeepCopy()
    {
        HashTable<TKey, TValue> clone = new HashTable<TKey, TValue>(table.Length);

        foreach (var item in this)
        {
            if (item.Value is ICloneable icloneableValue && item.Key is ICEngine icloneableKey)
            {
                clone.Add((TKey)icloneableKey.Clone(), (TValue)icloneableValue.Clone());
            }
            else
            {
                clone.Add(item.Key, item.Value);
            }
        }

        return clone;
    }

    public HashTable<TKey, TValue> ShallowCopy()
    {
        HashTable<TKey, TValue> copy = new HashTable<TKey, TValue>(table.Length);

        copy.table = this.table;
        copy.count = this.count;

        return copy;
    }
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        for (int i = 0; i < table.Length; i++)
        {
            Node current = table[i];
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