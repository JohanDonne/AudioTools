using AudioTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioToolsDemo;
internal class AudioController : IDisposable
{
    private IAudioReader? _reader;
    private IAudioPlayer? _player;
    private string _currentDevice;
    private bool _playing = false;
    private bool disposedValue;

    public List<string> Devices = (new List<string> { "Default" }).Concat(AudioSystem.OutputDeviceCapabilities.Select(c => c.ProductName)).ToList();    

    public TimeSpan AudioLength => _reader?.TimeLength ?? new TimeSpan();
    public TimeSpan AudioPosition => _reader?.TimePosition ?? new TimeSpan();

    public float Volume
    {
        get => (int)(_player?.Volume * 100f ?? 100f);
        set
        {
            if (_player == null) return;
            if (value < 0f) _player.Volume = 0f;
            if (value > 100f) _player.Volume = 1f;
            _player.Volume = value / 100f;
        }
    }

    public AudioController()
    {
        _currentDevice = Devices[0];
    }

    public void SetSource(string path)
    {
        _playing = false;
        _reader = new AudioReader(path);
        _player = new AudioPlayer(_currentDevice,_reader.SampleRate);
        _player.OnSampleFramesNeeded += Player_OnSampleFramesNeeded;
    }

    public void SetDevice(string device)
    { 
        _currentDevice= device;
        if (_reader != null)
        {
            var oldplayer = _player; 
            oldplayer!.OnSampleFramesNeeded -= Player_OnSampleFramesNeeded;
            _player = new AudioPlayer(_currentDevice, _reader.SampleRate);
            _player.OnSampleFramesNeeded += Player_OnSampleFramesNeeded;
            oldplayer?.Dispose();
        }
        if (_playing) _player?.Start();
    }

    private void Player_OnSampleFramesNeeded(int frameCount)
    {
        for (int i = 0; i < frameCount; i++)
        {
            _player?.WriteSampleFrame(_reader!.ReadSampleFrame());
        }
    }

    public void Start()
    {
        _player?.Start();
        _playing = true;
    }

    public void Stop()
    {
        _player?.Stop();
        _playing = false;   
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                Stop();
                _reader?.Dispose();
                _player?.Dispose();
            }

            _reader = null;
            _player = null;
            disposedValue = true;
        }
    }       

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
