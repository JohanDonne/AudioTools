# AudioTools
A basic wrapper for the NAudio Library to facilitate experimenting with digital audio.    

[NAudio](https://github.com/naudio/NAudio) is an excellent, powerful library for developing applications that deal with any kind of digital audio processing on Windows. However, it can be a bit overwhelming for someone who is starting in the field of audio processing or who simply wants to experiment without bothering too much with the technical details.
The AudioTools library aims to provide easy access to basic functionality for those scenario's.   
    
Note: This library targets .Net 6 and Windows applications.

If you like this, spread the word by giving a star. If you have any suggestions, recommendations or complaints, contact the author.
    
Disclaimer: No attempt is made for ultra-efficient code or low latency processing. When those are important, using a wrapper is probably not a good idea. In that case I highly recommend using NAudio itself and spending some time learning to use it in an efficient way.

[Getting Started](Docs/GettingStarted.md)    
[Representing audio information: AudioSampleFrame](Docs/AudioSampleFrame.md)   
[Reading samples from an audio file: AudioFileReader](Docs/AudioFileReader.md)   
[Playing a stream of audio samples: AudioPlayer](Docs/AudioPlayer.md)       
[Writing a stream of audio samples to an mp3 file: Mp3FileWriter](Docs/Mp3FileWriter.md)    
[Generating audio signals: SignalGenerator](Docs/SignalGenerator.md)    
Useful collections: [CircularBuffer](Docs/circularBuffer.md), [DelayLine](Docs/DelayLine.md)     
[Using AudioTools with Dependency Injection](Docs/DependencyInjection.md)
  
