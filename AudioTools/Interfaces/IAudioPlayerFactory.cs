namespace AudioTools.Interfaces;
public interface IAudioPlayerFactory
{
    IAudioPlayer Create(int sampleRate);
    IAudioPlayer Create(string deviceProductName, int sampleRate);
}