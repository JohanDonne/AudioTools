namespace AudioTools.Interfaces;
public interface IDelaylineFactory
{
    IDelayLine<T> Create<T>(int capacity);
}