using AudioTools.Interfaces;
using System.Collections;

namespace AudioTools.Implementation;

public class CircularBuffer<T> : ICircularBuffer<T>
{
    private T[]? _buffer;
    private int _head;
    private readonly int _capacity;
    private bool _changed = false;

    public int Length => _buffer?.Length ?? 0;

    public T this[int i]
    {
        get
        {
            return i >= 0 && i < _capacity ? _buffer![(_head + 1 + i) % _capacity] : throw new IndexOutOfRangeException();
        }
        set
        {
            if (i >= 0 && i < _capacity)
            {
                _buffer![(_head + 1 + i) % _capacity] = value;
                _changed = true;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }

    public CircularBuffer(int capacity)
    {
        this._capacity = capacity;
        Clear();
    }

    public void Clear()
    {
        _buffer = new T[_capacity];
        _head = _capacity;
        _changed = true;
    }

    public void Add(T value)
    {
        _head++;
        if (_head >= _capacity)
        {
            _head = 0;
        }

        _buffer![_head] = value;
        _changed = true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        _changed = false;
        for (int i = 0; i < _capacity; i++)
        {
            yield return !_changed ? this[i] : throw new InvalidOperationException("Buffer was modified during enumeration.");
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
