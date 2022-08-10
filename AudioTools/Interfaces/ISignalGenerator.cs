using NAudio.Wave.SampleProviders;

namespace AudioTools.Interfaces;
public interface ISignalGenerator
{
    int SampleRate { get; }
    SignalGeneratorType SignalType { get; set; }
    double Frequency { get; set; }
    double FrequencyEnd { get; set; }
    double SweepLengthSecondss { get; set; }
    AudioSampleFrame ReadSampleFrame();
    int ReadSamples(float[] left, float[] right);

}
