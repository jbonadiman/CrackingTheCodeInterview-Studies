using System;

namespace Structures
{
    public class SimpleNode<T>
    {
        public T Value { get; private set; }
        public SimpleNode<T> Next { get; set; }

        public SimpleNode(T value, SimpleNode<T> next)
        {
            this.Value = value;
            this.Next = next;
        }

        public SimpleNode(T value) : this(value, null) { }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
