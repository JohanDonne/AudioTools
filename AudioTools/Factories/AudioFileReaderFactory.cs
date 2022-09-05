using AudioTools.Implementation;
using AudioTools.Interfaces;

namespace AudioTools.Factories;

public class AudioFileReaderFactory : IAudioFileReaderFactory
{
    public IAudioFileReader Create(string audioFilePath)
    {
        return new AudioFileReader(audioFilePath);
    }
}
