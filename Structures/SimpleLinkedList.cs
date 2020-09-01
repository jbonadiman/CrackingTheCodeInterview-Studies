using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Structures
{
    public class SimpleLinkedList<T> : ICollection<T>, IEnumerable<T>, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>
    {
        private SimpleNode<T> head;
        private SimpleNode<T> tail;

        public int Count { get; private set; }

        public bool IsReadOnly => throw new NotImplementedException();

        public T this[int index]
        {
            get
            {
                int count = -1;
                SimpleNode<T> currentNode;

                currentNode = this.head;
                count++;
                try
                {
                    while (count < index)
                    {
                        count++;
                        currentNode = currentNode.Next;
                    }
                    return currentNode.Value;
                }
                catch (NullReferenceException)
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                int count = -1;
                SimpleNode<T> currentNode;

                currentNode = this.head;
                count++;
                try
                {
                    while (count < index)
                    {
                        count++;
                        currentNode = currentNode.Next;
                    }
                    SimpleNode<T> nextNode = currentNode.Next;
                    currentNode = new SimpleNode<T>(value, nextNode);
                }
                catch (NullReferenceException)
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public SimpleLinkedList()
        {
            this.head = null;
            this.tail = null;
        }

        public void Add(T value)
        {
            // First insertion
            if (this.head == null)
            {
                this.head = new SimpleNode<T>(value);
                this.tail = this.head;
                this.Count++;
            }
            else
            {
                this.tail.Next = new SimpleNode<T>(value);
                this.tail = this.tail.Next;
                this.Count++;
            }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder("[");
            str.Append(string.Join(", ", this));
            str.Append("]");

            return str.ToString();
        }

        public void Clear()
        {
            this.tail = this.head = null;
            this.Count = 0;
        }

        public bool Contains(T item)
        {
            foreach (var element in this)
            {
                if (element.Equals(item)) return true;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (arrayIndex + this.Count > array.Length)
            {
                throw new ArgumentException();
            }

            foreach (var element in this)
            {
                array[arrayIndex++] = element;
            }
        }

        public bool Remove(T item)
        {
            SimpleNode<T> currentNode = this.head;
            if (item.Equals(currentNode.Value))
            {
                this.head = currentNode.Next;
                this.Count--;
                return true;
            }

            while (currentNode != null)
            {
                if (currentNode.Next != null && item.Equals(currentNode.Next.Value))
                {
                    currentNode.Next = currentNode.Next?.Next;
                    this.Count--;
                    return true;
                }

                currentNode = currentNode.Next;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator() => new SimpleLinkedListEnumerator<T>(this);

        public int IndexOf(T item)
        {
            int index = -1;
            
            foreach (var element in this)
            {
                index++;
                if (element.Equals(item)) return index;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    internal class SimpleLinkedListEnumerator<T> : IEnumerator<T>
    {
        private SimpleLinkedList<T> internalList;
        private int index = -1;

        public SimpleLinkedListEnumerator(SimpleLinkedList<T> list) {
            this.internalList = list;
        }

        public T Current
        {
            get
            {
                if (index <= -1)
                {
                    throw new InvalidOperationException();
                }
                return this.internalList[index];
            }   
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            this.internalList = null;
        }

        public bool MoveNext()
        {
            index++;
            if (index < this.internalList.Count)
            {
                return true;
            }
            else
            {
                index = -1;
                return false;
            }
        }

        public void Reset()
        {
            index = -1;
        }
    }
}
