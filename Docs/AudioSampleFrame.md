### AudioSampleFrame

The `AudioSampleFrame` structs is used to represent the individual audio samples. It contains de values for the samples from left and right channel that were sampled at the same moment. Most classes and methods in this library will use the `AudioSampleFrame` as the basic entity to store or exchange audio-information.



Namespace:  AudioTools.Interfaces

##### Constructor

`AudioSampleFrame(float left, float right)`

&nbsp;&nbsp;&nbsp;&nbsp;*left*, *right*: the actual values for the left and right channel (range -1..1).

##### Properties

`Left`    
The actual values for the left channel (float, readonly).    
    

`Left`    
The actual values for the right channel (float, readonly).     
    
   

##### Methods

`Amplify(float factor)`

Returns a new AudioSampleFrame with the original values multiplied by `factor`.    
Note that 'saturation' arithmetic is used: the result will be clipped to the -1..1 range.
 
`bool Equals(AudioSampleFrame other)`

Used to compare with another sampleframe. 

`int GetHashCode()`

Returns a hashcode based on the values of `Left` and `Right`.

`string ToString()`

Returns a string representing the values for `Left` & `Right`.

##### Operators 

` == `, ` != ` : Check for equality of both `Left`& `Right` values.

` + `, ` - ` : Add/subtract the values for the respective `Left`& `Right` values using saturation arithmetic (clipped to the -1..1 range).


##### Events 

None

##### Remarks

* An `AudioSampleFrame` is immutable: the values it contains cannot be changed after construction.  