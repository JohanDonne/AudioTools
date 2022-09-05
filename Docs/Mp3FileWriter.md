### Mp3FileWriter

The `Mp3FileWriter` class is used to record a stream of AudioSampleFrames to an mp3 file.


Namespace:  AudioTools.Implementation

##### Constructors

`AudioPlayer(int sampleRate)`

&nbsp;&nbsp;&nbsp;&nbsp;*samplerate*: the rate at which the samples shoud be played (typically 44100 or 48000 samples per second).

&nbsp;&nbsp;&nbsp;&nbsp;The audio will be played using the default playback device that is set in Windows.    
     
`AudioPlayer((string deviceProductName, int sampleRate)`

&nbsp;&nbsp;&nbsp;&nbsp;*deviceProductName*: the name of the device through which the audio shoule be played.    
&nbsp;&nbsp;&nbsp;&nbsp;*samplerate*: the rate at which the samples shoud be played (typically 44100 or 48000 samples per second).     
     
&nbsp;&nbsp;&nbsp;&nbsp;A list of available playback devices with their properties (including `ProductName`) can be read from `AudioSystem.OutputDeviceCapabilities` 


##### Properties

`Volume`    
A multiplicationfactor that will be applied to the samples before playing (float in the range of 0..1.0, default = 1.0).    
    

##### Methods

`void Start()`    

Starts playing.

`void Stop()`

Pauzes playing.

`void WriteSampleFrame(AudioSampleFrame frame)`    
Writes the next sample for the left and right audio-channels to be played. This method should be called repeatedly at a sufficient rate to provide the player with the necessary audio-samples (rate should be at least equal to the samplerate passed to the constructor).     
 

`void Dispose()`    

Releases all resources used by the `AudioPlayer` instance (including unmanaged resources).    
Should be called when the instance will no longer be used. Once `Dispose`is called, the player should no longer be used.



##### Events 

`Action<int> SampleFramesNeeded`

Signals that the Player needs samples to play. A suggested count for the number of samples to be provided (by calling `WriteSampleFrame`) is passed as a parameter.    
The handlers for this event will be called on a backgroud thread managed by the player. Any UI manipulation (or other operations with thread affinity) done from within the handler may have to take this into account. 


##### remarks

* `AudioPlayer` implements `IDisposable`. Therefore `Dispose` should be called when the reader is no longer used.
* Internally, the player uses a buffering mechanism for playback. The number of frames provided in response to the 'OnSampleFramesNeeded ' event is not critical. If you provide (much) less than requested, a new event will soon be triggered, involving extra CPU load.    
* In the extreme case where samples are not provided at a sufficient rate and the player runs out of samples to play, playback will be interrupted by silence until new samples are received.    
* If, on the other hand, you provide much more than the requested number of sample frames, additional memory will be required to buffer them all. In practice, the number requested by the `SampleFramesNeeded ` event is usually optimal for good sound quality.
