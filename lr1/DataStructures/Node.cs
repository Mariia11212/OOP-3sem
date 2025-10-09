namespace lr1.DataStructures
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T>? Next { get; set; } // Може бути null

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }
}