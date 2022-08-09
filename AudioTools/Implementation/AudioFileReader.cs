using AudioTools.Interfaces;
using NAudio.Wave;

namespace AudioTools.Implementation;

public class AudioFileReader : IAudioFileReader
{
    private NAudio.Wave.AudioFileReader? fileReader;
    private WaveChannel32? waveChannel;
    private ISampleProvider? sampleProvider;
    private bool disposedValue;

    public int SampleRate => waveChannel?.WaveFormat.SampleRate ?? 0;
    public TimeSpan TimeLength => waveChannel?.TotalTime ?? TimeSpan.FromSeconds(0);
    public TimeSpan TimePosition => waveChannel?.CurrentTime ?? TimeSpan.FromSeconds(0);
    public float Volume
    {
        get => waveChannel?.Volume ?? 0;
        set
        {
            if (waveChannel != null)
            {
                waveChannel.Volume = (value < 0) ? 0 : (value <= 1.0) ? value : 1F;
            }
        }
    }

    public AudioFileReader(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new ArgumentException($"file does not exist.");
        }

        try
        {
            fileReader = new NAudio.Wave.AudioFileReader(filePath);
        }
        catch
        {
            throw new ArgumentException($"filetype not supported.");
        }

        waveChannel = new WaveChannel32(fileReader);
        sampleProvider = waveChannel.ToSampleProvider();
    }

    public AudioSampleFrame ReadSampleFrame()
    {
        float[] buffer = new float[2];
        sampleProvider?.Read(buffer, 0, 2);
        return new AudioSampleFrame(left: buffer[0], right: buffer[1]);
    }

    public int ReadSamples(float[] left, float[] right)
    {
        float[]? buffer = new float[left.Length * 2];
        int count = sampleProvider?.Read(buffer, 0, left.Length * 2) / 2??0;
        for (int i = 0; i < count; i++)
        {
            left[i] = buffer[i * 2];
            right[i] = buffer[(i * 2) + 1];
        }
        return count;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                waveChannel?.Dispose();
                fileReader?.Dispose();
            }
            sampleProvider = null;
            waveChannel = null;
            fileReader = null;
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~AudioReader()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
