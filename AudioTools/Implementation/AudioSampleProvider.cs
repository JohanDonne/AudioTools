using AudioTools.Interfaces;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioTools.Implementation;

internal class AudioSampleProvider:ISampleProvider
{

    private const int _bufferSize = 30000;
    private const int _sampleLowMark = 20000;
    private const int _sampleHighMark = 20000;
    private readonly Queue<float> _sampleBuffer;
    

    public WaveFormat WaveFormat { get; }

    public event Action<int>? SampleFramesNeeded;

    public AudioSampleProvider(WaveFormat waveFormat)
    {
        WaveFormat = waveFormat;
        _sampleBuffer = new Queue<float>(_bufferSize);
       
    }

    public void Write(AudioSampleFrame frame)
    {
        _sampleBuffer.Enqueue(frame.Left);
        _sampleBuffer.Enqueue(frame.Right);
    }
    public int Read(float[] buffer, int offset, int count)
    {
        int index = 0;
        //read available samples
        while((index < count) && (_sampleBuffer.Count > 0))
        {
            buffer[offset + index] = _sampleBuffer.Dequeue();
            index++;
        }
        // and pad with silence
        while(index < count)
        {
            buffer[offset + index] = 0F;
            index++;
        }
        if(_sampleBuffer.Count < _sampleLowMark) SampleFramesNeeded?.Invoke(_sampleHighMark - _sampleBuffer.Count-10);

        return index;
    }
}
