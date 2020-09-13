using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class ArrayList<T> : ICollection<T>, IEnumerable<T>, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>
{
  private T[] internalArray;
  private int insertionIndex;
  const int DEFAULT_CAPACITY = 8;

  public int Count { get; private set; }

  public int Capacity
  {
    get { return this.internalArray.Length; }
  }

  public bool IsReadOnly => throw new NotImplementedException();

  public T this[int index]
  {
    get
    {
      if (index < 0 || index > this.Count)
      {
        throw new IndexOutOfRangeException();
      }

      return this.internalArray[index];
    }
    set
    {
      if (index < 0 || index > this.Count)
      {
        throw new IndexOutOfRangeException();
      }

      this.internalArray[index] = value;
    }
  }

  public ArrayList(int capacity)
  {
    this.internalArray = new T[capacity];
    this.Count = 0;
    this.insertionIndex = 0;
  }

  public ArrayList() : this(DEFAULT_CAPACITY) { }

  public void Add(T value)
  {
    if (this.insertionIndex >= this.Capacity)
    {
      T[] temp = new T[this.Capacity * 2];
      for (int i = 0; i < this.Capacity; i++)
      {
        temp[i] = this.internalArray[i];
      }

      temp[this.insertionIndex] = value;
      this.internalArray = temp;
    }
    else
    {
      this.internalArray[this.insertionIndex] = value;
    }
    this.insertionIndex++;
  }

  public override string ToString()
  {
    StringBuilder str = new StringBuilder("[");
    str.Append(string.Join(", ", this.internalArray));
    str.Append("]");

    return str.ToString();
  }

  public void Clear()
  {
    this.Count = 0;
    this.insertionIndex = 0;
    this.internalArray = new T[DEFAULT_CAPACITY];
  }

  public bool Contains(T item)
  {
    foreach (var element in this.internalArray)
    {
      if (item.Equals(element)) return true;
    }
    return false;
  }


  public void CopyTo(T[] array, int arrayIndex)
  {
    if (arrayIndex + this.Count > array.Length)
    {
      throw new ArgumentException();
    }

    foreach (var element in this.internalArray)
    {
      array[arrayIndex++] = element;
    }
  }

  public bool Remove(T item)
  {
    bool write = false;
    for (int i = 0; i < this.Count - 1; i++)
    {
      if (item.Equals(this.internalArray[i])) write = true;
      if (write) this.internalArray[i] = this.internalArray[i + 1];
    }

    if (!write && item.Equals(this.internalArray[this.Count - 1])) this.internalArray[this.Count - 1] = default;
    return false;
  }
  public IEnumerator<T> GetEnumerator() => new Enumerator<T>(this);
  public int IndexOf(T item)
  {
    throw new NotImplementedException();
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
  public struct Enumerator<U> : IEnumerator<T>, IDisposable
  {
    private ArrayList<T> internalList;
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

    public Enumerator(ArrayList<T> list)
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