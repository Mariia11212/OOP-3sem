using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis; 

namespace lr1.DataStructures
{
    public class MyDynamicArrayBasedList<T> : IDataList<T>
    {
        private T?[] _items;
        private int _capacity;
        public int Count { get; private set; }

        private const int DefaultCapacity = 4;

        public MyDynamicArrayBasedList()
        {
            _capacity = DefaultCapacity;
            _items = new T?[_capacity];
            Count = 0;
        }

        private void EnsureCapacity()
        {
            if (Count == _capacity)
            {
                _capacity *= 2;
                T?[] newItems = new T?[_capacity];
                Array.Copy(_items, newItems, Count);
                _items = newItems;
            }
        }

        public void Add(T item)
        {
            EnsureCapacity();
            _items[Count] = item;
            Count++;
        }

        public void InsertAt(T item, int index)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of bounds.");

            EnsureCapacity();
            Array.Copy(_items, index, _items, index + 1, Count - index);
            _items[index] = item;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of bounds.");

            Count--;
            Array.Copy(_items, index + 1, _items, index, Count - index);
            _items[Count] = default;
        }

        // GetAt тепер безпечніше, тому що використовує ! для гарантії, що елемент не null
        // якщо T є reference type, і вважається, що список не зберігає null для T.
        public T GetAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of bounds.");

          
            return _items[index]!;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<T?>.Default.Equals(_items[i], item))
                {
                    return i;
                }
            }
            return -1;
        }

        public T? FindFirst(Func<T, bool> predicate)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_items[i] != null && predicate(_items[i]!))
                {
                    return _items[i];
                }
            }
            return default;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void Clear()
        {
            Array.Fill(_items, default);
            Count = 0;
        }

        public override string ToString()
        {
            List<string> items = new List<string>();
            for (int i = 0; i < Count; i++)
            {
                items.Add(_items[i]?.ToString() ?? "null");
            }
            return $"[{string.Join(", ", items)}]";
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _items[i]!; 
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}