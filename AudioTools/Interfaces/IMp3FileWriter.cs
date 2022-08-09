using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioTools.Interfaces;

internal interface IMp3FileWriter
{ 
    int Samplerate { get; }
    string FilePath { get; }

    void WriteSampleFrame(AudioSampleFrame frame);
    void Close();
}
