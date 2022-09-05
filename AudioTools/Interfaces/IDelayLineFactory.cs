namespace AudioTools.Interfaces;
public interface IDelayLineFactory
{
    IDelayLine<T> Create<T>(int capacity);
}