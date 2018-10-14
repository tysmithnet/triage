# Mortician
Herein lies the plumbing code. It is primarily responsible for creating an environment in which plugins can cooperate to analyze a memory dump.

### Process Overview
There are 2 main phases of the application life cycle. The first is setup and configuration. This is where command line arguments, settings files, etc are 
evaluated to ultimately provide an environment where plugins are able to do useful work and communicate with one another in an isolated fashion.
The second phase is plugin execution. The discovered and instantiated plugins are given the core components needed to do useful work.

### Technical Considerations
Obviously, we want to run in parallel what can be run in parallel. Unfortunately, processing the memory dump itself must be done in a 
single threaded fashion. This means that the components that plugins use must be created in a more controlled way. This work is done by 
the CoreComponentFactory.

### Process Outline
1. Basic application setup is performed
   1. Settings deserialization
   1. Find and load plugin assemblies
   1. Options processing
1. Memory dump is used to create the DataTarget for CLRMd
1. An adapter is wrapped around the DataTarget and all items produced from it so they have a Triage interface
1. tbd

### Features
- [x] Structured logging
- [ ] Performance counters
- [ ] Managed abstraction over 
- [ ] Minidump testing and support 