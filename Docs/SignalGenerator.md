### SignalGenerator

The `SignalGenerator` class is used to generate fundamental signals (Sine, Square...).


Namespace:  AudioTools.Implementation

##### Constructors

`SignalGenerator()`
     

##### Properties

`SampleRate`    
The samplerate used in recording the audio source (int, readonly).    
    
`SignalType`    
The type of signal generated (`AudioSignalType` enum value)

`Frequency`     
The frequency of the generated signal. Ignored for noise signals, Starting frequency for the 'Sweep' signal (double, default = 100Hz).

`FrequencyEnd`    
Upper frequency for the 'Sweep' signal. Ignored when another type is selected (double, Default = 20.000Hz).

`SweepLengthSeconds`     
Duration (in seconds) of a single sweep for the 'Sweep' signal. Ignored when another type is selected (double, default = 10sec).
    

##### Methods

`AudioSampleFrame ReadSampleFrame()`

Returns the next sample for the left and right audio-channel combined in an AudioSampleFrame.   
 

`int ReadSamples(float[] left, float[] right)`

Fills the provided arrays with samples from the signal generator and returns the number of samples read.    
This number will be equal to the length of the provided arrays.


##### Events 

None

##### Remarks

* The available signaltypes can be found in the `AudioSignalType` enum in the `Audiotools.Interfaces` namespace.     
  They are: pink noise, white noise, frequency sweep, sine, square, triangle, sawtooth. 
