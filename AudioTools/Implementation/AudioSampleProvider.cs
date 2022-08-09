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

    private const int bufferSize = 30000;
    private const int sampleLowMark = 20000;
    private const int sampleHighMark = 20000;
    private readonly Queue<float> sampleBuffer;
    

    public WaveFormat WaveFormat { get; }

    public event Action<int>? OnSampleFramesNeeded;

    public AudioSampleProvider(WaveFormat waveFormat)
    {
        WaveFormat = waveFormat;
        sampleBuffer = new Queue<float>(bufferSize);
       
    }

    public void Write(AudioSampleFrame frame)
    {
        sampleBuffer.Enqueue(frame.Left);
        sampleBuffer.Enqueue(frame.Right);
    }
    public int Read(float[] buffer, int offset, int count)
    {
        int index = 0;
        //read available samples
        while((index < count) && (sampleBuffer.Count > 0))
        {
            buffer[offset + index] = sampleBuffer.Dequeue();
            index++;
        }
        // and pad with silence
        while(index < count)
        {
            buffer[offset + index] = 0F;
            index++;
        }
        if(sampleBuffer.Count < sampleLowMark) OnSampleFramesNeeded?.Invoke(sampleHighMark - sampleBuffer.Count-10);

        return index;
    }
}
