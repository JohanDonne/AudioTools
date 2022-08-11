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

    private AudioSignalType _signalType = AudioSignalType.Sine;

    private readonly NAudio.Wave.SampleProviders.SignalGenerator _generator = new(44100, 2)
    {
        Frequency=100.0,
        FrequencyEnd = 20000.0,
        SweepLengthSecs = 10
    };
    
    public int SampleRate => 44100;
    public AudioSignalType SignalType
    {
        get => _signalType;
        set
        {
            _signalType = value;
            SetInternalType();
        }
    }
    public double Frequency { get => _generator.Frequency; set => _generator.Frequency = value; }
    public double FrequencyEnd { get => _generator.FrequencyEnd; set => _generator.FrequencyEnd = value; }
    public double SweepLengthSecondss { get => _generator.SweepLengthSecs; set => _generator.SweepLengthSecs = value; }

    private void SetInternalType()
    {
        _generator.Type = _signalType switch
        {
            AudioSignalType.Pink => SignalGeneratorType.Pink,
            AudioSignalType.White => SignalGeneratorType.White,
            AudioSignalType.Sweep => SignalGeneratorType.Sweep,
            AudioSignalType.Sine => SignalGeneratorType.Sin,
            AudioSignalType.Square => SignalGeneratorType.Square,
            AudioSignalType.Triangle => SignalGeneratorType.Triangle,
            AudioSignalType.SawTooth => SignalGeneratorType.SawTooth,
            _ => SignalGeneratorType.Sin
        };
    }

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
