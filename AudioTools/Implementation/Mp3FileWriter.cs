using AudioTools.Interfaces;
using NAudio.MediaFoundation;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioTools.Implementation;
public class Mp3FileWriter : IMp3FileWriter, IWaveProvider, ISampleProvider
{
    private readonly Queue<AudioSampleFrame> _samples = new();
    private readonly SampleToWaveProvider _converter;
    
    public int Samplerate => 44100;

    public string FilePath { get; }

    public WaveFormat WaveFormat => WaveFormat.CreateIeeeFloatWaveFormat(44100,2);

    public Mp3FileWriter(string filePath)
    {
        FilePath = filePath;
        _converter = new(this as ISampleProvider);
        MediaFoundationApi.Startup();
    }

    public void Close()
    {
        using var output = new FileStream(FilePath, FileMode.Create);
        MediaFoundationEncoder.EncodeToMp3(this, output);
    }

    
    public void WriteSampleFrame(AudioSampleFrame frame)
    {
        _samples.Enqueue(frame);
    }

    //Implement IWaveProvider
    public int Read(byte[] buffer, int offset, int count)
    {
        return _converter.Read(buffer, offset, count);
    }

    
    // Implement ISampleProvider
    public int Read(float[] buffer, int offset, int count)
    {
        int samplesRead = 0;
        while ((samplesRead < count-2) && (_samples.Count>0))
        {
            var sample = _samples.Dequeue();
            buffer[samplesRead++] = sample.Left;
            buffer[samplesRead++] = sample.Right;
        }
        return samplesRead;
    }    
}
