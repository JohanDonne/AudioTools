### CircularBuffer\<T>

The `CircularBuffer<T>` class is a generic array-like collection that (obviously) implements a circular buffer constructed with a specific capacity.
Elements can be added to the buffer in a regular way (just like adding to a List\<T>) but when more items are added than the capacity of the buffer, the newes items overwrite the oldest ones.
So the circular buffer (with a capacity n) retains the n last values that were added to the buffer.


Namespace:  AudioTools.Implementation

##### Constructors

`CircularBuffer(int capacity)`
    
&nbsp;&nbsp;&nbsp;&nbsp;*Capacity*: The number of values that will be retained in the buffer. 

&nbsp;&nbsp;&nbsp;&nbsp;Upon construction, all values in the buffer will be initialized to the default value of stored type.

##### Properties

`Length`    
The number of values retained in the buffer (int, readonly).    

##### Methods

`void Clear()`

Clears the items in the array to the default value for the stored type.   
 
`void Add(T value)`

Adds the value to the buffer (overwriting the oldest value).


##### Events 

None

##### Remarks

* The `CircularBuffer<T>` implements an indexer so its values can be accessed via an index: `buffer[0]` returns  the oldest value, `buffer[^1]` returns the newest value.
* `CircularBuffer<T>` implements 'IEnumerable\<T>'. Enumerating the buffer will yield the values from oldest to newest.
    
