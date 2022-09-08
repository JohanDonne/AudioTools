using System;
using System.Collections.Generic;

namespace AudioToolsDemo;
public interface IAudioController
{
    public List<string> Devices { get; }
    TimeSpan AudioLength { get; }
    TimeSpan AudioPosition { get; }
    TimeSpan EchoDelay { get; set; }
    bool IsRecording { get; }
    float Volume { get; set; }

    void Dispose();
    void SetDevice(string device);
    void SetSource(string path);
    void Start();
    void StartRecording();
    void Stop();
    void StopRecording();
}