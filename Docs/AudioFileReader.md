### AudioFileReader

The `AudioFileReader` class is used to read individual audio samples from an audio file in a supported format (most audio- and video formats will be supported).



Namespace:  AudioTools.Implementation

##### Constructor

`AudioFileReader(string filePath)`

&nbsp;&nbsp;&nbsp;&nbsp;*filePath*: A string containing the path to the file to be used as the source for the audio samples.

##### Properties

`SampleRate`    
The samplerate used in recording the audio source (int, readonly).    
    

`TimeLength`    
The duration of the complete recording (TimeSpan, readonly).    
    

`TimePosition`    
The time position of the next sample frame that will be read by calling `ReadSampleFrame` (TimeSpan, readonly).    
    

`Volume`    
A multiplicationfactor that will be applied to the samples read (float in the range of 0..1.0, default = 1.0).    
    


##### Methods

`AudioSampleFrame ReadSampleFrame()`

Returns the next sample for the left and right audio-channel combined in an AudioSampleFrame.   
When reading past the end of the audiosource, silence will be returned (an AudioSampleFrame with both 'Left'- and 'Right' properties set to 0.0)
 

`int ReadSamples(float[] left, float[] right)`

Fills the provided arrays with samples from the audio source and returns the number of samples read.    
This number will normally be equal to the length of the provided arrays unless there where not enough samples vailable in the audio source. So this method will not return silence when trying to read past the end of the source.

`void Dispose()`    

Releases all resources used by the `AudioFileReader` instance (including unmanaged resources).    
Should be called when the instance will no longer be used. Once `Dispose`is called, the reader should no longer be used.


##### Events 

None


##### remarks

* `AudioFileReader` implements `IDisposable`. Therefore `Dispose` should be called when the reader is no longer used.
* When reading a mono audio source (with a single audio-channel), both `Left` and `Right` will contain the same value read from the original sample.