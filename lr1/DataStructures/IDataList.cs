using System;
using System.Collections.Generic;

namespace lr1.DataStructures
{
    public interface IDataList<T> : IEnumerable<T> // Додано успадкування від IEnumerable<T>
    {
        int Count { get; }
        void Add(T item);
        void RemoveAt(int index);
        T GetAt(int index);
        int IndexOf(T item);
        T? FindFirst(Func<T, bool> predicate);
        bool Contains(T item);
        void Clear();
        void InsertAt(T item, int index);
    }
}