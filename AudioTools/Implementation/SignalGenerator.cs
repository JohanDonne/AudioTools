using AudioTools.Interfaces;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioTools.Implementation;
public class SignalGenerator : ISignalGenerator
{

    private readonly NAudio.Wave.SampleProviders.SignalGenerator _generator = new(44100, 2)
    {
        Frequency=100.0,
        FrequencyEnd = 20000.0,
        SweepLengthSecs = 10
    };
    
    public int SampleRate => 44100;
    public SignalGeneratorType SignalType { get => _generator.Type; set => _generator.Type = value; }
    public double Frequency { get => _generator.Frequency; set => _generator.Frequency = value; }
    public double FrequencyEnd { get => _generator.FrequencyEnd; set => _generator.FrequencyEnd = value; }
    public double SweepLengthSecondss { get => _generator.SweepLengthSecs; set => _generator.SweepLengthSecs = value; }
    
    public AudioSampleFrame ReadSampleFrame()
    {
        float[] buffer = new float[2];
        _generator.Read(buffer, 0, 2);
        return new AudioSampleFrame(left: buffer[0], right: buffer[1]);
    }

    public int ReadSamples(float[] left, float[] right)
    {
        float[]? buffer = new float[left.Length * 2];
        int count = _generator.Read(buffer, 0, left.Length * 2) / 2; 
        for (int i = 0; i < count; i++)
        {
            left[i] = buffer[i * 2];
            right[i] = buffer[(i * 2) + 1];
        }
        return count;
    }
}
