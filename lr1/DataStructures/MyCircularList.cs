using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace lr1.DataStructures
{
    public class MyCircularList<T> : IDataList<T>
    {
        private Node<T>? _head;
        private Node<T>? _tail;
        public int Count { get; private set; }

        public MyCircularList()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        public void Add(T item)
        {
            Node<T> newNode = new Node<T>(item);
            if (_head == null)
            {
                _head = newNode;
                _tail = newNode;
                newNode.Next = _head;
            }
            else
            {
                newNode.Next = _head;
                _tail!.Next = newNode; // _tail не може бути null, якщо _head не null
                _tail = newNode;
            }
            Count++;
        }

        public void InsertAt(T item, int index)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of bounds.");

            if (index == Count)
            {
                Add(item);
                return;
            }

            Node<T> newNode = new Node<T>(item);
            if (index == 0)
            {
                if (_head == null)
                {
                    _head = newNode;
                    _tail = newNode;
                    newNode.Next = _head;
                }
                else
                {
                    newNode.Next = _head;
                    _tail!.Next = newNode;
                    _head = newNode;
                }
            }
            else
            {
                Node<T> current = _head!; // _head не може бути null, якщо Count > 0
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next!;
                }
                newNode.Next = current.Next;
                current.Next = newNode;
            }
            Count++;
        }


        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of bounds.");
            if (_head == null) return; // Список пустий

            if (Count == 1)
            {
                _head = null;
                _tail = null;
            }
            else if (index == 0)
            {
                _head = _head.Next;
                _tail!.Next = _head; // _tail не може бути null, якщо Count > 1
            }
            else
            {
                Node<T> current = _head;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current!.Next; // current не може бути null
                }
                current!.Next = current.Next!.Next; // current і current.Next не можуть бути null
                if (index == Count - 1)
                {
                    _tail = current;
                }
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
                current = current!.Next;
            }
            return current!.Value;
        }

        public int IndexOf(T item)
        {
            if (_head == null) return -1;

            Node<T> current = _head;
            for (int i = 0; i < Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, item))
                {
                    return i;
                }
                current = current.Next!;
            }
            return -1;
        }

        public T? FindFirst(Func<T, bool> predicate)
        {
            if (_head == null) return default;

            Node<T> current = _head;
            for (int i = 0; i < Count; i++)
            {
                if (predicate(current.Value))
                {
                    return current.Value;
                }
                current = current.Next!;
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
            _tail = null;
            Count = 0;
        }

        public override string ToString()
        {
            List<string> items = new List<string>();
            if (_head == null) return "[]";

            Node<T> current = _head;
            for (int i = 0; i < Count; i++)
            {
                items.Add(current.Value?.ToString() ?? "null");
                current = current.Next!;
            }
            return $"[{string.Join(", ", items)}]";
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T>? current = _head;
            for (int i = 0; i < Count && current != null; i++)
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