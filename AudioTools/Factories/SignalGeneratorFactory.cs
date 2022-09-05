using AudioTools.Implementation;
using AudioTools.Interfaces;

namespace AudioTools.Factories;
public class SignalGeneratorFactory : ISignalGeneratorFactory
{
    public ISignalGenerator Create()
    {
        return new SignalGenerator();
    }
}
