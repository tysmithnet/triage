# Mortician.Core
This assembly contains the core types and abstractions required to interface with Mortician. If you are a plugin author, you will at a minimum need to reference this code.

### Glossary
| Term                  | Definition |
|-----------------------|------------|
|Analysis Observer      |This is for all intents and purposes, an analyzer. The distinction comes with their lifetime. After all analyzers have finished executing, the EventHub is shut down and analysis observers are given a chance to finish up what observation they were performing.|
|Analyzer               |This is the core unit of extensibilty. If you implement `IAnalyzer` you are free to import any reports, repositories, event hubs, settings, etc in order to perform work. Analyzers are given an opportunity to perform any setup routines they might require before performing their work. They are then processed in parallel. Analyzers can do almost anything -create files, update databases, run other programs, serve web pages, and more.|
|CodeLocation           |Represents some instruction code in the memory dump.|
|Engine                 |The engine is the most central and important part of the application.  It is responsible for providing a stable execution environment for `IAnalyzer`'s to do useful work.|
|Event Hub              |Used to send and receive information to various other decoupled components in the system e.g. notifying other components that a connection to AWS has been made|
|Object Extractor       |Users will want to be able extract information from their own types. If an extractor is capable of operating on object in the heap, it can extract some information to be used later by analyzers. The extraction happens very early in the process and so these cannot rely on components being setup at this time.
|Repository             |Collections related entities e.g. Objects, Threads, Exceptions, Modules, AppDomains. Analyzers will use these to perform their functions.|
|Settings               |Any component can be configured using the settings file. Any component can define its own settings and they will be processed at the start of execution. The only restrictions are that the type implement `ISettings` and has a default constructor. The settings file is a .json file with a single object within. Each property of the object corresponds to the type name. You should use the `AssemblyQualifiedName` of the type.|