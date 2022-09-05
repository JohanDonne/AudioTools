using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioTools.Interfaces;

public interface IMp3FileWriter
{ 
    void WriteSampleFrame(AudioSampleFrame frame);
    void Close();
}
