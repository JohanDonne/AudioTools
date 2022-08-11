namespace AudioTools.Interfaces;
public interface ISignalGenerator
{
    int SampleRate { get; }
    AudioSignalType SignalType { get; set; }
    double Frequency { get; set; }
    double FrequencyEnd { get; set; }
    double SweepLengthSecondss { get; set; }
    AudioSampleFrame ReadSampleFrame();
    int ReadSamples(float[] left, float[] right);

}
