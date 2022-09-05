using AudioTools.Interfaces;
using NAudio.Wave;

namespace AudioTools.Implementation;

public class AudioPlayer : IAudioPlayer
{
    private readonly AudioSampleProvider _sampleProvider;
    private WaveOutEvent? _waveOut;
    private readonly WaveFormat _format;
    private bool _disposedValue;

    public event Action<int>? OnSampleFramesNeeded;

    public float Volume
    {
        get => _waveOut?.Volume ?? 0;
        set
        {
            if (_waveOut != null)
            {
                _waveOut.Volume = (value < 0) ? 0 : (value <= 1.0) ? value : 1F;
            }
        }
    }

    public AudioPlayer(int sampleRate) : this(string.Empty, sampleRate) { }

    public AudioPlayer(string deviceProductName, int sampleRate)
    {
        _format = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 2);
        _sampleProvider = new AudioSampleProvider(_format);
        _sampleProvider.OnSampleFramesNeeded += SampleProvider_OnSampleFramesNeeded;

        _waveOut = new WaveOutEvent();
        if (!string.IsNullOrEmpty(deviceProductName))
        {
            _waveOut.DeviceNumber = GetDeviceId(deviceProductName);
        }
        _waveOut.Init(_sampleProvider);
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
        _waveOut?.Play();
    }

    public void Stop()
    {
        _waveOut?.Pause();
    }

    public void WriteSampleFrame(AudioSampleFrame frame)
    {
        _sampleProvider.Write(frame);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _sampleProvider.OnSampleFramesNeeded -= SampleProvider_OnSampleFramesNeeded;
                _waveOut?.Dispose();                
            }
            _waveOut = null; ;
            _disposedValue = true;
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
