using AudioTools.Implementation;
using AudioTools.Interfaces;

namespace AudioTools.Factories;
public class AudioPlayerFactory : IAudioPlayerFactory
{
    public IAudioPlayer Create(int sampleRate)
    {
        return new AudioPlayer(sampleRate);
    }

    public IAudioPlayer Create(string deviceProductName, int sampleRate)
    {
        return new AudioPlayer(deviceProductName, sampleRate);
    }
}
