namespace AudioTools.Interfaces;
public interface IAudioFileReaderFactory
{
    IAudioFileReader Create(string audioFilePath);
}