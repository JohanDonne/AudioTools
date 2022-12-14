using AudioTools;
using AudioTools.Implementation;
using AudioTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToolsDemo;
internal class AudioController : IDisposable
{

    // max delay is calculated based an a maximum samplerate of 48000Hz and the constant MaxEchoDelay property. 
    private readonly IDelayLine<AudioSampleFrame> _delayLine = new DelayLine<AudioSampleFrame>((int) (MaxEchoDelay.TotalSeconds * 48000));
    private IAudioFileReader? _reader;
    private IAudioPlayer? _player;
    private IMp3FileWriter? _recorder;
    private string _currentDevice;
    private bool _playing = false;
    public bool IsRecording { get; private set; } = false;
    private TimeSpan _delay = TimeSpan.FromMilliseconds(0);
    private float _volume = 50f;
    private bool _disposedValue;

    public List<string> Devices => new List<string> { "Default" }.Concat(AudioSystem.OutputDeviceCapabilities.Select(c => c.ProductName)).ToList();    
    public TimeSpan AudioLength => _reader?.TimeLength ?? new TimeSpan();
    public TimeSpan AudioPosition => _reader?.TimePosition ?? new TimeSpan();

    public float Volume
    {
        get => (int)_volume;
        set
        {
            _volume = value < 0f ? 0f : value > 100f ? 100f : value;
            if (_player == null) return;            
            _player.Volume = _volume / 100f;
        }
    }

    public static TimeSpan MaxEchoDelay => TimeSpan.FromSeconds(1);

    public TimeSpan EchoDelay
    {
        get => TimeSpan.FromMilliseconds(_delayLine.Delay * 1000 / (_reader?.SampleRate??44100));
        set
        {
            _delay = (value <= MaxEchoDelay) ? value : MaxEchoDelay;
            _delayLine.Delay = TimeSpanToFrames(_delay);
        }
    }

    public AudioController()
    {
        _currentDevice = Devices[0];
    }

    private int TimeSpanToFrames(TimeSpan interval)
    {
        return (int)interval.TotalMilliseconds * (_reader?.SampleRate ?? 0) / 1000;
    }

    public void SetSource(string path)
    {
        Stop();
        _reader?.Dispose();
        _reader = null;
        _player?.Dispose();
        _player = null;
        if (!string.IsNullOrWhiteSpace(path))
        {
            _reader = new AudioFileReader(path);
            CreatePlayer();
        }       
    }

    private void CreatePlayer()
    {
        _player = new AudioPlayer(_currentDevice, _reader!.SampleRate)
        {
            Volume = _volume
        };
        _delayLine.Clear();
        _player.SampleFramesNeeded += Player_OnSampleFramesNeeded;
    }

    public void SetDevice(string device)
    { 
        _currentDevice= device;
        if (_reader != null)
        {
            var oldplayer = _player; 
            oldplayer!.SampleFramesNeeded -= Player_OnSampleFramesNeeded;
            CreatePlayer();
            oldplayer?.Dispose();
        }
        if (_playing) _player?.Start();
    }

    private void Player_OnSampleFramesNeeded(int frameCount)
    {
        for (int i = 0; i < frameCount; i++)
        {
            var sampleFrame = CalculateNextFrame();
            if (IsRecording) _recorder!.WriteSampleFrame(sampleFrame);
            _player?.WriteSampleFrame(sampleFrame);
        }
    }

    private AudioSampleFrame CalculateNextFrame()
    {
        var frame = _reader!.ReadSampleFrame();
        _delayLine.Enqueue(frame);
        return frame + _delayLine.Dequeue().Amplify(0.7F);
    }

    public void Start()
    {
        if (_player == null) return;
        _player.Start();
        _playing = true;
    }

    public void StartRecording()
    {
        const string filePath = "fragment.mp3";
        if (!_playing) return;
        _recorder = new Mp3FileWriter(filePath, _reader!.SampleRate);
        IsRecording = true;
    }

    public void StopRecording()
    {
        if (!IsRecording) return;
        _recorder!.Close();
        _recorder = null;
        IsRecording = false;
    }

    public void Stop()
    {
        _player?.Stop();
        StopRecording();
        _playing = false;   
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                Stop();
                _reader?.Dispose();
                _player?.Dispose();
            }
            _reader = null;
            _player = null;
            _disposedValue = true;
        }
    }       

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
