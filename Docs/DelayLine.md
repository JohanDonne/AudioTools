### DelayLine\<T>

The `DelayLine<T>` class is a generic queue-like collection that provides an adjustable delay between enqueuing and dequeuing an element. It can be very usefull when implementing effects like echo or reverb.
The `DelayLine<T>` can be used much in the same way as a regular `Queue<T>` with the following changes:    

•	A 'delay' can be set as a number of elements. When dequeueing an element, the result is the value in the queue offset 'delay' positions from the last value enqueued. A 'delay' of 0, will always yield the last value enqueued.    
•	Internally the `DelayLine` is implemented as a (circular) buffer with a specific capacity (passed as a constructor parameter). This capacity limits the value that can be set for 'delay'. 



Namespace:  AudioTools.Implementation

##### Constructors

`DelayLine(int maxDelay)`
    
&nbsp;&nbsp;&nbsp;&nbsp;*maxDelay*: The maximum value for 'Delay' that will/can be used with this instance. 

&nbsp;&nbsp;&nbsp;&nbsp;Upon construction, all values in the delayline will be initialized to the default value of stored type.

##### Properties

`Delay`    
The number of dequeuing calls needed before an enqueued element is returned by a subsequent call to `Dequeue`. A value of 0 will cause an enqueued element to be returned on the first subsequent call to `Dequeue` (int, default = 0);

##### Methods

`void Clear()`    
Clears the elements in the delay to the default value for the stored type.   
 
`void Enqueue(T value)`    
Enqueues the value for later (delayed) retrieval.    

`T Dequeue()`    
Returns the delayed value (or the default value for the stored type for the first calls until the first enqueued value has passed trough the delayline).


##### Events 

None

##### Remarks

* The `DelayLine<T>` is expected to be used with interleaved calls to `Enqueue' and `Dequeue'. When enqueing more elements than the 'delay' value without dequeueing, the oldest values will be lost. 
* For this class, a textual explanation of its use may seem more complcated than its actual use. In the demo application a delayline is used to add a simple attenuated echo-effect to an audio stream.
    
