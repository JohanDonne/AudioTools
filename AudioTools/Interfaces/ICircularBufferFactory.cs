namespace AudioTools.Interfaces;
public interface ICircularBufferFactory
{
    ICircularBuffer<T> Create<T>(int capacity);
}