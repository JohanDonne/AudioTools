### Integrating AudioTools with Microsoft Dependency Injection

The AudioTools library can be easily integrated with .Net Core Dependency Injection.

* Interfaces have been declared for all public classes in the library:    
  * `IAudioFileReader`
  * `IAudioPlayer`
  * `IMp3FileWriter`
  * `ISignalGenerator`
  * `ICircularBuffer\<T>`
  * `IDelayLine\<T>`
  
* Factory methods are available for all classes in the library. They can be injected via their associated Interfaces:    
  * `IAudioFileReaderFactory`
  * `IAudioPlayerFactory`
  * `IMp3FileWriterFactory`
  * `ISignalGeneratorFactory`
  * `ICircularBufferFactory`
  * `IDelayLineFactory`

* An Extension method is provided to register all relevant services: `AddAudioServices()`.

In the GitHub repository a DI version of the AudioToolsDemo App can be found ('AudioToolsDemoWithDI', DI setup can be found in App.xaml.cs).