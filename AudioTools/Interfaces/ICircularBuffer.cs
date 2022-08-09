namespace AudioTools.Interfaces;

public interface ICircularBuffer<T> : IEnumerable<T>
{
    T this[int i] { get; set; }

    int Length { get; }

    void Add(T value);
    void Clear();
}