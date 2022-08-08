using NAudio.Wave;

namespace AudioTools;

public static class AudioSystem
{
    public static List<WaveOutCapabilities> WaveOutDeviceCapabilities { get; } = new List<WaveOutCapabilities>();
    
    static AudioSystem()
    {
        int count = WaveOut.DeviceCount;
        for (int i = 0; i < count; i++)
        {
            WaveOutDeviceCapabilities.Add(WaveOut.GetCapabilities(i));
        }
    }
}
