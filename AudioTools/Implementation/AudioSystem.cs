using NAudio.Wave;

namespace AudioTools;

public static class AudioSystem
{
    public static List<WaveOutCapabilities> OutputDeviceCapabilities { get; } = new List<WaveOutCapabilities>();
    
    static AudioSystem()
    {
        int count = WaveOut.DeviceCount;
        for (int i = 0; i < count; i++)
        {
            OutputDeviceCapabilities.Add(WaveOut.GetCapabilities(i));
        }
    }
}
