using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis; // Для [NotNull]

namespace lr1.DataStructures
{
    public class MyLinkedList<T> : IDataList<T>
    {
        private Node<T>? _head;
        public int Count { get; private set; }

        public MyLinkedList()
        {
            _head = null;
            Count = 0;
        }

        public void Add(T item)
        {
            Node<T> newNode = new Node<T>(item);
            if (_head == null)
            {
                _head = newNode;
            }
            else
            {
                Node<T> current = _head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            Count++;
        }

        public void InsertAt(T item, int index)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of bounds.");

            if (index == 0)
            {
                Node<T> newNode = new Node<T>(item);
                newNode.Next = _head;
                _head = newNode;
            }
            else
            {
                // current не може бути null, якщо index > 0 і Count > 0
                Node<T> current = _head!;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next!;
                }

                Node<T> newNode = new Node<T>(item);
                newNode.Next = current.Next;
                current.Next = newNode;
            }
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of bounds.");

            if (index == 0)
            {
                _head = _head?.Next;
            }
            else
            {
                // current не може бути null, якщо index > 0 і Count > 0
                Node<T> current = _head!;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next!;
                }

                // current.Next не може бути null, оскільки index < Count
                current.Next = current.Next!.Next;
            }
            Count--;
        }

        public T GetAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of bounds.");
            if (_head == null) throw new InvalidOperationException("List is empty.");

            Node<T> current = _head;
            for (int i = 0; i < index; i++)
            {
                current = current!.Next; // current не може стати null до кінця циклу
            }
            return current!.Value; // current не може бути null, якщо index валідний
        }

        public int IndexOf(T item)
        {
            Node<T>? current = _head;
            int index = 0;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, item))
                {
                    return index;
                }
                current = current.Next;
                index++;
            }
            return -1;
        }

        public T? FindFirst(Func<T, bool> predicate)
        {
            Node<T>? current = _head;
            while (current != null)
            {
                if (predicate(current.Value))
                {
                    return current.Value;
                }
                current = current.Next;
            }
            return default;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void Clear()
        {
            _head = null;
            Count = 0;
        }

        public override string ToString()
        {
            List<string> items = new List<string>();
            Node<T>? current = _head;
            while (current != null)
            {
                items.Add(current.Value?.ToString() ?? "null");
                current = current.Next;
            }
            return $"[{string.Join(", ", items)}]";
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T>? current = _head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}