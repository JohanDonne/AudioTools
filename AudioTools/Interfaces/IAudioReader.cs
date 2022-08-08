using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioTools;

public interface IAudioReader:IDisposable
{
    int SampleRate { get; }
    TimeSpan TimeLength { get; }                
    TimeSpan TimePosition { get; }
    float Volume { get; set; }
    AudioSampleFrame ReadSampleFrame();
    int ReadSamples (float[] left, float[] right);
}
