using AudioTools.Interfaces;

namespace AudioTools.Implementation;

public class DelayLine<T> : IDelayLine<T>
{
    private T[]? _buffer;
    private int _head;
    private readonly int _capacity;
    private int _delay;

    public int Delay
    {
        get => _delay;
        set => _delay = (value < 0) ? 0 : (value < _capacity) ? value : _capacity - 1;
    }

    public DelayLine(int maxDelay)
    {
        this._capacity = maxDelay+1;
        Clear();
    }

    public void Clear()
    {
        _buffer = new T[_capacity];
        _head = 0;
    }

    public void Enqueue(T value)
    {
        _head++;
        if (_head >= _capacity)
        {
            _head = 0;
        }

        _buffer![_head] = value;
    }

    public T Dequeue()
    {
        int position = (_capacity + _head - _delay) % _capacity;
        return _buffer![position];
    }
}
