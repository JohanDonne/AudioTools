using AudioTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioTools.Interfaces;

public interface IAudioPlayer:IDisposable
{
    event Action<int> SampleFramesNeeded;

    float Volume { get; set; }

    void Stop();
    void Start();

    void WriteSampleFrame(AudioSampleFrame frame);
    
}
