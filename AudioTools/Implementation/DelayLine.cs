using AudioTools.Interfaces;

namespace AudioTools;

public class DelayLine<T> : IDelayLine<T>
{
    private T[]? buffer;
    private int head;
    private readonly int capacity;
    private int delay;

    public int Delay
    {
        get => delay;
        set => delay = (value < 0) ? 0 : (value < capacity) ? value : capacity - 1;
    }

    public DelayLine(int capacity)
    {
        this.capacity = capacity;
        Reset();
    }

    public void Reset()
    {
        buffer = new T[capacity];
        head = 0;
    }

    public void Enqueue(T value)
    {
        head++;
        if (head >= capacity)
        {
            head = 0;
        }

        buffer![head] = value;
    }

    public T Dequeue()
    {
        int position = (capacity + head - delay) % capacity;
        return buffer![position];
    }
}
