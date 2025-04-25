using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary10;

namespace laba14
{
    public static class HashTableExpansion
    {
        public static IEnumerable<KeyValuePair<TKey, TValue>> Where<TKey, TValue>(
            this HashTable<TKey, TValue> table, Func<KeyValuePair<TKey, TValue>, bool> predicate)
            where TValue : Production
        {
            foreach (var item in table)
                if (predicate(item))
                    yield return item;
        }


        public static int Count<TKey, TValue>(
            this HashTable<TKey, TValue> table, Func<KeyValuePair<TKey, TValue>, bool> predicate)
            where TValue : Production
        {
            int count = 0;
            foreach (var item in table)
                if (predicate(item))
                    count++;
            return count;
        }


        public static int Max<TKey, TValue>(
                this HashTable<TKey, TValue> table, Func<KeyValuePair<TKey, TValue>, int> selector)
                where TValue : Production
        {
            int max = int.MinValue;
            foreach (var item in table)
            {
                int value = selector(item);
                if (value > max) 
                    max = value;
            }
            return max;
        }

        public static int Min<TKey, TValue>(
            this HashTable<TKey, TValue> table, Func<KeyValuePair<TKey, TValue>, int> selector)
            where TValue : Production
        {
            int min = int.MaxValue;
            foreach (var item in table)
            {
                int value = selector(item);
                if (value < min) 
                    min = value;
            }
            return min;
        }
    }
}
