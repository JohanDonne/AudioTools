using AudioTools.Interfaces;
using NAudio.MediaFoundation;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace AudioTools.Implementation;
public class Mp3FileWriter : IMp3FileWriter, IWaveProvider, ISampleProvider, IDisposable
{
    private MemoryStream? _memory = new();
    private BinaryWriter? _writer;
    private BinaryReader? _reader;
    private bool _closed = false;
    private bool _disposedValue;
    private readonly string _filePath;
    private readonly SampleToWaveProvider _converter;

    //public int Samplerate => 44100;
    
    public WaveFormat WaveFormat { get; private set; }  
    
    public Mp3FileWriter(string filePath, int samplerate)
    {
        WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(samplerate, 2);
        _filePath = filePath;
        _converter = new(this as ISampleProvider);
        _memory = new MemoryStream();
        _writer = new BinaryWriter(_memory);
        MediaFoundationApi.Startup();

    }

    public void Close()
    {
        if (_closed)
        {
            return;
        }

        _closed = true;
        _writer?.Flush();
        _memory?.Seek(0, SeekOrigin.Begin);
        using (_reader = new(_memory!))
        {
            using FileStream? output = new(_filePath, FileMode.Create);
            MediaFoundationEncoder.EncodeToMp3(this, output, desiredBitRate: 320000);
        }
        Dispose(disposing: true);
    }

    public void WriteSampleFrame(AudioSampleFrame frame)
    {
        if (!_closed)
        {
            _writer!.Write(frame.Left);
            _writer!.Write(frame.Right);
        }
    }

    //Implement IWaveProvider
    public int Read(byte[] buffer, int offset, int count)
    {
        return _converter.Read(buffer, offset, count);
    }

    // Implement ISampleProvider
    public int Read(float[] buffer, int offset, int count)
    {
        int index = 0;
        while ((_reader!.BaseStream.Position != _reader.BaseStream.Length) && (index < count))
        {
            buffer[index++] = _reader.ReadSingle();
            buffer[index++] = _reader.ReadSingle();
        }
        return index;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _reader?.Dispose();
                _writer?.Dispose();
                _memory?.Dispose();
            }
            _reader = null;
            _writer = null;
            _memory = null;
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
