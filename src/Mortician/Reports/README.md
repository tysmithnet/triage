# Reports
A `IReport` is a managed interface over the output of a typical debugger command. Although it can be used to execute anything
it's that is not true to its intention.

### Glossary
| Term | Definition |
|---|---|
| Report | A managed API over a command e.g. `!runaway`, `~*e !clrstack`. |
|ReportFactory|Responsible for executing a command and processing its output to produce a report|

### Implementation notes
- Reports are generated **BEFORE** any repositories or analyzers are even created
  - Can't rely on any imports
  - Don't write reports that hog the thread 
  - Don't write reports that can easily be generated from the available repositories
    - Don't run !dumpheap -stat because the object repo can do this for you
- Each factory will be asked to execute a command and store the output
   - This happens serially because the debugging interface only supports STA threads
   - For this reason you only perform the commands you need to generate the output required to produce the report, but do not do any actual processing at this stage. 
- Each factory will then be asked to process the output from step 1, but this will happen in parallel

### Built In Reports
- [x] !runaway
- [x] !eestack
- [x] !dumpdomain
- [ ] !threadpool
- [ ] .chain
- [ ] !threads
- [ ] !eeversion
- [ ] ~*e k
- [ ] ~*e !clrstack
- [ ] x *!
- [ ] !analyze -v
  - [ ] Look up error code