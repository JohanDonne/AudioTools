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
    private IAudioFileReader? _reader;
    private IAudioPlayer? _player;
    private Mp3FileWriter? _recorder;
    private string _currentDevice;
    private bool _playing = false;
    public bool IsRecording { get; private set; } = false;
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
        _reader = new AudioFileReader(path);
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
            var sampleFrame = _reader!.ReadSampleFrame();
            if (IsRecording) _recorder!.WriteSampleFrame(sampleFrame);
            _player?.WriteSampleFrame(sampleFrame);
        }
    }

    public void Start()
    {
        _player?.Start();
        _playing = true;
    }

    public void StartRecording()
    {
        const string filePath = "fragment.mp3";
        if (!_playing) return;
        _recorder = new Mp3FileWriter(filePath);
        IsRecording = true;
    }

    public void StopRecording()
    {
        if (!IsRecording) return;
        _recorder!.Close();
        IsRecording = false;
    }

    public void Stop()
    {
        _player?.Stop();
        if (IsRecording) _recorder!.Close();
        IsRecording = false;
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
