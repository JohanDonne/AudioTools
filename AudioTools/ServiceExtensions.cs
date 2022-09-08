using AudioTools.Factories;
using AudioTools.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AudioTools;
public static class ServiceExtensions
{

    public static void AddAudioServices(this ServiceCollection services)
    {
        services.AddTransient<ISignalGenerator>((s) => new SignalGeneratorFactory().Create());
        services.AddSingleton<ISignalGeneratorFactory, SignalGeneratorFactory>();   
        services.AddSingleton<IMp3FileWriterFactory, Mp3FileWriterFactory>();
        services.AddSingleton<IAudioPlayerFactory, AudioPlayerFactory>();
        services.AddSingleton<IAudioFileReaderFactory, AudioFileReaderFactory>(); 
        services.AddSingleton<ICircularBufferFactory, CircularBufferFactory>();
        services.AddSingleton<IDelaylineFactory, DelayLineFactory>();
    }
}
