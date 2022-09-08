namespace AudioTools.Interfaces;

public interface IDelayLine<T>
{
    int Delay { get; set; }

    T Dequeue();
    void Enqueue(T value);
    void Clear();
}