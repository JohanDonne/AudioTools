namespace AudioTools.Interfaces;
public interface IMp3FileWriterFactory
{
    IMp3FileWriter Create(string audioFilePath, int samplerate);
}