using AudioTools.Implementation;
using AudioTools.Interfaces;

namespace AudioTools.Factories;
public class Mp3FileWriterFactory : IMp3FileWriterFactory
{

    public IMp3FileWriter Create(string audioFilePath)
    {
        return new Mp3FileWriter(audioFilePath);
    }
}
