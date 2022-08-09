using AudioTools.Interfaces;
using System.Collections;

namespace AudioTools.Implementation;

public class CircularBuffer<T> : ICircularBuffer<T>
{
    private T[]? buffer;
    private int head;
    private readonly int capacity;
    private bool changed = false;

    public int Length => buffer?.Length ?? 0;

    public T this[int i]
    {
        get
        {
            if ((i < 0) || (i >= capacity))
            {
                throw new IndexOutOfRangeException();
            }

            return buffer![(head + 1 + i) % capacity];
        }
        set
        {
            if ((i < 0) || (i >= capacity))
            {
                throw new IndexOutOfRangeException();
            }

            buffer![(head + 1 + i) % capacity] = value;
            changed = true;
        }
    }

    public CircularBuffer(int capacity)
    {
        this.capacity = capacity;
        Clear();
    }

    public void Clear()
    {
        buffer = new T[capacity];
        head = capacity;
        changed = true;
    }

    public void Add(T value)
    {
        head++;
        if (head >= capacity)
        {
            head = 0;
        }

        buffer![head] = value;
        changed = true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        changed = false;
        for (int i = 0; i < capacity; i++)
        {
            if (changed)
            {
                throw new InvalidOperationException("Buffer was modified during enumeration.");
            }
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
