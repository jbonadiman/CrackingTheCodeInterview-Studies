using Operations;
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

        public SimpleLinkedList(IEnumerable<T> collection) : this()
        {
            foreach (T element in collection)
            {
                this.Add(element);
            }
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

        public IEnumerator<T> GetEnumerator() => new Enumerator<T>(this);

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
            if (index < 0 || index > this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (index == 0)
            {
                var newNode = new SimpleNode<T>(item, this.head);
                this.head = newNode;
            }
            else
            {
                SimpleNode<T> currentNode = this.head;
                int currentIndex = 0;

                while (currentNode != null && currentIndex < index - 1)
                {
                    currentIndex++;
                    currentNode = currentNode.Next;
                }

                var newNode = new SimpleNode<T>(item, currentNode.Next);
                currentNode.Next = newNode;
            }

            this.Count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (index == 0)
            {
                this.head = head.Next;
            }
            else
            {
                SimpleNode<T> currentNode = this.head;
                int currentIndex = 0;

                while (currentNode != null && currentIndex < index)
                {
                    if (currentNode?.Next != null && currentIndex + 1 == index)
                    {
                        currentNode.Next = currentNode.Next?.Next;
                        break;
                    }

                    currentIndex++;
                    currentNode = currentNode.Next;
                }
            }
            
            this.Count--;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        //
        // Summary:
        //     Sorts the elements in the entire System.Collections.Generic.List`1 using the
        //     specified System.Comparison`1.
        //
        // Parameters:
        //   comparison:
        //     The System.Comparison`1 to use when comparing elements.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     comparison is null.
        //
        //   T:System.ArgumentException:
        //     The implementation of comparison caused an error during the sort. For example,
        //     comparison might not return 0 when comparing an item with itself.
        public static SimpleLinkedList<T> Sort(Comparison<T> comparison);
        //
        // Summary:
        //     Sorts the elements in the entire System.Collections.Generic.List`1 using the
        //     default comparer.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     The default comparer System.Collections.Generic.Comparer`1.Default cannot find
        //     an implementation of the System.IComparable`1 generic interface or the System.IComparable
        //     interface for type T.
        public static SimpleLinkedList<T> Sort(SimpleLinkedList<T> originalList)
        {
            return new SimpleLinkedList<T>(Sorting.MergeSort(originalList));
        }
        //
        // Summary:
        //     Sorts the elements in the entire System.Collections.Generic.List`1 using the
        //     specified comparer.
        //
        // Parameters:
        //   comparer:
        //     The System.Collections.Generic.IComparer`1 implementation to use when comparing
        //     elements, or null to use the default comparer System.Collections.Generic.Comparer`1.Default.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     comparer is null, and the default comparer System.Collections.Generic.Comparer`1.Default
        //     cannot find implementation of the System.IComparable`1 generic interface or the
        //     System.IComparable interface for type T.
        //
        //   T:System.ArgumentException:
        //     The implementation of comparer caused an error during the sort. For example,
        //     comparer might not return 0 when comparing an item with itself.
        public void Sort(IComparer<T>? comparer);


        public struct Enumerator<U> : IEnumerator<T>, IDisposable
        {
            private SimpleLinkedList<T> internalList;
            private int index;

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

            public Enumerator(SimpleLinkedList<T> list)
            {
                this.index = -1;
                this.internalList = list;
            }

            public void Dispose()
            {
                this.internalList = null;
            }

            public bool MoveNext()
            {
                this.index++;
                if (index < this.internalList.Count)
                {
                    return true;
                }
                else
                {
                    this.index = -1;
                    return false;
                }
            }

            public void Reset()
            {
                this.index = -1;
            }
        }
    }
}
