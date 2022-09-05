### Mp3FileWriter

The `Mp3FileWriter` class is used to record a stream of AudioSampleFrames to an mp3 file.


Namespace:  AudioTools.Implementation

##### Constructors

`AudioPlayer(string filePath, int samplerate)`

&nbsp;&nbsp;&nbsp;&nbsp;*filePath*: A string containing the path to the file to te be created (existing files will be overwritten).   
&nbsp;&nbsp;&nbsp;&nbsp;*samplerate*: The rate at which the samples have been acquired (to be read from the audio source).     
     

##### Properties

None   
    

##### Methods

`void WriteSampleFrame(AudioSampleFrame frame)`

Writes the next sample for the left and right audio-channels to the internal recording buffer.

`void Close()`

Encodes the buffered audio samples to an mp3 file and closes the `Mp3FileWriter` instance. A closed instance can no longer be used.



`void Dispose()`    

Releases all resources used by the `Mp3FileWriter` instance (including unmanaged resources).    
Should be called when the instance will no longer be used. Once `Dispose`is called, the writer should no longer be used.    
Note: Disposing a writer that was not previously closed will clear all buffered audio samples and will abort the creation of the mp3 file.


##### Events 

None

##### Remarks

* The `Mp3FileWriter` records the provided audio samples to a stereo mp3 file with a sample rate of 44100 Hz an a fixed bitrate of 320k.
* The actual mp3 encoding/compression can only take place after all samples are available. Therefore all provided samples are buffered in a MemoryStream. The amount of memory that will be used depends on the duration of the audio fragment and its sample rate. Example: a 1-minute audiofragment generated with a sample rate of 44100 Hz wil require 60 x 44100 x 2 floats = 21.168.000 bytes of memory. Therefore, even if the recording is aborted, it is important to Dispose of the `Mp3FileWriter` instance in order to release the buffer memory!
