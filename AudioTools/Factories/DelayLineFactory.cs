using AudioTools.Implementation;
using AudioTools.Interfaces;

namespace AudioTools.Factories;

public class DelayLineFactory : IDelaylineFactory
{
    public IDelayLine<T> Create<T>(int capacity)
    {
        return new DelayLine<T>(capacity);
    }
}
