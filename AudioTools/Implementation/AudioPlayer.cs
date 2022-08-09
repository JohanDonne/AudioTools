using NAudio.Wave;

namespace AudioTools;

public class AudioPlayer : IAudioPlayer
{
    private readonly AudioSampleProvider sampleProvider;
    private WaveOutEvent? waveOut;
    private readonly WaveFormat format;
    private bool disposedValue;

    public event Action<int>? OnSampleFramesNeeded;

    public float Volume
    {
        get => waveOut?.Volume ?? 0;
        set
        {
            if (waveOut != null)
            {
                waveOut.Volume = (value < 0) ? 0 : (value <= 1.0) ? value : 1F;
            }
        }
    }

    public AudioPlayer(int sampleRate) : this(string.Empty, sampleRate) { }

    public AudioPlayer(string deviceProductName, int sampleRate)
    {
        format = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 2);
        sampleProvider = new AudioSampleProvider(format);
        sampleProvider.OnSampleFramesNeeded += SampleProvider_OnSampleFramesNeeded;

        waveOut = new WaveOutEvent();
        if (!string.IsNullOrEmpty(deviceProductName))
        {
            waveOut.DeviceNumber = GetDeviceId(deviceProductName);
        }
        waveOut.Init(sampleProvider);
    }

    private static int GetDeviceId(string device)
    {
        for (int i = 0; i < AudioSystem.OutputDeviceCapabilities.Count; i++)
        {
            if (AudioSystem.OutputDeviceCapabilities[i].ProductName == device)
            {
                return i;
            }
        }
        return -1;
    }

    private void SampleProvider_OnSampleFramesNeeded(int framesRequested)
    {
        OnSampleFramesNeeded?.Invoke(framesRequested);
    }

    public void Start()
    {
        waveOut?.Play();
    }

    public void Stop()
    {
        waveOut?.Pause();
    }

    public void WriteSampleFrame(AudioSampleFrame frame)
    {
        sampleProvider.Write(frame);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                sampleProvider.OnSampleFramesNeeded -= SampleProvider_OnSampleFramesNeeded;
                waveOut?.Dispose();                
            }
            waveOut = null; ;
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~AudioPlayer()
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
